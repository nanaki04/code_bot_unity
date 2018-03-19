namespace CodeBotUnity

open UnityEngine

type Path = string

type Language =
| CSharp
| Unity
| Php
| PlantUml
| EzScript

type LanguageEnum =
| CSharp = 0
| Unity = 1
| Php = 2
| PlantUml = 3
| EzScript = 4

type Settings = {
  EscriptPath : Path;
  CodeBotUnityRoot : Path;
}

type EditorState = {
  SourceFile : Path;
  ImportFile : Path;
  TemporarySourceFile : Path;
  SourceText : string;
  ScrollState : Vector2;
  OutputLanguage : Language;
  InputLanguage : Language;
}

type MainState = {
  Settings : Settings;
  EditorState : EditorState
}
  
module Types =  
  
  let languageToString language =
    match language with
    | CSharp -> "c#"
    | Unity -> "u#"
    | Php -> "php"
    | PlantUml -> "uml"
    | EzScript -> "ez"
    
  let languageToExtension language =
    match language with
    | CSharp -> ".cs"
    | Unity -> ".cs"
    | Php -> ".php"
    | PlantUml -> ".uml"
    | EzScript -> ".ez"
    
  let languageEnumToLanguage languageEnum =
    match languageEnum with
    | LanguageEnum.CSharp -> CSharp
    | LanguageEnum.Unity -> Unity
    | LanguageEnum.Php -> Php
    | LanguageEnum.PlantUml -> PlantUml
    | LanguageEnum.EzScript -> EzScript
    | _ -> CSharp
    
  let languageToLanguageEnum language =
    match language with
    | CSharp -> LanguageEnum.CSharp
    | Unity -> LanguageEnum.Unity
    | Php -> LanguageEnum.Php
    | PlantUml -> LanguageEnum.PlantUml
    | EzScript -> LanguageEnum.EzScript