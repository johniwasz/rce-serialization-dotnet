# Juice Shop

Juice Shop is an intentionally vulnerable web site supported and maintained by [OWASP](owasp.org). It's available publicly at:

https://demo.owasp-juice.shop/

Most exercises can be completed against the [Juice Shop](https://demo.owasp-juice.shop/). More invasive exploit challenges require a local installation.

## Installing Linux

[WSL Install](wsl-install.md)

## Installing Juice Shop Locally

Running Juice Shop locally requires docker and Windows Subsystem for Linux.

[Docker Install](docker-install.md)  

To install Juice Shop a local docker container, see [Juice Shop Install](JuiceShopInstall.md) which creates a local running instance at http://localhost:88 hosted on docker on Ubuntu.

## Reconnaissance 

Intelligence can be gathered from public APIs and websites using public information. This is typically referred to as [OSINT](https://osintframework.com/). 


| Tool | Description |
| ---  | ----------  |
| [amass](OSINT/amass.md) | Find registered subdomains |
| [nikto](OSINT/nikto.md) | Find vulnerable headers and directories |
| [Kiterunner](OSINT/Kiterunner.md) | Find vulnerable routes |
| [securityheaders.com](OSINT/securityheaders.md) | Validate security headers |

## Penetration Testing

Penetration testing can be performed manually; however, tools ease the effort. These exercises use ZAP and Burp Suite Community Edition

Zed Attack Proxy is also used. Please follow these instructions:

[Zed Attack Proxy Installation](./zed-attack-proxy.md)

Please follow the instructions to [Install and Configure Burp Suite Community Edition](Burp-Suite-install.md).

