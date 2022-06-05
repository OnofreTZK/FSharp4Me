namespace StudentScores

module Float =

    let tryFromString s =
        if s = "N/A" then
            Nothing
        else
            Something(float s)

    let fromStringOr d s =
        s |> tryFromString |> Optional.defaultValue d
