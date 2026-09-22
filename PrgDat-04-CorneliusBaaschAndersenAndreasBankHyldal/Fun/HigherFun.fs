(* A functional language with integers and higher-order functions *)

module HigherFun

open Absyn

type 'v env = (string * 'v) list

let rec lookup env x =
    match env with
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x = y then v else lookup r x

type value =
  | Int of int
  | Closure of string * string list * expr * value env

let rec eval (e : expr) (env : value env) : value =
    match e with
    | CstI i -> Int i
    | CstB b -> Int(if b then 1 else 0)
    | Var x  -> lookup env x
    | Prim(ope, e1, e2) ->
      let v1 = eval e1 env
      let v2 = eval e2 env
      match (ope, v1, v2) with
      | ("*", Int i1, Int i2) -> Int(i1 * i2)
      | ("+", Int i1, Int i2) -> Int(i1 + i2)
      | ("-", Int i1, Int i2) -> Int(i1 - i2)
      | ("=", Int i1, Int i2) -> Int(if i1 = i2 then 1 else 0)
      | ("<", Int i1, Int i2) -> Int(if i1 < i2 then 1 else 0)
      | _ -> failwith "unknown primitive or wrong type"
    | Let(x, eRhs, letBody) ->
      let xVal = eval eRhs env
      eval letBody ((x, xVal) :: env)
    | If(e1, e2, e3) ->
      match eval e1 env with
      | Int 0 -> eval e3 env
      | Int _ -> eval e2 env
      | _     -> failwith "eval If"
    | Letfun(f, parameters, fBody, letBody) ->
      if List.isEmpty parameters then
        failwith "eval Letfun: a function must have at least one parameter"

      let closure = Closure(f, parameters, fBody, env)
      eval letBody ((f, closure) :: env)
    | Call(eFun, arguments) ->
      match eval eFun env with
      | Closure(f, parameters, fBody, fDeclEnv) as fClosure ->
        if List.length parameters <> List.length arguments then
          failwithf "eval Call: %s expects %d argument(s), but got %d"
            f (List.length parameters) (List.length arguments)

        let argumentValues = List.map (fun argument -> eval argument env) arguments
        let parameterBindings = List.zip parameters argumentValues
        let fBodyEnv = parameterBindings @ ((f, fClosure) :: fDeclEnv)
        eval fBody fBodyEnv
      | _ ->
        failwith "eval Call: not a function"

let run e = eval e []
