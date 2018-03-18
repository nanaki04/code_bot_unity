namespace CodeBotUnity

module SettingsWindow =

  open UnityEngine
  open UnityEditor
  open Types
  open State
  
  let requestEscriptPath state =
    EditorGUILayout.TextField ("Escript Path", state.Settings.EscriptPath)
    |> updateEscriptPath
    <| state
    
  let requestOutputLanguage state =
    EditorGUILayout.EnumPopup ("Output language", languageToLanguageEnum state.EditorState.OutputLanguage)
    :?> LanguageEnum
    |> languageEnumToLanguage
    |> updateLanguage
    <| state

  type Settings () =
    inherit EditorWindow ()
    member w.OnGUI () =
      loadState ()
      |> requestEscriptPath
      |> requestOutputLanguage
      |> saveState
      
  [<MenuItem("CodeBot/Settings")>]
  let ShowSettings () =
    EditorWindow.GetWindow<Settings> (false, "Settings", true)