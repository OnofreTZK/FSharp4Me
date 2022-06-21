open System


module MathSequence =

    type State = {
        n : int
        previous : int
        before : int 
    }

    // Building a sequence using pell numbers
    let pell =
        (0, 0, 0)
        |> Seq.unfold (fun (n, pn2, pn1) -> 
            let pn =
                match n with
                | 0 | 1 -> n
                | _ -> 2*pn1 + pn2
            let n' = n + 1
            Some (pn, (n', pn1, pn)))   
    
    // Building a sequence using pell numbers and a record type as
    // expressive static type to represent the current state of the sequence
    let pellV2 =
        { n = 0 ; previous = 0; before = 0 } 
        |> Seq.unfold (fun state -> 
            let pn =
                match state.n with
                | 0 | 1 -> state.n
                | _ -> 2*state.before + state.previous
            let n' = state.n + 1
            Some (pn, {n=n'; previous=state.before; before=pn}))   

    // Building a sequence using fibonacci numbers
    let fib =
        (0, 0, 0)
        |> Seq.unfold (fun (n, pn2, pn1) -> 
            let pn =
                match n with
                | 0 | 1 -> n
                | _ -> pn1 + pn2
            let n' = n + 1
            Some (pn, (n', pn1, pn)))  

module Drunkard =

    type Position = { X : int ; Y : int }

    let randomizer = Random ()

    let step () =
        randomizer.Next(-1, 2)

    let walk =
        { X = 0 ; Y = 0 }
        |> Seq.unfold (fun position ->
            let xNextStep = position.X + step ()
            let yNextStep = position.Y + step ()
            let position' = {X=xNextStep; Y=yNextStep}
            Some (position', position'))

[<EntryPoint>]
let main argv = 
    
    let total =
        seq { for i in 1..1000 -> i * i }
        |> Seq.sum
    
    printfn "The total is: %i" total

    MathSequence.pell
    |> Seq.truncate 10
    |> Seq.iter (fun x -> printf "%i, " x)

    printfn "..."
    
    MathSequence.fib
    |> Seq.truncate 10
    |> Seq.iter (fun x -> printf "%i, " x)
    
    printfn "..."
    
    MathSequence.pellV2
    |> Seq.truncate 10
    |> Seq.iter (fun x -> printf "%i, " x)

    printfn "..."
    
    Drunkard.walk
    |> Seq.take 10
    |> Seq.iter (fun pos -> printf "(X = %i, Y = %i)" pos.X pos.Y)

    printfn "..."

    0
