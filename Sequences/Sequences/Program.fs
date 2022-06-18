open System

[<EntryPoint>]
let main argv = 
    
    let total =
        seq { for i in 1..1000 -> i * i }
        |> Seq.sum
    
    printfn "The total is: %i" total

    0
