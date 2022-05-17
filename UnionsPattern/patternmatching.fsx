open System

module Green =

    let bottles n =
        match n with
        | 0 -> "bottleless :("
        | 1 -> "One green bottle"
        | 2 -> "Two green bottle"
        | _ -> sprintf "%i is a lot of bottles" n

module Option =
    
    let describe x =
        match x with
        | Some v -> sprintf "The value was %O" v
        | None -> sprintf "There was no value"

module Array =

    let describe a =
        match a with
        | [||] -> "An empty array"
        | [|x|] -> sprintf "An array with one element: %O" x
        | [|x;y|] -> sprintf "An array with two elements: %O, %O" x y
        | _ -> sprintf "An array with %i elements." a.Length


let () = printfn "%s\n" (Green.bottles 0) 
         |> fun () -> printfn "%s\n" (Green.bottles 1) 
         |> fun () -> printfn "%s\n" (Green.bottles 2)
         |> fun () -> printfn "%s\n" (Green.bottles 500)

let () = printfn "---"
         |> fun () -> printfn "%s" (Some 99 |> Option.describe) 
         |> fun () -> printfn "%s" (Some "abc" |> Option.describe) 
         |> fun () -> printfn "%s" (None |> Option.describe) 

let () = printfn "---"
         |> fun () -> printfn "%s" ([||] |> Array.describe) 
         |> fun () -> printfn "%s" ([|4|] |> Array.describe) 
         |> fun () -> printfn "%s" ([|"5"; "6"|] |> Array.describe) 
         |> fun () -> printfn "%s" ([|"5"; "6"; "7"; "8"|] |> Array.describe) 
