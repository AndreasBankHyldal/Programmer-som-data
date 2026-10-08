### Exercise 7.4

We have edittet Absyn.fs and Interp.fs to extend the two types in the eval function of Interp.fs (line 169) and the expr type in Absyn.fs (line 26).

### Exercise 7.5

We have edittet CLex.fsl and CPar.fsy. We have extended CLex with the token rules on line 54, and we have extended CPar.fsy with the tokens (line 21), precedence (line 32) and extended at line 146 in AtExprNotAccess aswell.

### Exercise 8.1

(i) We compiled and ran `ex11.c` with argument 8, producing 92 solutions to the eight-queens problem.

(ii) We compiled `ex03.c` and `ex05.c`; their annotated symbolic bytecode is in `ex3bytecode.txt` and `ex5bytecode.txt`. With argument 10, they print `0 1 2 3 4 5 6 7 8 9` and `100 10`, respectively. In `ex05`, the inner `r` uses a separate stack slot, allocated by `INCSP 1` and discarded when its block ends.

`ex3trace.txt` shows execution with argument 4. The stack frame contains the return address, saved base pointer, `n`, and `i`; temporary operands appear above them. `LDI` reads variables, `STI` updates them, and `LT`/`IFNZRO` implement the loop, which prints `0 1 2 3` before returning.

### Exercise 8.3

We added `PreInc` and `PreDec` cases to `cExpr` in `Comp.fs`. The compiler computes the address once, duplicates it, loads the value, adds/subtracts 1, and stores the result. `STI` leaves the updated value on the stack, so nested expressions work without evaluating side effects twice.

`ex83.c` checks both operators, including `++arr[++i]` and `--arr[--i]`. The output confirms that `i` changes only once per expression and only the selected array element is updated.

### Exercise 8.4

We compiled `ex08.c` and `ex13.c` and studied their symbolic bytecode. The loop in `ex08` executes 17 instructions per iteration, compared with 4 in `prog1`, because it repeatedly calculates the variable's address and loads/stores its value. The handwritten loop keeps its counter directly on the stack.

In `ex13`, the while loop surrounds an if statement whose `&&` and `||` operators use short-circuit jumps. The compiler introduces several labels and intermediate Boolean results before deciding whether to print `y`, resulting in extra jumps and no-op block cleanup instructions.