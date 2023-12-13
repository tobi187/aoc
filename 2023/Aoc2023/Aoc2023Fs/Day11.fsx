open System
open System.IO


let file = 
    File.ReadAllLines "in.txt" 
    |> Array.map (Seq.toList) 
    |> Array.toList


let rec addExtra f r =
    match f with
    | [] -> r
    | hd :: tl -> 
        match List.forall ((=)'.') hd with
        | true -> hd :: hd :: r
        | false -> hd :: r
        |> addExtra tl

let getCords (i, l) = 
    l 
    |> List.indexed 
    |> List.filter (fun (_, e) -> e = '#') 
    |> List.map (fun (a,_) -> a, i)

let rec getPermuts (l : (int*int) list) =
    [
        match l with 
        | h::t -> for e in t do yield h, e
                  yield! getPermuts t
        | _ -> ()
    ]

let manhattan (f, s) = abs (fst f - fst s) + abs (snd f - snd s)

(file, [])
||> addExtra 
|> List.transpose
|> List.map (List.rev)
|> fun r -> (r, [])
||> addExtra
|> List.map (List.rev)
|> List.indexed
|> List.collect (getCords)
|> getPermuts
|> List.map (manhattan)
|> List.sum


// Part two

// open Aoc2023 -> need .fs file to include C# Project

//let rec getPermuts (l : (int64*int64) list) =
//    [
//        match l with 
//        | h::t -> for e in t do yield h, e
//                  yield! getPermuts t
//        | _ -> ()
//    ]

// let manhattan ((f, s) : (int64*int64)*(int64*int64)) = abs (fst f - fst s) + abs (snd f - snd s)

//let d = Day11 ()

//let b = 
//    d.PartTwo () 
//    |> Seq.toList
//    |> List.map (fun x -> x.ToTuple ())

//let c = getPermuts b

//c
//|> List.map (manhattan)
//|> List.sum
//|> printfn "%A"


// 82000210 -> too high