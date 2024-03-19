# Install and Run the OWASP Launch Juice Shop

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