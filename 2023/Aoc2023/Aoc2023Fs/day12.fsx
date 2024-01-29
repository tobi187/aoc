open System
open System.IO

let parse (str:string) =
    let [|a; b|] = str.Split " "
    Seq.toList a
    ,
    b.Split ","
    |> Array.map (int)
    |> Array.toList

let file = 
    File.ReadAllLines "in.txt"
    |> Array.toList
    |> List.map (parse)

file

