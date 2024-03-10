using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace TodoApi
{
    public class BinaryJsonConverter : JsonConverter
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
                BinaryFormatter binaryformatter = new BinaryFormatter();
                convertedObj = binaryformatter.Deserialize(stream);
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