// Heating the engines...

open System
open FSharp.Stats
open FSharp.Data
open Deedle

let factorialOf3 = SpecialFunctions.Factorial.factorial 3

let () =
    printfn "%f" factorialOf3

// DATA ACCESS
// Retrieve data using the FSharp.Data package
let rawData = Http.RequestString @"https://raw.githubusercontent.com/dotnet/machinelearning/master/test/data/housing.txt"

// And create a data frame object using the ReadCsvString method provided by Deedle.
// Note: Of course you can directly provide the path to a local source.
let df = Frame.ReadCsvString(rawData,hasHeaders=true,separators="\t")

// Using the Print() method, we can use the Deedle pretty printer to have a look at the data set.
df.Print()

// DATA CRUNCHING

//The data set of choice is the Boston housing data set. As you can see from analyzing the printed output, 
//it consists of 506 rows. Each row represents a house in the boston city area and each column encodes a 
//feature/variable, such as the number of rooms per dwelling (RoomsPerDwelling), the median value of 
//owner-occupied homes in $1000's (MedianHomeValue) and even variables indicating if the house is 
//bordering river Charles (CharlesRiver, value = 1) or not (CharlesRiver, value = 0).
//
//Lets consider in our analysis that we are only interested in the variables just described, 
//furthermore we only want to keep rows where the value of the indicator variable is 0. We can use 
//Deedle to easily create a new frame that fullfills our criteria. In this example we'll also cast 
//the value of the column "CharlesRiver" to be of type bool, this illustrates how data frame 
//programming can become typesafe using Deedle.*/

let housesNotAtRiver = 
    df
    |> Frame.sliceCols ["RoomsPerDwelling";"MedianHomeValue";"CharlesRiver"]
    |> Frame.filterRowValues (fun s -> s.GetAs<bool>("CharlesRiver") |> not )

let housesNearHighway =
    df
    |> Frame.sliceCols ["HighwayDistance"] // sliceCols is to get easier to visualize based in the values of the chosen cols
    |> Frame.filterRowValues (fun distance -> (distance.GetAs<int>("HighwayDistance")) <= 2 )

let zeroPercentResidental =
    df
    |> Frame.sliceCols ["PercentResidental"]
    |> Frame.filterRowValues (fun percentage -> (percentage.GetAs<float>("PercentResidental")) = 0.0)

//sprintf "The new frame does now contain: %i rows and %i columns" housesNotAtRiver.RowCount housesNotAtRiver.ColumnCount

housesNotAtRiver.Print()
housesNearHighway.Print()
zeroPercentResidental.Print()
