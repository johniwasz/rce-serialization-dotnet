# Introduction

In this exercise, we'll scan the OWASP Juice Shop located at:

[https://demo.owasp-juice.shop](https://demo.owasp-juice.shop)

This uses nikto to walk the hosting site and discover potential vulnerabilities.

1. Open a DOS or Powershell command prompt and use:
```
wsl
```
2. Install nikto
```
sudo apt install nikto
```
3. Run nikto against the running OWASP Juice Shop server.
```
nikto -h http://localhost:88
```
4. Note the results and potential exploits.

