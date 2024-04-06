# Amass

Amass is an OWASP tool that can be used for both active and passive reconnaissance. This can be installed on Ubuntu by:

1. From a DOS or Powershell terminal:

    ``` bat
    wsl
    ```

1. Install amass at an Ubuntu command prompt:

    ``` bash
    sudo snap install amass
    ```

1. Download the default _config.ini_ file and save to the default Amass config file location.

    ``` bash
    mkdir -p ~/.config/amass
    curl https://raw.githubusercontent.com/OWASP/Amass/master/examples/datasources.yaml >$HOME/.config/amass/datasources.yaml
    ```

1. Perform dns passive reconnaissance. This may take a few minutes to return.

    ``` bash
    amass enum --passive -d owasp-juice.shop
    ```
