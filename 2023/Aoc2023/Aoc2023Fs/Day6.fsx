open System
open System.IO

let file = 
    File.ReadAllLines "in.txt" 
    |> Array.map (fun st -> st.Split(":")[1])
    |> Array.map (fun st -> st.Split())
    |> Array.map (Array.filter((<>)String.Empty))
    |> (fun arr -> arr[0], arr[1])
    ||> Array.zip
    |> Array.map (fun (a,b) ->  a, b)

let file2 = 
    File.ReadAllLines "in.txt" 
    |> Array.map (fun st -> st.Split(":")[1])
    |> Array.map (fun s -> s.Replace (" ", ""))
    |> (fun arr -> [| (int64 arr[0], int64 arr[1]) |])
    

let math ((t, d): (int64*int64)) =
    // let inner n =
        
    List.init ((int)t-1) (fun i -> (int64)i+1L)
    |> List.map (fun v -> v * (t-v))
    |> List.filter (fun v -> v > d)
    |> List.length

//file
//|> Array.map (math)
//|> Array.reduce (fun s v -> s * v)

file2
|> Array.map (math)
|> Array.reduce (fun s v -> s * v)
