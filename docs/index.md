# Introduction

This repo explores .NET Framework and .NET serialization vulnerabilities. This documentation also includes instructions for configuring vulnerable API test environments.

[Serialization Vulnerabilities](SerializationVulnerabilities.md)

## API Test Environments

Vulerable API, GraphQL, and Website hosts can be used to build an vulnerability testing environment. OWASP maintains a list of vulnerable test projects at [OWASP Vulnerable Web Applications Directory](https://owasp.org/www-project-vulnerable-web-applications-directory/).

One of the most popular and maintained externally available vulnerable sites are:

| Site   |  Owner | Technology |
| -------- | ------- | ------- | 
| [Gin and Juice Shop](https://ginandjuice.shop/)  | PortSwigger  | 
| [OWASP Juice Shop](https://juice-shop.herokuapp.com/#/)  | OWASP  |
| March    | $420    |

### Prerequisites

Install docker and docker compose using:

```
winget install "docker cli"
winget install "docker compose"
```
Optionally, install Docker Desktop. Companies with an excess of 250 employees or $10m in revenue are required to pay a subscription fee. Other conditional apply. At the time of this writing, it is free for personal use. For more information, please see [Docker Desktop Licensing](https://docs.docker.com/subscription/desktop-license/).
```
winget install "docker desktop"
```

Other alternative desktop management options:

[Minikube](https://minikube.sigs.k8s.io/docs/)  
[Rancher Desktop](https://rancherdesktop.io/)  

Additional installable docker packages can be found using:

```
 winget search docker
 ```

Setting up an environment on Windows requires enabling [Windows Subsystem for Linux](https://learn.microsoft.com/en-us/windows/wsl/) and launch it.

```
wsl --install
```
There may be multiple distributions installed. For these exercises we will use the default Ubuntu distribution. To check distributions use:
```
wsl -v
```
If Ubuntu is not the default, then use:
```
wsl --set-default Ubuntu
```

If docker reports that the service is not started when executing docker commands, use the following to start the docker service:

```
sudo systemctl restart snap.docker.dockerd.service
```

## Install and Run the OWASP Launch Juice Shop

Launch Windows Subsystem for Linux, install the container, and launch the Juice Shop.

1. Open an Ubuntu shell from a DOS or Powershell terminal:

```
wsl
```
2. Install the OWASP Juice Shop container From the Ubuntu command shell:

``` bash
sudo docker pull bkimminich/juice-shop
sudo docker run --rm -p 88:3000 bkimminich/juice-shop
```

3. Launch a browser and navigate to `http://localhost:88`.

## Install and Run the crAPI

Use these steps to install and run the [Completely Ridiculous API](https://github.com/OWASP/crAPI). This surfaces the [OWASP Top 10 API Security Risks](https://owasp.org/API-Security/editions/2023/en/0x11-t10/).  

1. Open an Ubuntu shell from a DOS or Powershell terminal:

```
wsl
```
2. Install the crAPI docker containers from an Ubuntu terminal: 

``` bash
curl -o docker-compose.yml https://raw.githubusercontent.com/OWASP/crAPI/main/deploy/docker/docker-compose.yml

sudo docker-compose pull
```
3. Launch the crAPI docker containers
```
sudo docker-compose -f docker-compose.yml --compatibility up -d
```
## Install and Run the Damn Vulnerable GraphQL

GraphQL is increasing in popularity and is in use at Facebook, Netflix, IBM, AWS, and Azure. 

1. Open an Ubuntu shell from a DOS or Powershell terminal:

```
wsl
```
2. Install the Damn Vulnerable GraphQL container:
```
sudo docker pull dolevf/dvga
```
