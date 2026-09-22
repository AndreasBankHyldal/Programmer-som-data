### Exercise 4.5

We extended the FunLex.fsl and FunPar.fsy to support AND OR operators.

In FunLex.fsl: We added the the operators in rule token on lines 58-59.

In FunPar.fsy: We added AND OR as a token on line 14 and we added %left precedence for AND OR on line 22-23, and then we added the Expr on line 58-59 as descripted in the exercise.