namespace FsMap.V1.Core

open System
open System.Text.Json
open System.Text.Json.Serialization

module Models =

    [<RequireQualifiedAccess>]
    type BaseType =
        | Boolean
        // Bytes
        | Byte
        | SByte
        // Int
        | Int16
        | Int32
        | Int64
        | UInt16
        | UInt32
        | UInt64
        // Float
        | Single
        | Double
        // Decimal
        | Decimal
        // Date/times
        | DateTime
        | DateTimeOffset
        | TimeSpan
        // Strings
        | String
        // Other
        | Guid

        member bt.GetCLRType() =
            match bt with
            | Boolean -> typeof<bool>
            | Byte -> typeof<byte>
            | SByte -> typeof<sbyte>
            | Int16 -> typeof<int16>
            | Int32 -> typeof<int>
            | Int64 -> typeof<int64>
            | UInt16 -> typeof<uint16>
            | UInt32 -> typeof<uint>
            | UInt64 -> typeof<uint64>
            | Single -> typeof<single>
            | Double -> typeof<double>
            | Decimal -> typeof<decimal>
            | DateTime -> typeof<DateTime>
            | DateTimeOffset -> typeof<DateTimeOffset>
            | TimeSpan -> typeof<TimeSpan>
            | String -> typeof<string>
            | Guid -> typeof<Guid>

    type [<RequireQualifiedAccess>] FieldType =
        | BaseType of BaseType
        | Record of RecordDefinition
        | DiscriminatedUnion of DiscriminatedUnionDefinition
        | Function of FunctionDefinition
        | Reference of Name: string

    and RecordDefinition =
        { Namespace: string option
          Name: string
          Attributes: AttributeDefinition list
          Fields: FieldDefinition list }

    and FieldDefinition =
        { Name: string
          FieldType: FieldType
          Attributes: AttributeDefinition list
          Optional: bool }

    and DiscriminatedUnionDefinition =
        { Namespace: string option
          Name: string
          Attributes: Attribute list }

    and DiscriminatedUnionCaseDefinition =
        { Name: string
          Attributes: Attribute list }

    and FunctionDefinition = { Name: string }

    and AttributeDefinition =
        { Namespace: string option
          Name: string }

    and ModuleDefinition =
        { Name: string option
          Attributes: Attribute list }

    and [<RequireQualifiedAccess>] ModuleMember =
        | Record of RecordDefinition
        | DiscriminatedUnion of DiscriminatedUnionDefinition
        | Function of FunctionDefinition
        