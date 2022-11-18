# Dictionary Models

## Word

* Id : long
* Name : string
* Defitinions : List<.Definition>


## Definition

* Id : long
* Word : Word
* WordId: long
* Definition : string
* DefinitionType : string
* ExampleSentence : string

Word owns many definitions which own one word.