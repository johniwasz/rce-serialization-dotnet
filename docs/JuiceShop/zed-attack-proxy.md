# ~~OWASP~~ Zed Attack Proxy (ZAP)

Formerly, ZAP was owned and maintained by OWASP. It has since become part of the [Software Security Project](https://softwaresecurityproject.org/).

[ZAP Funding and the Open Source Fellowship](https://www.zaproxy.org/blog/2024-03-13-zap-funding-and-the-open-source-fellowship/)

1. If the Java Runtime environment is not already installed. Download it from [https://www.java.com/en/download/](https://www.java.com/en/download/). If Maltego is already installed, then it is already available. See the Maltego Java Config Windows desktop application for the location of the JRE used by Maltego.

1. Download the Windows 64-bit OWASP ZAP installer at [https://www.zaproxy.org/download/](https://www.zaproxy.org/download/).

1. Open Chrome or your preferred browser.

1. Add _FoxyProxy Basic_ browser add-in for Chrome or your preferred browser.

1. Launch _FoxyProxy Basic_.

    ![Launch FoxyProxy](./images/FoxyProxy00.png "FoxyProxy extension")

1. Select Options.

    ![Select Options](./images/FoxyProxy01.png "Select Options")

1. Add a proxy with the following settings and click _Save_.  
  
    | Setting  | Value | Description |
    | --- | --- | --- |
    | Title | ZAP | Name of the proxy |
    | Hostname | 127.0.0.1 | Local host |
    | Port | 8081 | Proxy for ZAP |

    ![Add Proxy](./images/FoxyProxy02.png "Add Proxy")

1. Chrome bypasses the proxy when navigating to `localhost`. To avoid this, navigate to the Options tab and add `<-loopback>` to the Global Exclude text box and Save.

    ![Include Loopback](./images/FoxyProxy05.png "Include Loopback")

1. Enable the ZAP Proxy. Browsing any non-local site will result in a "No Internet" connection error until ZAP is started and configured for port 8081. Select _Disable_ when done with ZAP.

    ![Enable Proxy](./images/FoxyProxy04.png "Enable Proxy")

1. Open Zap and navigate to Tools | Options. In the list of options, select  Network | Local Servers/Proxies. Change the port to 8081.

    ![Port 8081](./images/ZAP00.png "Port 8081")

1. Navigate to Network | Server Certificates. Click _View_.

    ![View Certificate](./images/ZAP01.png "View Certificate")

1. Click _Install Certificate_.

    ![Install Certificate](./images/ZAP02.png "Install Certificate")

1. Leave _Current User_ selected and click _Next_.

1. Select _Place all certificates in the following store_, click _Browse_ and select the _Trusted Certificate Authorities_ store. Click _Next_.

    ![Store Certificate](./images/BurpCert02.png "Store Certificate")

1. Click _Finish_. The browser using Foxy Proxy may need to be restarted for the certificate to take effect.

1. Navigate to [http://zap/](http://zap/) on the configured browser. This documentation page is a launching point into tutorials and background about the tool.

1. In ZAP, select Manage Add-Ons in the tool bar.

    ![Install Add-Ons](./images/ZAP03.png "Install Add-Ons")

1. Navigate to the Marketplace tab. Select and install the following Add-Ons:

    - FuzzDb
    - SVN Digger Files
