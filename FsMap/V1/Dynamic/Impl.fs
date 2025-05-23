namespace FsMap.V1.Dynamic

open System
open System.Reflection

[<AutoOpen>]
module Impl =

    type MappedType =
        { Type: Type
          DefaultProperty: PropertyInfo }

    and MappedProperty =
        { Name: string
          PropertyInfo: PropertyInfo }


    type DynamicMapper() =

        member _.Map<'TFrom, 'TTo>(fromType: 'TFrom) =



            ()
       

    ()
