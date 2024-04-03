# Binary Serialization

The [BinaryFormatter](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter?view=net-8.0&?WT.mc_id=MVP_337682) is present in .NET Framework, .NET Core[^1], and .NET 5-8. Microsoft released an extensive statement about the dangers of using the BinaryFormatter here:

[BinaryFormatter Security Guide](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide?WT.mc_id=MVP_337682)

[^1]: Binary Formatter was removed in .NET Core 1.0, but reappeared in .NET Core 1.1 and onward.

On Feb. 9th, Microsoft announced the BinaryFormatter is being removed from .NET 9:

[Announcement: BinaryFormatter is being removed in .NET 9](https://github.com/dotnet/runtime/issues/98245)

## Migration Considerations

Migrating from the BinaryFormatter is not straight-forward as there is no direct replacement. If the BinaryFormatter has been used to persist data in a product or service then a migration strategy is necessary. The [BinaryFormatter Security Guide](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide?WT.mc_id=MVP_337682) recommends using:

- [XmlSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=net-8.0&WT.mc_id=MVP_337682) and [DataContractSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.datacontractserializer?view=net-8.0&WT.mc_id=MVP_337682) to serialize object graphs into and from XML. Do not confuse [DataContractSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.datacontractserializer?view=net-8.0&WT.mc_id=MVP_337682) with [NetDataContractSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.netdatacontractserializer?view=netframework-4.8.1?WT.mc_id=MVP_337682).
- [BinaryReader](https://learn.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-8.0&WT.mc_id=MVP_337682) and [BinaryWriter](https://learn.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-8.0&WT.mc_id=MVP_337682) for XML and JSON.
- The [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json?view=net-8.0&WT.mc_id=MVP_337682) APIs to serialize object graphs into JSON.

Other alternatives include:

- [MessagePack](https://msgpack.org/) is a fast binary serializer.
- [ProtoBuf](https://protobuf.dev/getting-started/csharptutorial/). This is used by some Microsoft teams[^2].

[^2]: See Preparing for migration in [BinaryFormatter is being removed in .NET 9 #293](https://github.com/dotnet/announcements/issues/293)