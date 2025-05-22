open System.Reflection
open FsMap.V1.Core.Attributes

type Foo =
    { Value: string }

    [<Default>]
    static member Default = { Value = "Test" }

type Bar =
    { [<MapFrom(typeof<Foo>, "Value")>]
      Name: string
      Value: int }

    [<Default>]
    static member Default = { Name = ""; Value = 42 }


let t = typeof<Foo>

let props = t.GetProperties()

let ``has default attribute`` (pi: PropertyInfo) =
    pi.GetCustomAttribute<DefaultAttribute>() |> Option.ofObj |> Option.isSome

let defaultProperty =
    props
    |> Array.tryFind (fun pi ->
        ``has default attribute`` pi //&& (pi :> MemberInfo).
        
        )

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
