open System

// MARK: - Demigod's list
let demigodsList = ["Godwyn"; "Morgott"; "Mohg"; "Rykard"; "Radahn"; "Ranni"; "Malenia"; "Miquella"]

// MARK: - Loops and Iterations
let forWithIndexRange (argv: string[]) =
    for i in 0..argv.Length-1 do
        let demigod = (argv.[i].ToLower ())
        in
        if (List.exists (fun (name: string) -> demigod.Equals(name.ToLower ())) demigodsList) then
            printf "%s it's a demigod!\n" demigod
        else
            printf "%s it's a foul tarnished!\n" demigod

let forWithIterator (argv: string[]) =
    for name in argv do
        if (List.exists (fun (demigod: string) -> name.ToLower().Equals(demigod.ToLower ())) demigodsList) then
            printf "%s it's a demigod!\n" (name.ToLower ())
        else
            printf "%s it's a foul tarnished!\n" (name.ToLower ())

let arrayIterLoopAux (name: string) =
    if (List.exists (fun (demigod: string) -> name.ToLower().Equals(demigod.ToLower ())) demigodsList) then
        printf "%s it's a demigod!\n" (name.ToLower ())
    else
        printf "%s it's a foul tarnished!\n" (name.ToLower ())

let arrayIterLoop (argv: string[]) =
    Array.iter arrayIterLoopAux argv

// MARK: - Running

let args = fsi.CommandLineArgs |> Array.tail

forWithIndexRange args
forWithIterator args
arrayIterLoop args
