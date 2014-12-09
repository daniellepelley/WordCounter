// Learn more about F# at http://fsharp.net
open System.IO

let CountWords file = 
    seq { use fileReader = new StreamReader(File.OpenRead(file))
        while not fileReader.EndOfStream do
            yield fileReader.ReadLine() }
        |> Seq.collect (fun line -> line.Split [|' '|])
        |> Seq.countBy (fun x -> x)
        |> Seq.toArray

[<EntryPoint>]
let main argv = 

    let result = CountWords "UKWiki.txt"

    printfn "%A" result
    let r = System.Console.ReadLine()

    0