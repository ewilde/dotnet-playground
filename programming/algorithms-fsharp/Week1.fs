module Week1
open System.IO


let getInitialList = fun () ->
    File.ReadAllLines("p079_keylog.txt") 

let distinctheads (xs: string array) =
    xs 
    |> Array.filter(fun x -> x.Length > 0)
    |> Array.map(fun x -> x.[0])
    |> Seq.distinct
    |> Seq.toList  

let isAlwaysFirst (x: char, combinations: string array) =
    combinations 
    |> Array.exists(fun item -> item.IndexOf(x) > 0)
    |> not

let chomp (c: char, s: string) : string =
    match s with
        | str when str.Length = 0 -> ""
        | str when str.[0] = c -> str.Substring(1)
        | _ -> s

let computenext (combinations: string array) =
    match combinations.Length with
    | 0 -> ' '
    | _  ->  distinctheads (combinations) 
                |> List.filter(fun h -> isAlwaysFirst(h, combinations))
                |> List.head

let removenext (next: char, combinations: string array) =
    combinations
    |> Array.map(fun x -> chomp(next, x)) 
    |> Array.filter(fun x -> x.Length > 0)

let compute = fun () ->
    let rec calculate  (result: string, combs: string array) =
        match (combs.Length, computenext(combs)) with
            | (0, _) -> result
            | (_, next) -> calculate ( System.String.Concat(result, next),  removenext(next, combs))

    calculate ("", getInitialList())
    

let Computeaverage (input: string array) =     
    let rec calculate  (result: string, combs: string array) =
        match (combs.Length, computenext(combs)) with
            | (0, _) -> result
            | (_, next) -> calculate ( System.String.Concat(result, next),  removenext(next, combs))

    calculate ("", input)
    
