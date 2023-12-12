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


// Wieland

let difference (a,b) = b - a

let extrapolate_row = List.pairwise >> List.map difference

let extrapolate numbers =
    if List.sum numbers = 0 then None else
    Some (numbers, extrapolate_row numbers)

let calc_next_value s ns = s + List.last ns

let parse_line (s:string) =
    s.Split " "
    |> Array.toList
    |> List.map int
    |> List.unfold extrapolate
    |> List.fold calc_next_value 0

let start () =
    File.ReadAllLines "input.txt"
    |> Array.sumBy parse_line
    |> printfn "%A (1702218515 is correct)"


