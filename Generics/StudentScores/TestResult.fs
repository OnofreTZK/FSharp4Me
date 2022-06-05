namespace StudentScores

type TestResult =
    | Absent
    | Excused
    | Scored of float

module TestResult =

    let fromString =
        function
        | "A" -> Absent
        | "E" -> Excused
        | str -> Scored(float str)

    let effectiveScore (testResult: TestResult) =
        match testResult with
        | Absent -> 0.0
        | Excused -> 50.0
        | Scored score -> score

    let ignoreExcused (testResult: TestResult) =
        match testResult with
        | Excused -> None
        | Absent -> Some 0.0
        | Scored score -> Some score
