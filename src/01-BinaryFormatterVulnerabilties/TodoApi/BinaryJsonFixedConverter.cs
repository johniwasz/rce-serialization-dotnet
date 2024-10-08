using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Formats.Nrbf;

namespace TodoApi
{
    public class BinaryJsonFixedConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            object convertedObj = null;

            string origValue = reader.ValueType == typeof(string) ? reader.Value.ToString() : JRaw.Create(reader).ToString();

            if (string.IsNullOrEmpty(origValue))
            {
                return convertedObj;
            }

            byte[] buffer = Convert.FromBase64String(origValue);

            using (var stream = new MemoryStream(buffer))
            {
                SerializationRecord record = NrbfDecoder.Decode(stream);

                if (record is PrimitiveTypeRecord primitiveRecord)
                {
                    return primitiveRecord.Value;
                }
                else if (record is ClassRecord classRecord)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    foreach(string member in classRecord.MemberNames)
                    {
                        dict.Add(member, classRecord.GetRawValue(member));
                    }
                    return dict;
                }
                else if (record is SZArrayRecord<byte> arrayOfBytes)
                {
                    return arrayOfBytes;
                }
            }

            return convertedObj;
        }

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Serialize binary json using the BinaryFormatter
            BinaryFormatter binaryformatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                binaryformatter.Serialize(stream, value);
                // Convert stream to byte array
                byte[] byteArray = stream.ToArray();
                writer.WriteValue(Convert.ToBase64String(byteArray));
            }
        }
    }
}