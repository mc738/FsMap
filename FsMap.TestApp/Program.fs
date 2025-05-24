open System.IO
open System.Reflection
open FsMap.V1.Core.Attributes
open FsMap.V1.Core.Models
open FsMap.V1.Generated.Json
open FsMap.V1.Generated.Json.Schema

module GenTest =

    open FsMap.V1.Generated.CodeGeneration.Common

    let members =
        [ ModuleMember.Record
              { Namespace = None
                Name = "Bar"
                Attributes = []
                Fields =
                  [ { Name = "Value1"
                      FieldType = FieldType.BaseType BaseType.String
                      Attributes = []
                      Optional = false }
                    { Name = "Value2"
                      FieldType = FieldType.BaseType BaseType.Int32
                      Attributes = []
                      Optional = true }
                    { Name = "Value3"
                      FieldType = FieldType.BaseType BaseType.DateTime
                      Attributes = []
                      Optional = true } ] }
          ModuleMember.Record
              { Namespace = None
                Name = "Foo"
                Attributes = []
                Fields =
                  [ { Name = "Bar"
                      FieldType = FieldType.Reference "Bar"
                      Attributes = []
                      Optional = false } ] } ]

    let run () =

        generated "MyModule" members
        |> fun r -> File.WriteAllText("C:\\Users\\mclif\\Projects\\dotnet\\FsMap\\FsMap.TestApp\\Test.fs", r)


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

module JsonSchemaTest =

    open FsMap.V1.Generated.CodeGeneration.Common

    let example1Path =
        "C:\\Users\\mclif\\Projects\\data\\json_schema\\examples\\example1.json"


    let run _ =
        let schema = Schema.readSchemaFile example1Path

        buildModels schema
        |> Result.map ModuleMember.Record
        |> Result.iter (fun mm ->
            generated "Examples" [ mm ]
            |> fun r -> File.WriteAllText("C:\\Users\\mclif\\Projects\\dotnet\\FsMap\\FsMap.TestApp\\Examples.fs", r))

        ()

module Test =

    let run () =
        let t = typeof<Foo>

        let props = t.GetProperties()

        let ``has default attribute`` (pi: PropertyInfo) =
            pi.GetCustomAttribute<DefaultAttribute>() |> Option.ofObj |> Option.isSome

        let defaultProperty =
            t.GetProperties(BindingFlags.Static ||| BindingFlags.Public)
            |> Array.tryFind ``has default attribute``

        let dv = defaultProperty |> Option.map (fun dp -> dp.GetValue(null))

        let j =
            Schema.readSchemaFile "C:\\Users\\mclif\\Projects\\data\\json_schema\\examples\\example1.json"

        ()

GenTest.run ()

JsonSchemaTest.run ()

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
