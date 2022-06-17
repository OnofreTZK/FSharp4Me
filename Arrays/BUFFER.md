# Array.zeroCreate and Stream.read

```fsharp
let readBytes length (stream: Stream) =
    let buffer = Array.zeroCreate<byte> length
    let count = stream.Read(buffer, 0, length)
    if count = length then
       buffer
    else
       raise <| EndOfStreamException ()
```
