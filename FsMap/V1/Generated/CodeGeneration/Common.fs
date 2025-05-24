namespace FsMap.V1.Generated.CodeGeneration

open Fabulous.AST
open FsMap.V1.Core.Models
open type Fabulous.AST.Ast

module Common =

    type BaseType with

        member bt.ToAstType() =
            match bt with
            | BaseType.Boolean -> Boolean()
            | BaseType.Byte -> Byte()
            | BaseType.SByte -> SByte()
            | BaseType.Int16 -> Int16()
            | BaseType.Int32 -> Int()
            | BaseType.Int64 -> Int64()
            | BaseType.UInt16 -> UInt16()
            | BaseType.UInt32 -> UInt32()
            | BaseType.UInt64 -> UInt64()
            | BaseType.Single -> Float32()
            | BaseType.Double -> Float()
            | BaseType.Decimal -> Decimal()
            | BaseType.DateTime -> LongIdent("System.DateTime")
            | BaseType.DateTimeOffset -> LongIdent("System.DateTimeOffset")
            | BaseType.TimeSpan -> LongIdent("System.TimeSpan")
            | BaseType.String -> String()
            | BaseType.Guid -> LongIdent("System.Guid")

    let ``convert non-scalar field types to references`` (fieldType: FieldType) =
        match fieldType with
        | FieldType.BaseType baseType -> fieldType
        | FieldType.Record recordDefinition -> FieldType.Reference recordDefinition.Name
        | FieldType.DiscriminatedUnion discriminatedUnionDefinition ->
            FieldType.Reference discriminatedUnionDefinition.Name
        | FieldType.Function functionDefinition -> fieldType
        | FieldType.Reference name -> fieldType

    let ``add OptionPostfix if require`` (isOptional: bool) (value: WidgetBuilder<Fantomas.Core.SyntaxOak.Type>) =
        if isOptional then OptionPostfix value else value

    let createField (field: FieldDefinition) =
        match field.FieldType with
        | FieldType.BaseType baseType ->
            Field(field.Name, baseType.ToAstType() |> ``add OptionPostfix if require`` field.Optional)
        | FieldType.Record recordDefinition ->
            Field(
                field.Name,
                LongIdent(recordDefinition.Name)
                |> ``add OptionPostfix if require`` field.Optional
            )
        | FieldType.DiscriminatedUnion discriminatedUnionDefinition ->
            Field(
                field.Name,
                LongIdent(discriminatedUnionDefinition.Name)
                |> ``add OptionPostfix if require`` field.Optional
            )
        | FieldType.Function functionDefinition -> failwith "todo"
        | FieldType.Reference name ->
            Field(field.Name, LongIdent(name) |> ``add OptionPostfix if require`` field.Optional)

    let record (definition: RecordDefinition) =
        Record(definition.Name) {
            for field in definition.Fields do
                createField field
        }

    let generated (moduleName: string) (moduleMembers: ModuleMember list) =
        Oak() {
            Namespace("Widgets") {
                Module(moduleName) {
                    for definition in moduleMembers do
                        match definition with
                        | ModuleMember.Record recordDefinition -> record recordDefinition
                        | ModuleMember.DiscriminatedUnion discriminatedUnionDefinition -> failwith "todo"
                        | ModuleMember.Function functionDefinition -> failwith "todo"
                }
            }
        }
        |> Gen.mkOak
        |> Gen.run
