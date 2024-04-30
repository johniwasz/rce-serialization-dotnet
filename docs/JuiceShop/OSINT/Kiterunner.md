# Kiterunner

Use [Kiterunner](https://github.com/assetnote/kiterunner) to find API endpoints. Kiterunner needs to be built locally and uses go. Kiterunner is in wide use for OSINT API reconnaissance, but has not be updated in three years.

1. Use a DOS or Powershell terminal to launch a Linux terminal:

    ``` bat
    wsl
    ```

1. Install prerequisites.

    ``` bash
    sudo apt install make
    sudo apt install golang-go
    ```

1. Get and build Kiterunner.

    ``` bash
    git clone https://github.com/assetnote/kiterunner.git
    cd kiterunner
    make build
    ```

1. Create an alias (kr) for Kiterunner.

    ``` bash
    sudo ln -s $(pwd)/dist/kr /usr/local/bin/kr
    ```

1. Review wordlists for use by Kiterunner to find common api routes.

    ``` bash
    kr wordlist list
    ```

1. Run a scan against a running OWASP Juice Shop. This command uses the first 20,000 words in the apiroutes-240128 wordlist. It uses ten concurrent requests per host; the default is 3.

    ``` bash
    kr scan https://demo.owasp-juice.shop -A=apiroutes-240428:20000 -x 10 --ignore-length=34 --fail-status-codes 404
    ```

1. Run the scan against the local OWASP Juice Shop.

    ``` bash
    kr scan https://localhost:88 -A=apiroutes-240428:20000 -x 10 --ignore-length=34 --fail-status-codes 404
    ```

1. Optional. Replay a request. Replace the text in the quotes with the output of a 200 response from the prior run. NOTE: There may be a bug which prevents successful completion.

    ``` bash
    go run ./cmd/kiterunner kb replay "GET     200 [  80220, 3235,   1] https://demo.owasp-juice.shop/api/challenges 2dpmnJyrnny32octfJuK7zz3n7l"
    ```
