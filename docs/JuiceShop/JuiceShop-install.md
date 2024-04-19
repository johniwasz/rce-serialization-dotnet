# Install and Run the OWASP Launch Juice Shop

Launch Windows Subsystem for Linux, install the container, and launch the Juice Shop.

1. Open an Ubuntu shell from a Windows Command or Powershell terminal:

    ``` powershell
    wsl
    ```

    If you need to install Windows Subsystem for Linux, please see [WSL Install](wsl-install.md).

1. Install the OWASP Juice Shop container From the Ubuntu command shell:

    ``` bash
    sudo docker pull bkimminich/juice-shop
    sudo docker run --rm -p 88:3000 bkimminich/juice-shop
    ```

    If you need to install Docker, see [Docker Install](./docker-install.md).

1. Launch a browser and navigate to `http://localhost:88`.
