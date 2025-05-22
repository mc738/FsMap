namespace FsMap.V1.Core

open System

module Attributes =
    
    
    type MapFromAttribute(fromTypee: Type, field: string) =
        inherit Attribute()
        
        member _.Field = field
       
        member this.FromType = fromTypee
        
    type MapToAttribute(toType: Type, field: string) =
        inherit Attribute()
        
        member _.Field = field
        
        member _.ToType = toType
        
    [<AttributeUsage(AttributeTargets.Property)>]    
    type DefaultAttribute() =
        inherit Attribute()

    //type F<'T>() = ()