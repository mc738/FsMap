namespace FsMap.V1.Core

open System
open System.Text.Json
open System.Text.Json.Serialization

module Models =

    /// <summary>This constructor initializes the new Point to
    /// (<paramref name="xPosition"/>,<paramref name="yPosition"/>).
    /// </summary>
    /// <param name="xPosition">the new Point's x-coordinate.</param>
    /// <param name="yPosition">the new Point's y-coordinate.</param>
    type DocumentComments =
        { Summary: string list
          Examples: ExampleComment list
          Exceptions: ExceptionComment list
          Includes: IncludeComment list
          Parameters: ParameterComment list

        }

    and [<RequireQualifiedAccess>] SummaryCommentElement =
        | Literal of string
        /// <summary>
        /// <c>para</c> element.
        /// </summary>
        | Paragraph of string list
        /// <summary>
        /// <c>paramref</c> element.
        /// </summary>
        | ParameterReference
        | List
        /// <summary>
        /// <c>see</c> element.
        /// </summary>
        /// <param name="Cref">The <c>cref</c> value.</param>
        | See of Cref: string

    and ListElement =
        { ListType: ListType
          Header: ListItem
          Items: ListItem }

    and [<RequireQualifiedAccess>] ListType =
        | Bullet
        | Number
        | Table

    and ListItem = { Term: string; Description: string }

    and ExampleComment = { Lines: string list }

    and ExceptionComment = { Cref: string; Lines: string list }

    and IncludeComment = { File: string; Path: string }

    and ParameterComment = { Name: string; Content: string }

    and PermissionComment = { Cref: string; Content: string }

    and RemarksComment = { Content: string }

    and ReturnsComment = { Content: string }

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

    [<RequireQualifiedAccess>]
    type FieldType =
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
