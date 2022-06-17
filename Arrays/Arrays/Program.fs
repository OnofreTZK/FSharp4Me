open System


let arrayExercice () =
    let arr =
        [|
            for i in 1..1000 do
                yield (i * i)
        |]

    let sumOfSquaresArr =
        arr
        |> Array.sum

    printfn ""
    printfn "%A" sumOfSquaresArr
    printfn ""

let arrayExerciceTwo () =
    let arr = 
        Array.init 1000 (fun i -> 
            let next = i + 1
            next * next)

    let sumOfSquaresArr =
        arr
        |> Array.sum

    printfn ""
    printfn "%A" sumOfSquaresArr
    printfn ""


let initiallyZeros () = 
    let zero = Array.zeroCreate<int> 10

    printfn ""
    printfn "%A" zero 
    printfn ""


[<EntryPoint>]
let main argv = 

    let isEven x =
        x % 2 = 0

    let todayIsThursday () =
        DateTime.Now.DayOfWeek = DayOfWeek.Thursday

    let numbers = 
        [|
            100
            if todayIsThursday () then 42
            for i in 0..9 do // -> pown 2 i
                let x = i * i
                if x |> isEven then
                    x
            900
        |]

    let initArray = Array.init 5 (fun i -> pown 2 i)

    printfn "%A" numbers
    arrayExercice ()
    printfn "%A" initArray
    arrayExerciceTwo ()
    initiallyZeros ()
    
    0
