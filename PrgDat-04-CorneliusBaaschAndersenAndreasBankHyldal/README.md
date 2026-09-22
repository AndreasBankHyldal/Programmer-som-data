### Exercise 4.5

We extended the FunLex.fsl and FunPar.fsy to support AND OR operators.

In FunLex.fsl: We added the the operators in rule token on lines 58-59.

In FunPar.fsy: We added AND OR as a token on line 14 and we added %left precedence for AND OR on line 22-23, and then we added the Expr on line 58-59 as descripted in the exercise.


### Exercise 5.7

We extended TypedFun.fs to support the typeL in type on line 40. Then in tyexpr we added the ListExpr to have the tyexpr list on line 54. Then typ function we added ListExpr in the match case to check if every element in a list has an element type. 

### Exercise 6.1
The first three programs evaluate to `Int 7`.

The third result is expected. When `add 2` is evaluated, it returns
the function `f` as a closure. This closure captures the declaration
environment in which `x` is bound to `2`. The later binding
`let x = 77` therefore does not affect `addtwo`. This demonstrates
lexical scoping.

The fourth program returns a closure because `add 2` supplies the
argument for `x`, but the returned function `f` still needs an argument
for `y`. The closure contains the function body `x + y` together with
the captured binding `x = 2`.

### Exercise 6.2

Anonymous functions were added to the abstract syntax using:

`Fun of string * expr`

Evaluating a `Fun` expression creates a non-recursive `Clos` value
containing its parameter, body, and declaration environment. When the
closure is called, the argument is evaluated and bound to the parameter
in the captured environment.

For example, applying `fun x -> 2*x` to `21` evaluates to `Int 42`.


### Exercise 6.3

The lexer and parser were extended to support anonymous functions using
the concrete syntax `fun x -> expression`.

The lexer recognizes `fun` as the `FUN` token and `->` as the `ARROW`
token. The parser converts `fun x -> expression` into the abstract
syntax `Fun("x", expression)`.

Both of the following programs evaluate to `Int 7`:

`let add x = fun y -> x+y in add 2 5 end`

`let add = fun x -> fun y -> x+y in add 2 5 end`