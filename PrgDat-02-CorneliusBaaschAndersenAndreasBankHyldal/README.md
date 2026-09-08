### exercise 2.4
Written assembler in IntComp1.fs on line 353.

### exercsie 2.5

Written assemblerToFile in IntComp1.fs on line 414.

In Machine.java we have: 
- added imports on line 19
- Replaced hardcoded arguments in Main
- Added helper readfile

### Exercise 3.2

First the regular expression:   

-  (b | ab)*a?
    
Second the NFA:

-   | Tilstand | a | b | ε |
    |---|---|---|---|
    | → 8 | – | – | 6, 9 |
    | 6 | – | – | 1, 3 |
    | 1 | – | 2 | – |
    | 2 | – | – | 7 |
    | 3 | 4 | – | – |
    | 4 | – | 5 | – |
    | 5 | – | – | 7 |
    | 7 | – | – | 6, 9 |
    | 9 | – | – | 12 |
    | 12 | – | – | 10, 13 |
    | 10 | 11 | – | – |
    | 11 | – | – | 13 |
    | *13 | – | – | – |

Third the DFA:

- | Navn | Mængde af NFA-tilstande | a | b | Accept |
  |---|---|---|---|---|
  | → A | {1, 3, 7, 9, 11, 12, 13, 14} | B | C | ja |
  | B | {4, 5, 8, 14} | D | E | ja |
  | C | {1, 2, 3, 7, 9, 10, 12, 13, 14} | B | C | ja |
  | D | {} | D | D | nej |
  | E | {1, 3, 6, 7, 9, 10, 12, 13, 14} | B | C | ja |
