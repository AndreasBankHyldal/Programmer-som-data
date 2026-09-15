
### Exercise 3.5 
We have tried the parser on several example expressions, both well-formed and ill-formed ones from the book, and some of our own.

### Exercise 3.6

We have written compString function that parses a string to an expression to a list of operations on line 338.

### Exercise 3.7

We have extended Absyn.fs (l. 12), ExprLex.fsl (l. 22-24) ExprPar.fsy (l. 14, l. 38)

### Exercise 4.1

We have ran everything at tested examples from the readme

### Exercise 4.2

1. run (fromString "let sum n = if n = 0 then 0 else n + sum (n - 1) in sum 1000 end");;
2. run (fromString "let f n =  if n = 0 then 1 else 3 * f (n-1) in f 8 end");;
3. run (fromString "let s n = if n = 12 then 0 else (let f x =  if x = 0 then 1 else 3 * f (x-1) in f n end) + s (n + 1)in s 0 end");;
4. run (fromString "let s n = if n = 11 then 0 else (let f x = if x = 0 then 1 else n * f (x-1) in f 8 end) + s (n + 1) in s 1 end");;

### 