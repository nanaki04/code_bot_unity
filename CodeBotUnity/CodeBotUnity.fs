namespace CodeBotUnity

module CodeBotUnityMain =

  open UnityEditor
  open State
  open Generator

  [<MenuItem("CodeBot/Generate")>]
  let generateFromCurrentState () =
    loadState ()
    |> generate
