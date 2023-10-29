# Introduction

This project demonstrates serialization vulnerabilities using Newtonsoft.Json and BinarySerialization. The Todo project is baseed on the starter tutorial available here:

[Create a controller based API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)

These examples were assembled based on serialization vulnerabilities highlighted in the [ysoserial.net](https://github.com/pwntester/ysoserial.net) git repo.

# Vulnerabilies

## JSON Vunlnerability

The JSON vulnerability is exposed in Newtonsoft.Json through the use of `TypeNameResolver.`

With .NET 6+ it is not possible to override the default JSON serializer from System.Text.Json when using minimal APIs. See [Minimal APIs quick reference](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#configure-json-deserialization-options-for-body-binding).

