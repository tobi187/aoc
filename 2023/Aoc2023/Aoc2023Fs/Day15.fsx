open System
open System.IO

let s = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"

let ss = File.ReadAllText "in.txt" 

type Lens = {
    label   : string
    len     : int
    hash    : int
}

let isDash l = l.len = 0 

let toLabel (str:string) = 
    let a = str.Split ([|'='; '-'|], StringSplitOptions.RemoveEmptyEntries)
    if Array.length a > 1 then 
        { label = a[0]; len = int a[1]; hash = 0 }
    else 
        { label = a[0]; len = 0; hash = 0 }
        
let rec hash str cv =
    match str with
    | [] -> cv
    | hd :: tl ->
        let nh = (int hd + cv) * 17 % 256
        hash tl nh
        
let l : Lens list array = Array.init 256 (fun _ -> [])


let dash lb =
    l[lb.hash] <-
        l[lb.hash]
        |> List.where (fun v -> v.label <> lb.label)

let eqSign lb =
    match List.tryFindIndex (fun v -> v.label = lb.label) l[lb.hash] with
    | Some ind -> 
        l[lb.hash] <- List.updateAt ind lb l[lb.hash]
    | None -> 
        l[lb.hash] <- List.append l[lb.hash] [lb]

let rec doStuff lb =
    match lb with
    | [] -> 
        l
        |> Array.map (List.map (fun v -> v.len))
        |> Array.mapi (fun ii vv -> List.mapi (fun i v -> (i+1) * v * (ii+1)) vv)
        |> Array.map (List.sum)
        |> Array.sum
    | hd :: tl -> 
        if isDash hd then
            dash hd
        else 
            eqSign hd
        doStuff tl

let a = 
    ss.Split ","
    |> Array.map (toLabel)
    |> Array.map (fun v -> { v with hash = hash (Seq.toList v.label) 0 })
    |> Array.toList

doStuff a
