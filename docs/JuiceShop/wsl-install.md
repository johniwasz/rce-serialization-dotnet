# Windows Subsystem for Linux Install

Setting up an environment on Windows requires enabling [Windows Subsystem for Linux](https://learn.microsoft.com/en-us/windows/wsl/?WT.mc_id=MVP_337682) and launch it.

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

## Validating Docker Install

If docker reports that the service is not started when executing docker commands in Ubuntu, like listing all containers:

``` bash
sudo docker ps -a
```

Then, use the following to start the docker service:

``` bash
sudo systemctl restart snap.docker.dockerd.service
```