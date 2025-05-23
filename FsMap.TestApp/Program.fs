open System.Reflection
open FsMap.V1.Core.Attributes
open FsMap.V1.Generated.Json

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
    t.GetProperties(BindingFlags.Static ||| BindingFlags.Public)
    |> Array.tryFind ``has default attribute``

let dv = defaultProperty |> Option.map (fun dp -> dp.GetValue(null))

let j = Schema.readSchemaFile "C:\\Users\\mclif\Projects\\data\\json_schema\\github\\pr_response.json"

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
