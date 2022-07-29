// Heating the engines...

open System
open FSharp.Stats
open FSharp.Data
open Deedle
open Plotly.NET

let factorialOf3 = SpecialFunctions.Factorial.factorial 3

let () = printfn "%f" factorialOf3

// DATA ACCESS
// Retrieve data using the FSharp.Data package
let rawData =
    Http.RequestString @"https://raw.githubusercontent.com/dotnet/machinelearning/master/test/data/housing.txt"

// And create a data frame object using the ReadCsvString method provided by Deedle.
// Note: Of course you can directly provide the path to a local source.
let df = Frame.ReadCsvString(rawData, hasHeaders = true, separators = "\t")

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
    |> Frame.sliceCols [ "RoomsPerDwelling"
                         "MedianHomeValue"
                         "CharlesRiver" ]
    |> Frame.filterRowValues (fun s -> s.GetAs<bool>("CharlesRiver") |> not)

let housesNearHighway =
    df
    |> Frame.sliceCols [ "HighwayDistance" ] // sliceCols is to get easier to visualize based in the values of the chosen cols
    |> Frame.filterRowValues (fun distance -> (distance.GetAs<int>("HighwayDistance")) <= 2)

let zeroPercentResidental =
    df
    |> Frame.sliceCols [ "PercentResidental" ]
    |> Frame.filterRowValues (fun percentage -> (percentage.GetAs<float>("PercentResidental")) = 0.0)

//sprintf "The new frame does now contain: %i rows and %i columns" housesNotAtRiver.RowCount housesNotAtRiver.ColumnCount

housesNotAtRiver.Print()
housesNearHighway.Print()
zeroPercentResidental.Print()

// DATA EXPLORATION

//Exploratory data analysis is an approach favored by many - to meet this demand we strongly
//advertise the use of Plotly.Net. The following snippet illustrates how we can access a column
//of a data frame and create an interactive chart in no time. Since we might want to get an idea
//of the distribution of the house prices, a histogram can come in handy:


// Note that we explicitly specify that we want to work with the values as floats.
// Since the row identity is not needed anymore when plotting the distribution we can
// directly convert the collection to a FSharp Sequence.
let pricesNotAtRiver: seq<float> =
    housesNotAtRiver
    |> Frame.getCol "MedianHomeValue"
    |> Series.values

let h1 =
    Chart.Histogram(pricesNotAtRiver)
    |> Chart.withXAxisStyle ("median value of owner occupied homes in 1000s")
    |> Chart.withXAxisStyle ("price distribution")
//|> Chart.show // Show in the browser

// Since plotly charts are interactive they invite us to combine multiple charts.
// Let repeat the filter step and see if houses that are located at the river show a similar distribution:
let housesAtRiver =
    df
    |> Frame.sliceCols [ "RoomsPerDwelling"
                         "MedianHomeValue"
                         "CharlesRiver" ]
    |> Frame.filterRowValues (fun s -> s.GetAs<bool>("CharlesRiver"))

let pricesAtRiver: seq<float> =
    housesAtRiver
    |> Frame.getCol "MedianHomeValue"
    |> Series.values

let h2 =
    [ // array of charts
      Chart.Histogram(pricesNotAtRiver)
      |> Chart.withTraceInfo "not at river" // pricesNotAtRiver  chart
      Chart.Histogram(pricesAtRiver)
      |> Chart.withTraceInfo "at river" ] // pricesAtRiver chart
    |> Chart.combine
    |> Chart.withXAxisStyle ("median value of owner occupied homes in 1000s")
    |> Chart.withXAxisStyle ("Comparison of price distributions")
//|> Chart.show

// The interactive chart allows us to compare the distributions directly.
// We can now reconstruct our own idea of the city of Boston, the sampled area,
// just by looking at the data e.g.:
// Assuming that the sampling process was homogenous while observing that there are much more houses
// sampled that are not located on the riverside could indicate that a spot on the river is a scarce
// commodity.
// This could also be backed by analyzing the tails of the distribution: it seems that houses located
// at the river are given a head-start in their assigned value - the distribution of the riverside houses
// is truncated on the left.

// Suppose we would have a customer that wants two models, one to predict the prices of a house at
// the riverside and one that predicts the prices if this is not the case, then we can meet this demand
// by using FSharp.Stats in combination with Deedle.

// Of course we need a variable that is indicative of the house price, in this case we will check if
// the number of rooms per dwelling correlates with the house value:

// Getting the values of a column as a series
let pricesAll: Series<int, float> = df |> Frame.getCol "MedianHomeValue"

// Getting the values of a column as a series
let roomsPerDwellingAll: Series<int, float> = df |> Frame.getCol "RoomsPerDwelling"

// The compiler couldn't infer that Seq in this case came from Plotly.NET
// Reference -> https://github.com/plotly/Plotly.NET/blob/7e6e50b2f64ee2b78fe934b76bc91625c93b79e2/src/Plotly.NET/InternalUtils.fs
// Splits a sequence of pairs into two sequences
let unzip (input: seq<_>) =
    let (lstA, lstB) =
        Seq.foldBack (fun (a, b) (accA, accB) -> a :: accA, b :: accB) input ([], [])

    (Seq.ofList lstA, Seq.ofList lstB)

let correlation =
    let tmpPrices, tmpRooms =
        Series.zipInner pricesAll roomsPerDwellingAll
        |> Series.values
        |> unzip

    Correlation.Seq.pearson tmpPrices tmpRooms

let () = printfn "correlation: %f" correlation

// So indeed, the number of rooms per dwelling shows a positive correlation with the house prices. 
// With a pearson correlation of ~0.7 it does not explain the house prices completely - 
// but this is nothing that really surprises us, as one of our hypothesis is that the location 
// (e.g. riverside) does also have influence on the price - however, it should be sufficient to create a linear model.
