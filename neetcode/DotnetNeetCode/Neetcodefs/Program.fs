(*
Given an array of integers; find if the array contains any duplicates.

Your function should return true if any value appears at least twice in the array, and it should return false if every element is distinct.

Example 1:
Input: [1,2,3,1]
Output: true

Example 2:
Input: [1,2,3,4]
Output: false

Example 3:
Input: [1,1,1,3,3,4,3,2,4,2]
Output: true
*)
open System
open System.Collections.Generic

let withTiming func arg =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    func arg |> ignore
    sw.Stop()
    sw.Elapsed

let runNTimes times func arg =
    Array.init times (fun _ -> func arg)

let benchmark func arg =
    let func = withTiming func
    runNTimes 50 func arg
    |> Array.averageBy(fun r -> r.TotalSeconds)
    |> System.TimeSpan.FromSeconds


let distinct l = List.distinct l |> List.length <> List.length l

let map l =
    let rec ads ss li = 
        match li with
        | [] -> false
        | hd :: _ when Set.contains hd ss -> true
        | hd :: tl -> ads (Set.add hd ss) tl

    ads (set [List.head l]) (List.tail l)

let cs l =
    let dic = HashSet<int>()

    List.exists (dic.Add >> not) l 

let run l name =
    printfn "Running %s" name

    printfn "distinct: %i" (benchmark distinct l).Milliseconds 
    printfn "map: %i" (benchmark map l).Milliseconds
    printfn "cs: %i" (benchmark cs l).Milliseconds

    printfn "\n"




run [1;1;1;3;3;4;3;2;4;2] "simple"
run [1..100000] "all ints"
let rnd = Random ()
run (List.init 100000 (fun _ -> rnd.Next())) "random"

