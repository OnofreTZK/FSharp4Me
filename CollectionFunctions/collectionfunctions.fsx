open System
open System.IO // Files

let printMeanScore (row : string) =
    let elements = row.Split('\t')
    let name = elements.[0]
    let id = elements.[1]
    let meanScore =
        elements
        |> Array.skip 2
        //|> Array.map float
        //|> Array.reduce (fun prev next -> (prev+next)/2.0) im dumb it worked as expected
        //|> Array.average
        |> Array.averageBy float

    printfn "Name: %s\tID: %s\tAverage: %0.1f\n" name id meanScore 

let summarize filePath =
    let rows = File.ReadAllLines filePath
    let studentCount = rows.Length - 1
    printfn "Student count: %i" studentCount
    rows 
    |> Array.skip 1 // Builds a new array that contains the elements of the given array, excluding the first N elements. 
    |> Array.iter printMeanScore

let run (argv : string[]) =
    if argv.Length = 1 then
        let filePath = argv.[0]
        if (File.Exists filePath) then
            printfn "Processing %s" filePath
            summarize filePath
            0
        else
            printfn "Please, provide a file that exists!"
            1
    else 
        printfn "Please, specify >only< one file"
        1

run (fsi.CommandLineArgs |> Array.tail)
