open System

let add a b = a + b

let c = add 2 3

let d = add 2

let e = d 4

// ------------------------------------------------------------------------------------------------

let quote symbol s =
    sprintf "%c%s%c" symbol s symbol

let singleQuote = quote '\''
let doubleQuote = quote '"'

let () =
    printfn "%s" (singleQuote "Here is a phrase, another phrase") 
    printfn "%s" (doubleQuote "Here is a phrase, another phrase")
