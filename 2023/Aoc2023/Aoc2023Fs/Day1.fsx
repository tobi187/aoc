open System
open System.IO

let file = File.ReadAllLines "in.txt"

let nums = "one, two, three, four, five, six, seven, eight, nine".Split(", ")

let dNums = nums |> Array.indexed |> Array.map (fun (f,s) -> s, string (f + 1)) |> dict

let findFirst (str: string) =
    
    let parsed = Array.fold (fun (s: string) (x: string) -> s.Replace(x, dNums.Item x)) str nums
    printfn "%A" parsed
    let s = parsed |> Seq.filter (Char.IsNumber) |> Seq.toArray
    if (s.Length < 2) then printfn "%s" str
    
    int (string s[0] + string (Seq.last s))
        

file
|> Array.map findFirst
|> Array.sum