namespace FsMap.V1.Generated.Json

open System.IO
open Microsoft.Json.Schema

module Schema =
    
    
    let readSchema (str: string) =
        
        SchemaReader.ReadSchema(str, "")

    
    let readSchemaFile (path: string) =
        use fs = File.OpenRead path
        
        
        SchemaReader.ReadSchema