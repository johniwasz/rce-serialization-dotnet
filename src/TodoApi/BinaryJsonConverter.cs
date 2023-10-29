using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Security.Claims;
using System.Data;

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

            string? origValue = reader.ValueType == typeof(string) ? reader.Value.ToString() : JRaw.Create(reader).ToString();

            if (string.IsNullOrEmpty(origValue))
            {
                return convertedObj;
            }

            byte[] buffer = Convert.FromBase64String(origValue!);

            using var stream = new MemoryStream(buffer);
            BinaryFormatter binaryformatter = new BinaryFormatter();
            convertedObj = binaryformatter.Deserialize(stream);

            return convertedObj;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
