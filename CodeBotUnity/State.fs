namespace CodeBotUnity

module State =

  open UnityEngine

  let initialSettings = {
    EscriptPath = "/usr/local/bin/escript";
    CodeBotUnityRoot = "Plugins/CodeBotUnity/Editor/";
  }
  
  let initialEditorState = {
    SourceFile = "Assets/Plugins/CodeBotUnity/Editor/test.ez";
    ImportFile = "Assets/Plugins/CodeBotUnity/Editor/test.ez";
    TemporarySourceFile = "";
    SourceText = "";
    ScrollState = Vector2 (0.0f, 0.0f)
    OutputLanguage = CSharp;
    InputLanguage = PlantUml;
  }
    
  let mutable state = {
    Settings = initialSettings;
    EditorState = initialEditorState;
  }
  
  let loadState () = state
  
  let saveState st = state <- st
  
  let updateSettings settings st =
    { st with Settings = settings }
    
  let updateEditorState editorState st =
    { st with EditorState = editorState }
      
  let updateEscriptPath path st =
    { st.Settings with EscriptPath = path }
    |> updateSettings
    <| st
  
  let updateImportFile importFile st =
    { st.EditorState with ImportFile = importFile }
    |> updateEditorState
    <| st
    
  let updateTemporarySourceFile path st =
    { st.EditorState with TemporarySourceFile = path }
    |> updateEditorState
    <| st
    
  let updateSourceText sourceText st =
    { st.EditorState with SourceText = sourceText }
    |> updateEditorState
    <| st
    
  let updateScrollState scrollState st =
    { st.EditorState with ScrollState = scrollState }
    |> updateEditorState
    <| st 
      
  let updateInputLanguage language st =
    { st.EditorState with InputLanguage = language }
    |> updateEditorState
    <| st
    
  let updateLanguage language st =
    { st.EditorState with OutputLanguage = language }
    |> updateEditorState
    <| st
    
  let updateSourceFile file st =
    { st.EditorState with SourceFile = file }
    |> updateEditorState
    <| st