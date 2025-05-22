namespace FsMap.V1.Dynamic

open System
open Microsoft.FSharp.Reflection

module Operations =
    
    let ``can map records`` (fromType: Type) (toType: Type) =
        match FSharpType.IsRecord fromType, FSharpType.IsRecord toType with
        | true, true ->
            toType.GetProperties()
            
            failwith "todo"
        | false, _
        | _, false -> false
    
    let testMapRecord<'TFrom, 'TTo> () =
        let fromType = typeof<'TFrom>
        let toType = typeof<'TTo>
        
        ()
    
    let mapRecord<'TFrom, 'TTo> (fromObj: 'TFrom) =
        let fromType = typeof<'TFrom>
        
        FSharpType.IsRecord
        
        ()

