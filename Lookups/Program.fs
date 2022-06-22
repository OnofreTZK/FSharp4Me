open System
open System.Collections.Generic


// Dicts
let mockDict = Dictionary<string, string> ()
let dynamicMockDict<'T> = Dictionary<_, _> ()

[<EntryPoint>]
let main argv =
    mockDict.Add ("Datarisk", "FSharp")
    mockDict.Add ("Stone", "Swift")
    printfn "Dictionary - Companies: %A" mockDict
    printfn "Total Number of Companies: %d" mockDict.Count
    printfn "The keys: %A" mockDict.Keys
    printfn "The Values: %A" mockDict.Values
    printfn "Value: %s" mockDict.["Datarisk"]

    0
