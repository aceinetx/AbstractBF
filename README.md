# AbstractBF
Abstract brainfck with c-like syntax 

Syntax:

`p++` move pointer to right (>)<br>
`p--` move pointer to left (<)<br>
`*p++` increment pointer (+)<br>
`*p--` decrease pointer (-)<br>
`whilenz [` declare non-zero loop ([)
`]` end non-zero loop
`putchar(*p)` print pointer value as char (.)
`putchar('A')` print constant char (caution: will override current pointer)
`puts("Hello, world!")` print constant string (caution: will override current pointer)
`*p = 5` set pointer value
