# Introduction

In this exercise, we'll scan the OWASP Juice Shop located at:

[https://demo.owasp-juice.shop](https://demo.owasp-juice.shop)

This uses nikto to walk the hosting site and discover potential vulnerabilities.

1. Open a DOS or Powershell command prompt and use:

    ``` bat
    wsl
    ```

1. Install nikto

    ``` bash
    sudo apt install nikto
    ```

1. Run nikto against the running OWASP Juice Shop server.

    ``` bash
    nikto -h https://demo.owasp-juice.shop
    ```

    Or

    ``` bash
    nikto -h https://localhost:88
    ```

1. Note the results and potential exploits.

    The following is returned from a local scan:

    ``` txt
    - Nikto v2.1.5
    ---------------------------------------------------------------------------
    + Target IP:          127.0.0.1
    + Target Hostname:    localhost
    + Target Port:        88
    + Start Time:         2024-03-23 12:24:28 (GMT-4)
    ---------------------------------------------------------------------------
    + Server: No banner retrieved
    + Server leaks inodes via ETags, header found with file /, fields: 0xW/ea4 0x18e4f07a9a9
    + Uncommon header 'x-frame-options' found, with contents: SAMEORIGIN
    + Uncommon header 'feature-policy' found, with contents: payment 'self'
    + Uncommon header 'x-recruiting' found, with contents: /#/jobs
    + Uncommon header 'x-content-type-options' found, with contents: nosniff
    + Uncommon header 'access-control-allow-origin' found, with contents: *
    + No CGI Directories found (use '-C all' to force check all possible dirs)
    + File/dir '/ftp/' in robots.txt returned a non-forbidden or redirect HTTP code (200)
    + "robots.txt" contains 1 entry which should be manually viewed.
    + Uncommon header 'access-control-allow-methods' found, with contents: GET,HEAD,PUT,PATCH,POST,DELETE
    + OSVDB-3092: /css: This might be interesting...
    + OSVDB-3092: /ftp/: This might be interesting...
    + OSVDB-3092: /public/: This might be interesting...
    + 6544 items checked: 2 error(s) and 12 item(s) reported on remote host
    + End Time:           2024-03-23 12:25:21 (GMT-4) (53 seconds)
    ---------------------------------------------------------------------------
    + 1 host(s) tested
    ```
