open System
open System.IO

let file = File.ReadAllLines "in.txt" |> Array.map (Seq.toArray)

type Point = {
    x:      int
    y:      int
    steps:  int
}

let start = 
    file 
    |> Array.map (fun a -> Array.tryFindIndex ((=)'S') a)
    |> Array.mapi (fun i o -> o |> function 
                                | Some ind -> Some (ind, i) 
                                | None -> None) 
    |> Array.choose (id) 
    |> Array.exactlyOne

start