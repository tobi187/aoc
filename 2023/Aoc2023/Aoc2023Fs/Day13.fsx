open System
open System.IO

let file = (File.ReadAllText "in.txt").Split "\r\n\r\n"

let identical (a, b) = a = b

let find l =
    let dt = List.distinct l
    0

let calc ind rows =
    let f, s = List.splitAt ind rows

    f.Length

let proces (grid:string) = 
    let rows =
        grid.Split "\r\n"
        |> Array.toList
        |> List.map (Seq.toList)
    
    let horRows =
        rows
        |> List.transpose
        |> List.map (List.rev)

    
file
|> Array.map (proces)
|> Array.sum
    
    
// 29857 -> too low