using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractBF
{
    internal class Compiler
    {
        bool safe_optimizations = true;
        bool codemarks = true;

        public Compiler(bool safe_optimizations=true, bool codemarks=true) {
            this.safe_optimizations = safe_optimizations;
            this.codemarks = codemarks;
        }

        public string CompileLine(string line)
        {
            string res = "";


            if(line == "p++")
            {
                res += ">";
            } 
            else if (line == "p--")
            {
                res += "<";
            }
            else if (line == "*p++")
            {
                res += "+";
            }
            else if (line == "*p--")
            {
                res += "-";
            }
            else if (line == "whilenz [")
            {
                res += "[";
            }
            else if (line == "]")
            {
                res += "]";
            }
            else if (line == "putchar(*p)")
            {
                res += ".";
            }
            else if (line == "getchar()")
            {
                res += ",";
            }
            else
            {
                if (line.StartsWith("putchar('"))
                {
                    if (line.EndsWith("')"))
                    {
                        string line_buf = line;
                        line_buf = line_buf.Remove(0, 9);
                        char ch;
                        ch = line_buf[0];
                        if (ch == '\'')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: no putchar character");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            System.Environment.Exit(1);
                        }
                        if (this.codemarks) res += "constdata";
                        res += "[-]";
                        for (char i = (char)0; i < ch; i++)
                        {
                            res += "+";
                        }
                        res += ".";
                    }
                }
                else if (line.StartsWith("puts(\""))
                {
                    if (line.EndsWith("\")"))
                    {
                        string line_buf = line;
                        line_buf = line_buf.Remove(0, 6);
                        int idx = 0;
                        char ch = line_buf[idx];
                        char prev_ch = (char)0;
                        if (this.codemarks) res += "constdata";
                        while (ch != '"')
                        {

                            if (prev_ch == 0 || (!this.safe_optimizations))
                            {
                                res += "[-]";
                                for (char i = (char)0; i < ch; i++)
                                {
                                    res += "+";
                                }
                                res += ".";
                            }
                            else if (ch > prev_ch && this.safe_optimizations)
                            {
                                for (char i = (char)0; i < ch - prev_ch; i++)
                                {
                                    res += "+";
                                }
                                res += ".";
                            }
                            else if (ch < prev_ch && this.safe_optimizations)
                            {
                                for (char i = (char)0; i < prev_ch - ch; i++)
                                {
                                    res += "-";
                                }
                                res += ".";
                            } else if (ch == prev_ch)
                            {
                                res += ".";
                            }

                            prev_ch = ch;

                            idx++;
                            ch = line_buf[idx];
                        }
                    }
                }
                else if (line.StartsWith("*p = "))
                {
                    res += "[-]";
                    for (int i = 0; i < int.Parse(line.Remove(0, 5)); i++)
                    {
                        res += "+";
                    }
                }
                else if (line.StartsWith("*p += "))
                {
                    for (int i = 0; i < int.Parse(line.Remove(0, 6)); i++)
                    {
                        res += "+";
                    }
                }
                else if (line.StartsWith("*p -= "))
                {
                    for (int i = 0; i < int.Parse(line.Remove(0, 6)); i++)
                    {
                        res += "-";
                    }
                }
                else if (line.StartsWith("*p *= "))
                {
                    res += ">[-]<";
                    res += "[>";
                    for (int i = 0; i < int.Parse(line.Remove(0, 6)); i++)
                    {
                        res += "+";
                    }
                    res += "<-]";
                    res += ">[<+>-]<";
                }
                else if (line.StartsWith("p += "))
                {
                    for (int i = 0; i < int.Parse(line.Remove(0, 5)); i++)
                    {
                        res += ">";
                    }
                }
                else if (line.StartsWith("p -= "))
                {
                    for (int i = 0; i < int.Parse(line.Remove(0, 5)); i++)
                    {
                        res += "<";
                    }
                }
                else if (line.StartsWith("ldstr \""))
                {
                    if (line.EndsWith("\""))
                    {
                        string line_buf = line;
                        line_buf = line_buf.Remove(0, 7);
                        int idx = 0;
                        char ch = line_buf[idx];

                        if (this.codemarks) res += "constdata";
                        while (ch != '"')
                        {
                            res += "[-]";
                            for (char i = (char)0; i < ch; i++)
                            {
                                res += "+";
                            }
                            res += ">";

                            idx++;
                            ch = line_buf[idx];
                        }
                        res += "<";
                    }
                } else if(line.StartsWith("#"))
                {
                    
                    char[] prohibited_chars = {'.', ',', '+', '-', '[', ']', '>', '<'};
                    for(int i=0; i< prohibited_chars.Length; i++)
                    {
                        if (line.Contains(prohibited_chars[i]))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: command in comments");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            System.Environment.Exit(1);
                        }
                    }
                    line = line.Replace("\\n", "\n");
                    res += line.Remove(0, 1);
                }
            }

            return res;
        }

        public string CompileFullCode(string code)
        {
            string res="";
            string[] splitted = code.Split('\n');

            for (int i=0; i<splitted.Length; i++)
            {
                res += this.CompileLine(splitted[i]);
            }

            return res;
        }
    }
}
