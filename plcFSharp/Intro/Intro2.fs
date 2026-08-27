(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;


(* Object language expressions with variables *)

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr;;

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i              -> i
    | Var x               -> lookup env x 
    | Prim("+", e1, e2)   -> eval e1 env + eval e2 env
    | Prim("*", e1, e2)   -> eval e1 env * eval e2 env
    | Prim("-", e1, e2)   -> eval e1 env - eval e2 env
    | Prim("max", e1, e2) -> if eval e1 env > eval e2 env then eval e1 env else eval e2 env
    | Prim("min", e1, e2) -> if eval e1 env < eval e2 env then eval e1 env else eval e2 env
    | Prim("==", e1, e2)  -> if eval e1 env = eval e2 env then 1 else 0
    | If(e1, e2, e3)      -> if eval e1 env <> 0 then eval e2 env else eval e3 env
    | Prim _              -> failwith "unknown primitive";;

let e1v  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;


(* Exercise 1.1 (iii): evaluate both arguments before branching on the operator *)

let rec evalOpe e (env : (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var x  -> lookup env x
    | If(e1, e2, e3) ->
        if evalOpe e1 env <> 0 then evalOpe e2 env else evalOpe e3 env
    | Prim(ope, e1, e2) ->
        let i1 = evalOpe e1 env
        let i2 = evalOpe e2 env
        match ope with
        | "+"   -> i1 + i2
        | "-"   -> i1 - i2
        | "*"   -> i1 * i2
        | "max" -> if i1 > i2 then i1 else i2
        | "min" -> if i1 < i2 then i1 else i2
        | "=="  -> if i1 = i2 then 1 else 0
        | _     -> failwith ("unknown primitive: " + ope);;


(* 1.1 ii *)

let example0 = Prim("max", CstI 3, Prim("+", CstI 4, CstI 5));;
let example1 = Prim("min", Prim("-", CstI 3, CstI 1), CstI 3);;
let example2 = Prim("==", CstI 6, Prim("*", CstI 2, CstI 3));;
let example3 = Prim("max", Var "a", Prim("-", Var "b", CstI 100));;
let example4 = Prim("==", Var "a", CstI 7);;
let example5 = If(Var "a", CstI 11, CstI 22);;

let example0v = eval example0 env;;   (* 9  *)
let example1v = eval example1 env;;   (* 2  *)
let example2v = eval example2 env;;   (* 1  *)
let example3v = eval example3 env;;   (* 11 *)
let example4v = eval example4 env;;   (* 0  *)
let example5v = eval example5 env;;   (* 11 *)

(* The two interpreters must agree on every expression *)
let agree0 = eval example0 env = evalOpe example0 env;;
let agree5 = eval example5 env = evalOpe example5 env;;


(* Changing the meaning of subtraction: negative results become zero *)

let rec evalm e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x
    | Prim("+", e1, e2) -> evalm e1 env + evalm e2 env
    | Prim("*", e1, e2) -> evalm e1 env * evalm e2 env
    | Prim("-", e1, e2) -> 
      let res = evalm e1 env - evalm e2 env
      if res < 0 then 0 else res 
    | If(e1, e2, e3)    -> if evalm e1 env <> 0 then evalm e2 env else evalm e3 env
    | Prim _            -> failwith "unknown primitive";;

let e4v = evalm (Prim("-", CstI 10, CstI 27)) env;;


(* The pretty printer: no environment needed, a variable's name is
   independent of its value *)

let rec fmt (e : expr) : string =
  match e with
  | CstI i              -> i.ToString()
  | Var x               -> x
  | Prim("+", e1, e2)   -> "(" + fmt e1 + " + " + fmt e2 + ")"
  | Prim("*", e1, e2)   -> "(" + fmt e1 + " * " + fmt e2 + ")"
  | Prim("-", e1, e2)   -> "(" + fmt e1 + " - " + fmt e2 + ")"
  | Prim("max", e1, e2) -> "max(" + fmt e1 + ", " + fmt e2 + ")"
  | Prim("min", e1, e2) -> "min(" + fmt e1 + ", " + fmt e2 + ")"
  | Prim("==", e1, e2)  -> "(" + fmt e1 + " == " + fmt e2 + ")"
  | If(e1, e2, e3)      -> "(if " + fmt e1 + " then " + fmt e2 + " else " + fmt e3 + ")"
  | Prim _              -> failwith "fmt: unknown primitive";;

let fmt0 = fmt example0;;
let fmt3 = fmt example3;;
let fmt5 = fmt example5;;
