# Dictionary API
- [Dictionary API](#dictionary-api)
    - [Get Word](#get-word)
        - [Get Word By Id Request](#get-word-by-id-request)
        - [Get Word By Id Response](#get-word-by-id-response)
        - [Get Word By Name Request](#get-word-by-name-request)
        - [Get Word By Name Response](#get-word-by-name-response)
    - [Create Word](#create-word)
        - [Create Word Request](#create-word-request)
        - [Create Word Response](#create-word-response)
    - [Update Word](#update-word)
        - [Update Word Request](#update-word-request)
    - [Delete Word](#delete-word)
        - [Delete Word By Id Request](#delete-word-by-id-request)
        - [Delete Word By Id Response](#delete-word-by-id-response)
        - [Delete Word By Name Request](#delete-word-by-name-request)
        - [Delete Word By Name Response](#delete-word-by-name-response)


## Get Word

### Get Word By Id Request
```js
GET /Dictionary/Word/{id}
```

### Get Word By Id Response

```js
200 Ok
```

````json
{
    "Id" : 1,
    "Name": "Sayrıl",
    "Definitions": [
        {
            "Id":1,
            "Definition": "Hastalıklı, marazi",
            "DefinitionType":"sıfat, eskimiş",
            "ExampleSentence": ""
        }
    ]
}
````
### Get Word By Name Request
```js
GET /Dictionary/Word/{wordName}
```

### Get Word By Name Response

```js
200 Ok
```

````json
{
    "Id" : 1,
    "Name": "Sayrıl",
    "Definitions": [
        {
            "Id":1,
            "Definition": "Hastalıklı, marazi",
            "DefinitionType":"sıfat, eskimiş",
            "ExampleSentence": ""
        }
    ]
}
````

## Create Word

### Create Word Request
````js
POST /Dictionary/CreateWord
````
````json
{
    "Name": "...",
    "Definitions": [
        {
            "Definition": "...",
            "DefinitionType":"...",
            "ExampleSentence": "..."
        }
    ]
}
````
### Create Word Response
````js
201 Created
````

````js
Location: {host}/Dictionary/Word/{id}
````
````json
{
    "Name": "...",
    "Definitions": [
        {
            "Definition": "...",
            "DefinitionType":"...",
            "ExampleSentence": "..."
        }
    ]
}
````

## Update Word

### Update Word Request
````js
PUT /Dictionary/Word/{wordName}
````
### Update Word Response

````js
200 Ok
{
  "message": "Succesfully Updated"
}
````

````js
400 Bad Request
{
  "message": "The word be updated couldn't be found."
}
````

## Delete Word

### Delete Word By Id Request
````js
DELETE /Dictionary/Word/{id:long}
````
### Delete Word By Id Response

````js
200 Ok
{
  "message": "Succesfully Deleted"
}
````

````js
400 Bad Request
{
  "message": "The word to be deleted couldn't be found."
}
````

### Delete Word By Name Request
````js
DELETE /Dictionary/Word/{wordName}
````
### Delete Word By Name Response

````js
200 Ok
{
  "message": "Succesfully Deleted"
}
````

````js
400 Bad Request
{
  "message": "The word be deleted couldn't be found."
}
````