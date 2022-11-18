# Dictionary API
- [Dictionary API](#dictionary-api)
    - [Get Word](#get-word)
        - [Get Word Request](#get-word-request)
        - [Get Word Response](#get-word-response)
    - [Create Word](#create-word)
        - [Create Word Request](#create-word-request)
        - [Create Word Response](#create-word-response)


## Get Word

### Get Word Request
```js
GET /word/{{id}}
```

### Get Word Response

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
POST /Word
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
Location: {{host}}/Breakfasts/{{id}}
````
`````json
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
