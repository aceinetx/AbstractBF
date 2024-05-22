using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractBF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AbstractBF v1.0");
            
            string file = File.ReadAllText(args[0]);

            Compiler compiler = new Compiler();
            Console.WriteLine(compiler.CompileFullCode(file));
        }
    }
}
