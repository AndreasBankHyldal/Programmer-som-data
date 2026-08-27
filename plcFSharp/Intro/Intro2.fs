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

type aexpr = 
    | CstI of int
    | Var of string
    | Add of aexpr * aexprsj
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr

(* 1.2 (ii) *)
let e4 = Sub(Var "v", Add(Var "w", Var "z"))

let e5 = Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z")))

let e6 = Add(Var "x", Add(Var "y", Add(Var "z", Var "v")))


let rec fmt (a : aexpr) : string = 
    match a with
    | CstI i -> i.toString()
    | Var x -> x 
    | Add (a1, a2) -> "(" + fmt a1 + " + " + fmt a2 + ")"
    | Mul (a1, a2) -> "(" + fmt a1 + " * " + fmt a2 + ")"
    | Sub (a1, a2) -> "(" + fmt a1 + " - " + fmt a2 + ")"

let rec simplify (a : aexpr) : aexpr = 
    match a with 
    | Add(a1, a2) -> 
        match a1,a2 with 
        | 0 , a2 -> a2
        | a1 , 0 -> a1
        | _ -> Add(simplify a1, simplify a2)
    | Sub(a1,a2) -> 
        match a1,a2 with 
        | a1 , 0 -> a1
        | a1 , a2 when a1 = a2 -> 0
        | _ -> Sub(simplify a1, simplify a2)
    | Mul(a1,a2) -> 
        match a1,a2 with 
        | a1 , 1 -> a1
        | 1 , a2 -> a2
        | 0 , a2 -> 0
        | a1 , 0 -> 0
        | _ -> Mul(simplify a1, simplify a2)
   