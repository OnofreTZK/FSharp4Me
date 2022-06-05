type Point<'T> =
    {
        X : 'T
        Y : 'T
    }

module Point =

    // We need the inline keyword for generic functions
    let inline moveBy (dx : 'T) (dy : 'T) (p : Point<'T>) =
        {
            X = p.X + dx
            Y = p.Y + dy
        }
