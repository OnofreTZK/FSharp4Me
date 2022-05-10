open System

let main (argv: string[]) =
    //let mutable person = "Anon Person"
    //if argv.Length > 0 then
    //    person <- argv.[0]
    let person = 
        if argv.Length > 0 then
            argv.[0]
        else
            "Anon Person"
    printfn "Hello %s from F# script!" person
    0

main (fsi.CommandLineArgs |> Array.tail)
