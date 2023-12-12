open System
open System.IO

let file = 
    File.ReadAllLines "in.txt" 
    |> Array.map (fun s -> 
                        s.Split " " 
                        |> Array.map (int) 
                        |> Array.toList) 
    |> Array.toList


let rec findZeroes (l : int list list) =
    let cl = List.head l
    match List.forall ((=)cl.Head) cl with
    | true -> 
        l
        |> List.skip 1
        |> List.fold (fun s v -> (List.last v) + s) (List.last l.Head)
        
    | false -> 
        //let nl = 
        cl 
        |> List.pairwise 
        |> List.map (fun (a,b) -> b - a)
        |> fun nl -> findZeroes (nl :: l)

let rec findZeroesBackwards (l : int list list) =
    let cl = List.head l
    match List.forall ((=)cl.Head) cl with
    | true -> 
        l
        |> List.skip 1
        |> List.fold (fun s v -> (List.head v) - s) (List.head l.Head)
        
    | false -> 
        //let nl = 
        cl 
        |> List.pairwise 
        |> List.map (fun (a,b) -> b - a)
        |> fun nl -> findZeroesBackwards (nl :: l)


file
|> List.map (fun l -> [l])
|> List.map (findZeroesBackwards)
|> List.sum