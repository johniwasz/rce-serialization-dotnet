# Juice Shop

Juice Shop is an intentionally vulnerable web site supported and maintained by [OWASP](owasp.org). It's available publicly at:

https://demo.owasp-juice.shop/

Most exercises can be completed against the [Juice Shop](https://demo.owasp-juice.shop/). More invasive exploit challenges require a local installation.

## Installing Linux

[WSL Install](wsl-install.md)

## Installing Juice Shop Locally

Running Juice Shop locally requires docker and Windows Subsystem for Linux.

[Docker Install](docker-install.md)  

To install Juice Shop a local docker container, see [Juice Shop Install](JuiceShopInstall.md) which creates a local running instance at https://localhost:88 and http://localhost:88 hosted on docker on Ubuntu.

## Reconnaissance 

Intelligence can be gathered from public APIs and websites using public information. This is typically referred to as [OSINT](https://osintframework.com/). 


| Tool | Description |
| ---  | ----------  |
| [nmap](OSINT/nmap.md) | network scanning |
| [amass](OSINT/amass.md) | Find registered subdomains |
| [nikto](OSINT/nikto.md) | Find vulnerable headers and directories |
| [Kiterunner](OSINT/Kiterunner.md) | Find vulnerable routes |
| [securityheaders.com](OSINT/securityheaders.md) | Validate security headers |

## Penetration Testing

Penetration testing can be performed manually; however, tools ease the effort. These exercises use ZAP and Burp Suite Community Edition

Zed Attack Proxy is used for these exercises. Please follow these instructions to install and configure ZAP:

[Zed Attack Proxy Installation](./zed-attack-proxy.md)

Burp Suite is a common tool used by professional penetration testers. A free version is available and can be used for these exercises as well.
Please follow the instructions to [Install and Configure Burp Suite Community Edition](Burp-Suite-install.md).

These exercises can use the public [Juice Shop](https://demo.owasp-juice.shop/) site or the local docker instance. The local instance is preferred as other users may compromise or take down the public site.

### CSP Header Vulnerability

In the Architecture overview you were told that the Juice Shop uses a modern Single Page Application frontend. That was not entirely true.

- Find a screen in the application that looks subtly odd and dated compared with all other screens
- Before trying any XSS attacks, you should understand how the page is setting its Content Security Policy
- For the subsequent XSS, make good use of the flaws in the homegrown sanitization based on a RegEx!

[CSP Header Vulnerability solution](./solutions/override-csp-header.md)

### SQL Injection

Use ZAP or browser network inspection to find an endpoint that is vulnerable to SQL injection. Compromise the endpoint to exfiltrate user data.

[SQL Injection solution](./solutions/JuiceShop-sqlinjection.md)

### Password Cracking

The SQL Injection exercise exposed details about user accounts. Use the exposed information to log as the `admin@juice-sh.op` user.

[Cracking Passwords solution](./solutions/cracking-passwords.md)

### Mass Assignment

Mass assignment occurs when a property of a JSON or request payload is available in an unintended security context.

Review the requests and responses that are generate when creating a user. There may be a mass assignment vulnerability that allows a new user to elevate their permissions.

[Mass Assignment solution](./solutions/mass-assignment.md)