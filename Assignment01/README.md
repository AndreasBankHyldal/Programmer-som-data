Exercise 1.1
(i) We pattern match on the two expressions and return either min or max, following the same pattern as the other cases
(ii) We have written examples showing that our new expressions in the eval function works
(iii) We added an evalOpe function that follows the same principles as the original eval function to evaluate the arguments of a primitive
before branching out on the operator.
(iv) We added if to the type expr
(v) We added both If to the expr type that takes three expressions, and then we added the case to the eval function

Exercise 1.2
(i) We declared the aexpr datatype with constructors for constants, variables, addition, multiplication, and subtraction
(ii) We wrote representations of the three arithmetic expressions using the aexpr constructors
(iii) We added a fmt function that converts an aexpr into a formatted string
(iv) We added a simplify function that recursively simplifies arithmetic expressions using pattern matching
(v) We added a diff function that performs symbolic differentiation, including the product rule for multiplication

Exercise 1.4
(i) We created a Java class hierarchy for arithmetic expressions and added toString methods for formatting them
(ii) We created and printed three arithmetic expressions using the new classes
(iii) We added eval methods that evaluate expressions using a map as the environment
(iv) We added simplify methods that recursively perform the same algebraic simplifications as in Exercise 1.2
