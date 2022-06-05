namespace StudentScores

module Summary =

    open System.IO

    let summarize filePath =
        let rows = File.ReadAllLines filePath
        let studentCount = (rows |> Array.length) - 1
        printfn "Student count %i" studentCount

        rows
        |> Array.skip 1
        |> Array.map Student.fromString
        |> Array.sortByDescending (fun student -> student.MeanScore)
        |> Array.iter Student.printSummary
