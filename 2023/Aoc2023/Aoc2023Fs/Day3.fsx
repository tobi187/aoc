open System.IO
open System

let file = File.ReadAllLines "in.txt"

let grid = file |> Array.map (Seq.toList) |> Array.toList

let rec findAdj x y res =
    0

let getNeighs x y =
    let nums = 
        [1,1; 0,1; 1,0; -1,0; 0,-1; -1,1; 1,-1; -1,1]
        |> List.where (fun (xx, yy) -> Char.IsNumber grid.[yy].[xx])
    
    let elems = [
        nums
        |> List.map (fun (xx,yy) -> 
            
        )
    ]
    
let getNum ((x, y) : int*int) =
    List.findIndex (fun x -> ) grid[y]
                        
