namespace CodeBotUnity

module EditWindow =

  open System.IO
  open UnityEngine
  open UnityEditor
  open Types
  open State
  open Generator
    
  let requestImportFile state =
    EditorGUILayout.TextField ("Import File Path", state.EditorState.ImportFile)
    |> updateImportFile
    <| state
    
  let requestImportAction state =
    GUILayout.Button("Import") |> function
      | true ->
        File.ReadAllText (state.EditorState.ImportFile)
        |> updateSourceText
        <| state
      | false -> state
  
  let requestSourceText state =
    state
    |> (
      EditorGUILayout.BeginScrollView (state.EditorState.ScrollState)
      |> updateScrollState
    )
    |> (
      EditorGUILayout.TextArea (state.EditorState.SourceText, GUILayout.Height(500.0f))
      |> updateSourceText
    )
    |> fun st ->
      EditorGUILayout.EndScrollView ()
      st
      
  let requestExportSourceFile state =
    EditorGUILayout.TextField ("Save Source File Path", state.EditorState.SourceFile)
    |> updateSourceFile
    <| state
    
  let requestSaveSourceFileAction state =
    GUILayout.Button("Save Source File") |> function
      | true ->
        File.WriteAllText (state.EditorState.SourceFile, state.EditorState.SourceText)
        state
      | false -> state
  
  let requestGenerateAction state =
    GUILayout.Button("Generate") |> function
      | true ->
        (state.EditorState.InputLanguage
        |> languageToExtension
        |> fun ext -> Application.dataPath + "/Plugins/CodeBotUnity/Editor/_cbot_tmp" + ext
        |> updateTemporarySourceFile <| state)
        |> fun st ->
          File.WriteAllText (st.EditorState.TemporarySourceFile, st.EditorState.SourceText)
          generate st
          st
      | false -> state

  let requestInputLanguage state =
    EditorGUILayout.EnumPopup ("Input language", languageToLanguageEnum state.EditorState.InputLanguage)
    :?> LanguageEnum
    |> languageEnumToLanguage
    |> updateInputLanguage
    <| state
    
  let requestOutputLanguage state =
    EditorGUILayout.EnumPopup ("Output language", languageToLanguageEnum state.EditorState.OutputLanguage)
    :?> LanguageEnum
    |> languageEnumToLanguage
    |> updateLanguage
    <| state
     
  let beginHorizontal state =
    EditorGUILayout.BeginHorizontal () |> ignore
    state
    
  let endHorizontal state =
    EditorGUILayout.EndHorizontal ()
    state
    
  type Edit () =
    inherit EditorWindow ()
    member w.OnGUI () =
      loadState ()
      |> beginHorizontal
      |> requestImportFile
      |> requestImportAction
      |> endHorizontal
      |> requestSourceText
      |> beginHorizontal
      |> requestExportSourceFile
      |> requestSaveSourceFileAction
      |> endHorizontal
      |> beginHorizontal
      |> requestInputLanguage
      |> requestOutputLanguage
      |> endHorizontal
      |> requestGenerateAction
      |> saveState
      
  [<MenuItem("CodeBot/Edit")>]
  let ShowSettings () =
    EditorWindow.GetWindow<Edit> (false, "Edit", true)