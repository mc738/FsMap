﻿namespace FsMap.V1.Generated.Json

//#nowarn "1010103001"

open System.IO
open System.Reflection.Metadata
open FsMap.V1.Core
open FsMap.V1.Core.Models
open Microsoft.Json.Schema
open FsToolbox.Extensions.Strings

module Schema =

    [<RequireQualifiedAccess>]
    type JsonType =
        | Array
        | Boolean
        | None
        | Integer
        | Number
        | Null
        | Object
        | String

        static member Get(schemaValue: SchemaType) =
            match schemaValue with
            | SchemaType.Array -> JsonType.Array
            | SchemaType.Boolean -> JsonType.Boolean
            | SchemaType.None -> JsonType.None
            | SchemaType.Integer -> JsonType.Integer
            | SchemaType.Number -> JsonType.Number
            | SchemaType.Null -> JsonType.Null
            | SchemaType.Object -> JsonType.Object
            | SchemaType.String -> JsonType.String
            | _ -> JsonType.None

        member jt.IsScalar() =
            match jt with
            | Array -> false
            | Boolean -> true
            | None -> true
            | Integer -> true
            | Number -> true
            | Null -> true
            | Object -> false
            | String -> true

        [<CompilerMessage("This method will throw for non-scalar types, it is assumed types will be checked with JsonType.IsScalar() first or errors. To suppress this error add #nowarn \"1010103001\"",
                          1010103001)>]
        member jt.GetDotNetScalarType() =
            match jt with
            | Array -> invalidOp "JsonType.Array is not a scalar type"
            | Boolean -> Some typeof<bool>
            | None -> Option.None //typeof<> //failwith "" // typeof<null>
            | Integer -> Some typeof<int32>
            | Number -> Some typeof<float>
            | Null -> failwith "" // typeof<null>
            | Object -> invalidOp "JsonType.Object is not a scalar type"
            | String -> Some typeof<string>

        member jt.ToFieldDefinition() = ()

    [<RequireQualifiedAccess>]
    type BuiltInFormat =
        | DateTime
        | Time
        | Date
        | Duration
        | Email
        | IdnEmail
        | Hostname
        | IdnHostname
        | Ipv4
        | Ipv6
        | Uuid
        | Uri
        | UriReference
        | Iri
        | IriReference
        | UriTemplate
        | JsonPointer
        | RelativeJsonPointer
        | Regex

        static member TryDeserialize(value: string) =
            match value with
            | "date-time" -> Some BuiltInFormat.DateTime
            | "time" -> Some BuiltInFormat.Time
            | "date" -> Some BuiltInFormat.Date
            | "duration" -> Some BuiltInFormat.Duration
            | "email" -> Some BuiltInFormat.Email
            | "idn-email" -> Some BuiltInFormat.IdnEmail
            | "hostname" -> Some BuiltInFormat.Hostname
            | "idn-hostname" -> Some BuiltInFormat.IdnEmail
            | "ipv4" -> Some BuiltInFormat.Ipv4
            | "ipv6" -> Some BuiltInFormat.Ipv6
            | "uuid" -> Some BuiltInFormat.Uuid
            | "uri" -> Some BuiltInFormat.Uri
            | "uri-reference" -> Some BuiltInFormat.UriReference
            | "iri" -> Some BuiltInFormat.Iri
            | "iri-reference" -> Some BuiltInFormat.IriReference
            | "uri-template" -> Some BuiltInFormat.UriTemplate
            | "json-pointer" -> Some BuiltInFormat.JsonPointer
            | "relative-json-pointer" -> Some BuiltInFormat.RelativeJsonPointer
            | "regex" -> Some BuiltInFormat.Regex
            | _ -> None

    let ``get built-in format`` (str: string) = ()

    let rec build (schema: JsonSchema) =

        let fields =
            schema.Properties
            |> Seq.map (fun kv ->
                match kv.Value.Type.Count with
                | 1 ->
                    match kv.Value.Type[0] with
                    | SchemaType.Array -> failwith "todo"
                    | SchemaType.Boolean ->
                        ({ Name = kv.Key.ToPascalCase()
                           FieldType = FieldType.BaseType BaseType.Boolean
                           Attributes = []
                           Optional = false }
                        : Models.FieldDefinition)
                    | SchemaType.None -> failwith "todo"
                    | SchemaType.Integer ->
                        ({ Name = kv.Key.ToPascalCase()
                           FieldType = FieldType.BaseType BaseType.Int32
                           Attributes = []
                           Optional = false }
                        : Models.FieldDefinition)
                    | SchemaType.Number ->
                        ({ Name = kv.Key.ToPascalCase()
                           FieldType = FieldType.BaseType BaseType.Double
                           Attributes = []
                           Optional = false }
                        : Models.FieldDefinition)
                    | SchemaType.Null -> failwith "todo"
                    | SchemaType.Object -> failwith "todo"
                    | SchemaType.String ->
                        //match kv.Value.Format

                        ({ Name = kv.Key.ToPascalCase()
                           FieldType = FieldType.BaseType BaseType.String
                           Attributes = []
                           Optional = false }
                        : Models.FieldDefinition)
                    | _ -> failwith "todo"
                | 0 ->
                    // No type defined...
                    failwith "TODO  - handle"
                | _ ->
                    // If there is more than one valid type,
                    // then any of these types if a valid value.
                    // So this could be mapped as a DU.
                    //
                    // If there are 2 value types and one is a null
                    // this can be simply put as an option


                    failwith "TODO - handle")
            |> List.ofSeq

        ({ Namespace = None
           Name = schema.Title.ToPascalCase()
           Attributes = []
           Fields = fields }
        : RecordDefinition)


    //schema.Title

    //match schema.Type.Count with
    //| 1 ->
    //    match schema.Type[0] with
    //    | SchemaType.Array -> failwith "todo"
    //    | SchemaType.Boolean ->
    //        ({ Name = schema.Title
    //           FieldType = FieldType.BaseType BaseType.Boolean
    //           Attributes = []
    //           Optional = false }
    //        : Models.FieldDefinition)
    //    | SchemaType.None -> failwith "todo"
    //    | SchemaType.Integer -> failwith "todo"
    //    | SchemaType.Number -> failwith "todo"
    //    | SchemaType.Null -> failwith "todo"
    //    | SchemaType.Object -> failwith "todo"
    //    | SchemaType.String -> failwith "todo"
    //    | _ -> failwith "todo"
    //| 0 ->
    //    // No type defined...
    //    failwith "TODO  - handle"
    //| _ ->
    //    // If there is more than one valid type,
    //    // then any of these types if a valid value.
    //    // So this could be mapped as a DU.
    //    //
    //    // If there are 2 value types and one is a null
    //    // this can be simply put as an option
    //

    //failwith "TODO - handle"

    let buildModels (schema: JsonSchema) =

        match schema.Type.Count with
        | 0 -> Result.Error "No root type defined"
        | 1 ->
            match schema.Type[0] with
            | SchemaType.Object -> build schema |> Ok // Ok()
            | _ -> Result.Error "Root type is not object"
        | _ -> Result.Error "Multiple types in root object not supported"

    let readSchema (str: string) =

        SchemaReader.ReadSchema(str, "")


    let readSchemaFile (path: string) =
        use fs = File.OpenRead path

        use sr = new StreamReader(fs)

        SchemaReader.ReadSchema(sr, "")
