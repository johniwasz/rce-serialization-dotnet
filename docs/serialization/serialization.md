# Introduction

This project demonstrates serialization vulnerabilities using Newtonsoft.Json and BinarySerialization. The Todo project is based on the starter tutorial available here:

[Create a controller based API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)

These examples were assembled based on serialization vulnerabilities highlighted in the [ysoserial.net](https://github.com/pwntester/ysoserial.net) git repo. This example reproduces a remote code execution vulnerability by exploiting the Newtonsoft.Json serializer. 

Prequistes

Install .NET Framework 4.8.1:

``` 
winget install Microsoft.DotNet.Framework.DeveloperPack_4 -v 4.8.1
```

## Projects

Each project demonstrates a serialization vulnerability.

[01-BinaryFormatterVulnerabilities](serialization/BinarySerialization.md)  
[02-Framework-JsonVulnerabilities](serialization/JSONSerialization.md)  
[03-.NET-JsonVulnerabilities](serialization/NET8JSON.md)

Load the solution in each project in Visual Studio, run the solution, and use the requests.http in the Solution Items folder to walk through the vulnerabilities.
