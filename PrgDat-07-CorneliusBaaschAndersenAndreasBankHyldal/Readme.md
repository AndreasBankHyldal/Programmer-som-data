### Exercise 8.5

We editted Absyn.fs on line 26, CLex.fsl on line 79-80, Comp.fs on line 216 and CPar.fsy on line 140.

### Exercise 8.6

We edited Absyn.fs on line 41, CLex.fsl on lines 32–33, Comp.fs starting at line 146, and CPar.fsy on line 22 and lines 106–107 and 117–125.

### Exercise 9.1

We compiled the C# version with optimization and disassembled it using ilspycmd. We compiled the Java version using javac and disassembled it using javap.
In Selsort.il and Selsort.jvmbytecode, we kept only the SelectionSort method and added comments explaining the instructions and their corresponding source variables.

### Exercise 9.3

The original code throws OutOfMemoryError. The field dummy always points to the first node, so all removed nodes stay reachable through the next chain and are never garbage collected.

Fix: remove dummy and create the sentinel node in the constructor (head = tail = new Node(-444, null)). Old nodes then become unreachable once head moves forward, and the program runs without errors.
