open System
open System.IO

let file = (File.ReadAllText "in.txt").Split "\r\n\r\n"

type Range = {
    dest:    int64
    source:    int64
    range:  int64
}

let toRange (s: string) =
    let ss = s.Split ()
    {
        dest = int64 ss[0]
        source = int64 ss[1]
        range = int64 ss[2]
    }

let seeds = (Array.head file).Replace("seeds: ", "").Split () |> Array.toList |> List.map (int64)

let seeds2 = 
    (Array.head file).Replace("seeds: ", "").Split () 
    |> Array.toList
    |> List.map (int64)
    |> List.chunkBySize 2
    |> List.map (fun [f;s] -> seq { f..(f+s-1L)  } |> Seq.toList )
    |> List.concat


let maps = 
    Array.tail file
    |> Array.map (
        fun s -> 
            s.Split("\r\n")
            |> Array.tail
            |> Array.map (toRange)
            |> Array.toList
    )
    |> Array.toList


let translate (num: int64) (rl: Range list) =
    let find rg =  rg.source <= num && num < rg.source + rg.range
    let theOne =
        rl
        |> List.filter (find)
        |> List.tryExactlyOne
        |> Option.defaultValue { source = 0; dest = 0; range = 0 }

    let diff = abs (theOne.source - theOne.dest)

    num + diff * int64 (sign (theOne.dest - theOne.source))

seeds
|> List.map (fun x -> List.fold (translate) x maps)
|> List.min
|> printfn "%A"