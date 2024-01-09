open System.IO
open System

let file = 
    File.ReadAllLines "in.txt" 
    |> Array.map (fun x -> x.Split(":")[1])

let calcRes (line:string) = 
    let a = line.Split("|")

    let win = a[0].Split(" ") |> Array.filter ((<>)"") |> Set
    let choice = a[1].Split(" ") |> Array.filter ((<>)"") |> Array.toList
    
    let c =
        choice
        |> List.filter (win.Contains)
    
    match List.isEmpty c with
    | true -> 0
    | false -> 
        c
        |> List.skip 1
        |> List.fold (fun s _ -> s * 2) 1

    

let calcResPartTwo (line:string) = 
    let a = line.Split("|")

    let win = a[0].Split(" ") |> Array.filter ((<>)"") |> Set
    let choice = a[1].Split(" ") |> Array.filter ((<>)"") |> Array.toList
    
    choice
    |> List.filter (win.Contains)
    |> List.length


let rec partTwo (f: string list) (n: int list) (r: int) =
    match f with
    | [] -> r
    | hd :: tl ->
        let getAm = calcResPartTwo hd
        let tn = n.Tail
        let hn = n.Head
        let nlnl = List.init getAm (fun _ -> hn)
        
        let newOne = 
            [
                for i in 1..(Math.Max(tn.Length, nlnl.Length)-1) do
                    match List.tryItem i tn, List.tryItem i nlnl with
                    | Some x, Some y -> yield x + y
                    | Some x, _ -> yield x
                    | _, Some y -> yield y
                    | _ -> failwith "Shit"
            ]

        let nnn = match List.isEmpty newOne with
                    | true -> [1]
                    | false -> List.updateAt 0 (newOne[0] + 1) newOne
        
        partTwo tl nnn (r + hn)


partTwo (file |> Array.toList) [1] 0 