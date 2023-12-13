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
    |> List.map (fun a -> a[0], a[1])
    |> List.map (fun (a, b) -> [0L..(b/100000L)..(b)] |> List.map (fun n -> n + a))

let pSeed = [1778931867..10..(1778931867+1436999653-1)]
    
let ps2 = [pSeed[1589310]..pSeed[1589325]]

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


seeds2
|> List.map (fun x -> List.map (fun y -> List.fold (translate) y maps) x)
|> List.indexed
|> List.minBy (fun (_, b) -> List.min b)
|> (fun (a,b) -> a)

pSeed
|> List.map (fun x -> List.fold (translate) x maps)
|> List.indexed
|> List.sortBy (snd)
|> List.take 5

// 27389529 -> That's not the right answer; your answer is too high.
