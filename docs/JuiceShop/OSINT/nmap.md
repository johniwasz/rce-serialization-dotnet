# Introduction

Nmap is a versatile network discovery tool and more. It's often used in penetration testing and attacks.

> **Nmap may not be permitted in a work environment as it is often used for penetration testing and attacking. Executing network scans without permission may trigger security alerts and prompt a response from the organization's Security Operations Center (SOC), potentially leading to disciplinary actions or legal consequences. Therefore, it is crucial to always obtain permission and follow established protocols before utilizing tools like Nmap in a professional setting.**

Its capabilities include:

- Network discovery  
- Port scanning  
- Service detection (web server, email service)  
- Operating system detection  
- Scripting and automation  

For more information, please see [nmap Reference Guide](https://nmap.org/book/man.html).

If nmap is not installed, open a Windows Command or Powershell terminal and execute:

``` bat
winget install Insecure.Nmap
```

To scan the public OWASP Juice shop for open ports, execute:

``` bat
nmap demo.owasp-juice.shop
```

This returns:

``` text
Starting Nmap 7.94 ( https://nmap.org ) at 2024-04-08 20:27 Eastern Daylight Time
Nmap scan report for demo.owasp-juice.shop (81.169.145.156)
Host is up (0.12s latency).
Other addresses for demo.owasp-juice.shop (not scanned): 2a01:238:20a:202:1156::
rDNS record for 81.169.145.156: w9c.rzone.de
Not shown: 995 closed tcp ports (reset)
PORT     STATE    SERVICE
21/tcp   open     ftp
25/tcp   filtered smtp
80/tcp   open     http
443/tcp  open     https
8080/tcp open     http-proxy
```

Nmap can also detect the version of the service associated with the port using:

``` bat
nmap -sV demo.owasp-juice.shop
```

This returns:

``` text
Nmap done: 1 IP address (1 host up) scanned in 20.53 seconds
PS C:\Users\username\source\repos\rce-serialization-dotnet> nmap -sV -sS  demo.owasp-juice.shop
Starting Nmap 7.94 ( https://nmap.org ) at 2024-04-08 20:42 Eastern Daylight Time
Nmap scan report for demo.owasp-juice.shop (81.169.145.156)
Host is up (0.11s latency).
Other addresses for demo.owasp-juice.shop (not scanned): 2a01:238:20a:202:1156::
rDNS record for 81.169.145.156: w9c.rzone.de
Not shown: 995 closed tcp ports (reset)
PORT     STATE    SERVICE    VERSION
21/tcp   open     ftp        ftpd.bin round-robin file server 3.4.0r16
25/tcp   filtered smtp
80/tcp   open     http-proxy F5 BIG-IP load balancer http proxy
443/tcp  open     ssl/http   Apache httpd 2.4.58 ((Unix))
8080/tcp open     http-proxy F5 BIG-IP load balancer http proxy
Service Info: Device: load balancer

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 19.56 seconds
```

Run operating system detection with:

``` bat
nmap -O demo.owasp-juice.shop
```

This returns:

``` text
Starting Nmap 7.94 ( https://nmap.org ) at 2024-04-08 20:34 Eastern Daylight Time
Nmap scan report for demo.owasp-juice.shop (81.169.145.156)
Host is up (0.10s latency).
Other addresses for demo.owasp-juice.shop (not scanned): 2a01:238:20a:202:1156::
rDNS record for 81.169.145.156: w9c.rzone.de
Not shown: 995 closed tcp ports (reset)
PORT     STATE    SERVICE
21/tcp   open     ftp
25/tcp   filtered smtp
80/tcp   open     http
443/tcp  open     https
8080/tcp open     http-proxy
Device type: general purpose|load balancer|firewall
Running (JUST GUESSING): OpenBSD 4.X|5.X|6.X|3.X (88%), F5 Networks TMOS 11.6.X|11.4.X (87%), FreeBSD 7.X (85%)
OS CPE: cpe:/o:openbsd:openbsd:4.4 cpe:/o:f5:tmos:11.6 cpe:/o:openbsd:openbsd:5 cpe:/o:openbsd:openbsd:6 cpe:/o:f5:tmos:11.4 cpe:/o:openbsd:openbsd:3 cpe:/o:freebsd:freebsd:7.0
Aggressive OS guesses: OpenBSD 4.4 - 4.5 (88%), F5 BIG-IP Local Traffic Manager load balancer (TMOS 11.6) (87%), OpenBSD 5.0 - 5.8 (87%), OpenBSD 6.0 - 6.4 (87%), OpenBSD 4.0 (87%), OpenBSD 4.3 (86%), OpenBSD 5.0 (86%), OpenBSD 4.7 (86%), OpenBSD 4.1 (86%), OpenBSD 4.6 (85%)
No exact OS matches for host (test conditions non-ideal).
Network Distance: 14 hops

OS detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 11.93 seconds
```

Take a moment to explore the [nmap reference guide](https://nmap.org/book/man.html) and run additional commands and also explore your localhost.
