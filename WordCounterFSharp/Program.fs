// Learn more about F# at http://fsharp.net
open System
open System.IO


let CountWords file = 
    let nonLetters = [0..255]
                        |> Seq.map(fun x -> Convert.ToChar x)
                        |> Seq.filter(fun x -> Char.IsLetter(x) <> true)
                        |> Seq.toArray
    seq { use fileReader = new StreamReader(File.OpenRead(file))
        while not fileReader.EndOfStream do
            yield fileReader.ReadLine() }
        |> Seq.collect (fun line -> line.Split nonLetters)
        |> Seq.countBy (fun x -> x)
        |> Seq.toArray

[<EntryPoint>]
let main argv = 

    let result = CountWords "UKWiki.txt"

    printfn "%A" result
    let r = System.Console.ReadLine()

    0