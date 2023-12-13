open System
open System.IO

let file = (File.ReadAllText "in.txt").Split "\r\n\r\n"

let identical (a, b) = a = b
let findIdentical l =
    let c = 
        l
        |> List.pairwise 
        |> List.indexed
        |> List.where (fun (_,d) -> identical d) 
    if c.Length = 0 then
        None
    else
        let ll = floor (float c.Length / 2.0)
        fst c[int ll]
        |> Some

let detWin ind rows =
    let f, s = List.splitAt ind rows
    let min = min f.Length s.Length

    f |> List.rev |> List.take min =  List.take min s

let calc ind rows =
    let f, s = List.splitAt ind rows

    //if f.Length < s.Length then s else f
    //|> List.sumBy (List.where ((=)'#') >> List.length)
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

    match findIdentical rows, findIdentical horRows with
    | Some i1, Some i2 -> 
        let a = detWin (i1+1) rows
        let b = detWin (i2+1) horRows
        if a && b then failwith "Maaaaaaaaaaaaaaaaaaaaan"
        elif a then (i1+1, rows) ||> calc |> fun res -> res * 100
        elif b then (i2+1, horRows) ||> calc 
        else failwith "MMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAN"
    | Some r, None -> (r+1, rows) ||> calc |> fun res -> res * 100
    | None, Some r -> (r+1, horRows) ||> calc
    | _ -> failwith "Not possi aswell i think"

file
|> Array.map (proces)
|> Array.sum
    
    
// 29857 -> too low