# AbstractBF
Abstract brainfck compiler with c-like syntax 

Syntax (no semicolons):
`p++` move pointer to right (>)<br>
`p--` move pointer to left (<)<br>
`p += 4` move pointer to right by some value (>)<br>
`p -= 4` move pointer to left by some value (<)<br>
`*p++` increment pointer (+)<br>
`*p--` decrease pointer (-)<br>
`whilenz [` declare non-zero loop ([)<br>
`]` end non-zero loop (])<br>
`putchar(*p)` print pointer value as char<br>
`putchar('A')` print constant char (caution: will override current pointer)<br>
`puts("Hello, world!")` print constant string (caution: will override current pointer, also produces very long output but still has some optimization to that)<br>
`*p = 5` set pointer value<br>
`*p *= 3` multiply current pointer value by constant value (caution: will override next and current pointer)<br>
`*p += 3` add to current pointer value
`*p -= 3` remove from current pointer value
