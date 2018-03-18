namespace CodeBotUnity

module Generator =

  open UnityEngine
  open UnityEditor
  open System.Diagnostics
  open Types
  open State
  
  let startInfo fileName args =
    ProcessStartInfo(
      UseShellExecute = false,
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      WorkingDirectory = Application.dataPath,
      FileName = fileName,
      Arguments = args
    )
    
  let cbotPath () =
    Application.dataPath + "/Editor/cbot"
    
  let formatArgs file language =
    cbotPath () + " " + file + " " + (languageToString language)
 
  let logOutput (prcess : Process) =
    prcess.StandardOutput.ReadToEnd() |> Debug.Log
    prcess.StandardError.ReadToEnd()
    |> function
      | "" -> ()
      | error -> Debug.LogError error
 
  let generate {Settings = settings; EditorState = editorState} =
    let {EscriptPath = escriptPath} = settings
    let {OutputLanguage = language; TemporarySourceFile = file} = editorState
    formatArgs file language
    |> startInfo escriptPath
    |> Process.Start
    |> logOutput
