# Introduction

This repo explores .NET Framework and .NET serialization vulnerabilities. This documentation also includes instructions for configuring vulnerable API test environments.

[Serialization Vulnerabilities](serialization/serialization.md)

[Juice Shop Vulnerabilities](JuiceShop/JuiceShop.md)

Serialization vulnerabilities and this documentation is maintained on Github at:

[rce-serialization-dotnet](https://github.com/johniwasz/rce-serialization-dotnet)

## API Test Environments

Vulerable API, GraphQL, and Website hosts can be used to build an vulnerability testing environment. OWASP maintains a list of vulnerable test projects at [OWASP Vulnerable Web Applications Directory](https://owasp.org/www-project-vulnerable-web-applications-directory/).

One of the most popular and maintained externally available vulnerable sites are:

| Site   |  Owner |
| -------- | ------- | 
| [Gin and Juice Shop](https://ginandjuice.shop/)  | PortSwigger  | 
| [OWASP Juice Shop](https://juice-shop.herokuapp.com/#/)  | OWASP  |

## Other Vulnerable APIs and Sites

### Install and Run the crAPI

Use these steps to install and run the [Completely Ridiculous API](https://github.com/OWASP/crAPI). This surfaces the [OWASP Top 10 API Security Risks](https://owasp.org/API-Security/editions/2023/en/0x11-t10/).  

1. Open an Ubuntu shell from a DOS or Powershell terminal:
    ```
    wsl
    ```
1. Install the crAPI docker containers from an Ubuntu terminal: 
    ``` bash
    curl -o docker-compose.yml https://raw.githubusercontent.com/OWASP/crAPI/main/deploy/docker/docker-compose.yml

    sudo docker-compose pull
    ```
1. Launch the crAPI docker containers
    ``` bash
    sudo docker-compose -f docker-compose.yml --compatibility up -d
    ```
### Install and Run the Damn Vulnerable GraphQL

GraphQL is increasing in popularity and is in use at Facebook, Netflix, IBM, AWS, and Azure. 

1. Open an Ubuntu shell from a DOS or Powershell terminal:
    ```
    wsl
    ```
1. Install the Damn Vulnerable GraphQL container:
    ``` bash
    sudo docker pull dolevf/dvga
    ```
1. Launch the Damn Vulnerable GraphQL container:
    ``` bash
    sudo docker run -t -p 5013:5013 -e WEB_HOST=0.0.0.0 dolevf/dvga
    ```

