(* Programming language concepts for software developers, 2010-08-28 *)

(* Representing object language expressions using recursive datatypes *)

module Intro1

type expr = 
  | CstI of int
  | Prim of string * expr * expr;;
  | If of expr * expr * expr;;

let e0 = Prim("+", CstI 2, Prim("*", CstI 3, CstI 4))

let e1 = CstI 17;;

let e2 = Prim("-", CstI 3, CstI 4);;

let e3 = Prim("+", Prim("*", CstI 7, CstI 9), CstI 10);;


(* Evaluating expressions using recursive functions *)

let rec eval (e : expr) : int =
    match e with
    | CstI i -> i
    | Prim("+", e1, e2) -> eval e1 + eval e2
    | Prim("*", e1, e2) -> eval e1 * eval e2
    | Prim("-", e1, e2) -> eval e1 - eval e2
    | Prim("max", e1, e2) -> if eval e1 > eval e2 then eval e1 else if eval e1 < eval e2 then eval e2 else failwith "equal expressions" 
    | Prim("min", e1, e2) -> if eval e1 < eval e2 then eval e1 else if eval e1 > eval e2 then eval e2 else failwith "equal expressions" 
    | Prim("==", e1, e2) -> if eval e1 = eval e2 then 1 else 0
    | If (e1, e2, e3) -> 

let rec evalOpe e (env : (string * int) list) : int =
    match e with
    | CstI i -> i
    | Prim(ope, e1, e2) -> 
        let i1 = evalOpe e1
        let i2 = evalOpe e2
        match ope with
        | "+" -> i1 + i2
        | "-" -> i1 - i2
        | "*" -> i1 * i2
        | "max" -> if i1 > i2 then i1 else if i2 > i1 then i2 else failwith "equal expressions"  
        | "min" -> if i1 < i2 then i1 else if i2 < i1 then i2 else failwith "equal expressions"  
        | "=="  -> if i1 = i2 then 1 else 0  
  
let e0v = eval e0;;
let e1v = eval e1;;
let e2v = eval e2;;
let e3v = eval e3;;

let example0 = Prim("max", CstI 3, Prim("+", CstI 4, CstI 5))
let example1 = Prim ("min", Prim("-", CstI 3, CstI 1), CstI 3)
let example2 = Prim ("==", CstI 6, Prim("*", CstI 2, CstI 3))

let example0v = eval example0
let example1v = eval example1
let example2v = eval example2



(* Changing the meaning of subtraction *)

let rec evalm (e : expr) : int =
    match e with
    | CstI i -> i
    | Prim("+", e1, e2) -> evalm e1 + evalm e2
    | Prim("*", e1, e2) -> evalm e1 * evalm e2
    | Prim("-", e1, e2) -> 
      let res = evalm e1 - evalm e2
      if res < 0 then 0 else res 
    | Prim _            -> failwith "unknown primitive";;


let e4v = evalm (Prim("-", CstI 10, CstI 27));;

(* The Pretty Printer function *)

let rec fmt (e : expr) : string =
  match e with
    CstI i -> i.ToString()
  | Prim("+", e1, e2) -> "(" + fmt e1 + "+" + fmt e2 + ")"
  | Prim("*", e1, e2) -> "(" + fmt e1 + "*" + fmt e2 + ")"
  | Prim("-", e1, e2) -> "(" + fmt e1 + "-" + fmt e2 + ")"
  | Prim _            -> failwith "fmt: unknown primitive";;
  
