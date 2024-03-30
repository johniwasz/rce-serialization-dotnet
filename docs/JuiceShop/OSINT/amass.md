# Amass

Amass is an OWASP tool that can be used for both active and passive reconaissance. This can be installed on Ubuntu by:

1. From a DOS or Powershell terminal:
```
wsl
```
2. Install amass at an Ubuntu command prompt:
```
sudo snap install amass
```
3. Download the default _config.ini_ file and save to the default Amass config file location.
```
mkdir -p ~/.config/amass
curl https://raw.githubusercontent.com/OWASP/Amass/master/examples/datasources.yaml >$HOME/.config/amass/datasources.yaml
```
4. Perform dns passive reconaissance. This may take a few minutes to return.
```
amass enum --passive -d owasp-juice.shop
```
