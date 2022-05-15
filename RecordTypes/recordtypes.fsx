open System
open System.IO // Files

type Student = 
    {
        Name : string
        Id : string
        MeanScore : float
        MaxScore : float
        MinScore : float
    }

module Student =
    let fromString (row : string) =
        let elements = row.Split('\t')
        let name = elements.[0]
        let id = elements.[1]
        let score =
            elements
            |> Array.skip 2
            |> Array.map float
        let meanScore = score|> Array.average
        let maxScore = score |> Array.max
        let minScore = score |> Array.min
        {
            Name = name
            Id = id
            MeanScore = meanScore
            MaxScore = maxScore
            MinScore = minScore
        }


    let printSummary (s : Student) =
        printfn "Name: %s\tID: %s\tAverage: %0.1f\tHigh score: %0.1f\tLow score: %0.1f" s.Name s.Id s.MeanScore s.MaxScore s.MinScore 

let summarize filePath =
    let rows = File.ReadAllLines filePath
    let studentCount = rows.Length - 1
    printfn "Student count: %i" studentCount
    rows 
    |> Array.skip 1 // Builds a new array that contains the elements of the given array, excluding the first N elements. 
    |> Array.map Student.fromString
    |> Array.sortByDescending (fun student -> student.MeanScore)
    |> Array.iter Student.printSummary

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
