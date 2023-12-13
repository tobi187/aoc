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

let pSeeds = 
    "1778931867 1436999653 3684516104 2759374 1192793053 358764985 1698790056 76369598 3733854793 214008036 4054174000 171202266 3630057255 25954395 798587440 316327323 290129780 7039123 3334326492 246125391".Split ()
    |> Array.toList
    |> List.map (int64)
    |> List.chunkBySize 2
    |> List.map (fun a -> a[0], a[1])
    |> List.map (fun (a,b) -> [a..10L..(a+b-1L)])

// let ps2 = [pSeed[1589310]..pSeed[1589325]]

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


//seeds2
//|> List.map (fun x -> List.map (fun y -> List.fold (translate) y maps) x)
//|> List.indexed
//|> List.minBy (fun (_, b) -> List.min b)
//|> (fun (a,b) -> a)

pSeeds
|> List.map (fun aa -> 
    aa
    |> List.map (fun x -> List.fold (translate) x maps)
    |> List.indexed
    |> List.sortBy (snd)
    |> List.take 5
)
|> List.indexed
|> List.sortBy (fun a -> List.min (snd a))
|> printfn "%A"

// 27389529 -> That's not the right answer; your answer is too high.
