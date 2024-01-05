open System
open System.IO

let file = (File.ReadAllText "in.txt").Split "\r\n\r\n"

let ident (a,b) = a = b

let rec findMaxAndCount (is, l, i1, i2) =
    let ret () = (is - i1, is + 1)
    if i1 < 0 || i2 >= List.length l then ret ()
    elif l[i1] <> l[i2] then ret ()
    else findMaxAndCount (is, l, (i1-1), (i2+1))
    

let rec find l =
    let pl = List.pairwise l
    [
        for i in 0..pl.Length-1 do
            if ident pl[i] then yield i
    ]
    |> List.map (fun v -> (v, l, v, v+1))
    |> List.map (findMaxAndCount)
    |> List.sortByDescending (fst)
    |> List.tryHead
    |> Option.defaultValue (0, 0)

let proces (grid:string) = 
    let rows =
        grid.Split "\r\n"
        |> Array.toList
        |> List.map (Seq.toList)
    
    let horRows =
        rows
        |> List.transpose
        |> List.map (List.rev)

    match find rows, find horRows with
    | ((a, b), (c, d)) when a > c -> b * 100
    | ((a, b), (c, d)) -> d

    
file
|> Array.map (proces) 
    
    
// 31764 -> too low