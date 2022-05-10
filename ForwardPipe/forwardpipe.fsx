open System

let sayHello person =
    printfn "Hello %s" person

let isValid person =
    String.IsNullOrWhiteSpace person // kind like swift .isNilOrEmpty
    |> not

let run (argv: string[]) =
    // Pipeline
    argv |> Array.filter isValid |> Array.iter sayHello // partial application
    printf "finishing\n"
    0

run (fsi.CommandLineArgs |> Array.tail)
