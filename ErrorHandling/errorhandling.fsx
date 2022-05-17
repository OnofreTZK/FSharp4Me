open System
open System.IO

let run (argv : string[]) =
    if argv.Length = 1 then
        let filePath = argv.[0]
        if (File.Exists filePath) then
            printfn "Processing %s" filePath
            try
                let rows = File.ReadAllLines filePath
                0
            with
            | :? FormatException as e -> e 
                                        |> fun _ -> printfn "fuck the error"
                                        |> fun () -> 0
            | :? IOException as e -> e 
                                        |> fun _ -> printfn "fuck the error"
                                        |> fun () -> 0
            | _ as e -> e 
                                        |> fun _ -> printfn "fuck the error"
                                        |> fun () -> 0
        else
            printfn "Please, provide a file that exists!"
            1
    else 
        printfn "Please, specify >only< one file"
        1

run (fsi.CommandLineArgs |> Array.tail)
