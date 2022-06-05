namespace StudentScores

// Like templates in C++
type Optional<'T> =
    | Something of 'T
    | Nothing

module Optional =

    let defaultValue (d: 'T) (optional: Optional<'T>) =
        match optional with
        | Something v -> v
        | Nothing -> d
