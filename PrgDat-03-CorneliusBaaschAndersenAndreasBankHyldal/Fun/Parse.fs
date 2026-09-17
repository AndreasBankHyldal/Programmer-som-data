module Parse

open FSharp.Text.Lexing
open Absyn

let fromString (str : string) : expr =
    let lexbuf = LexBuffer<char>.FromString(str)

    try
        FunPar.Main FunLex.Token lexbuf
    with
    | exn ->
        let pos = lexbuf.EndPos

        failwithf
            "%s near line %d, column %d"
            exn.Message
            (pos.Line + 1)
            pos.Column