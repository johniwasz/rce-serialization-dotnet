# Install Docker and Enable WSL

Install docker and docker compose using:

``` bat
winget install "docker cli"
winget install "docker compose"
```

Optionally, install Docker Desktop. Companies with an excess of 250 employees or $10m in revenue are required to pay a subscription fee. Other conditional apply. At the time of this writing, it is free for personal use. For more information, please see [Docker Desktop Licensing](https://docs.docker.com/subscription/desktop-license/).

``` bat
winget install "docker desktop"
```

Other alternative desktop management options:

[Minikube](https://minikube.sigs.k8s.io/docs/)  
[Rancher Desktop](https://rancherdesktop.io/)  

Additional installable docker packages can be found using:

``` bat
winget search docker
```
