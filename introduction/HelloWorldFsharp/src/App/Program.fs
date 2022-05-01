open System

[<EntryPoint>]
let main argv = // string [] -> int
    printfn "Hello from F#"
    let str_with_args = String.concat " " argv |> 
                        fun concatened_args -> "Argv input -> " + concatened_args
    in
    printfn "%s" str_with_args |> fun () -> 0
