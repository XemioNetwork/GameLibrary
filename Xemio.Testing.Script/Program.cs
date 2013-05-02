using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xemio.GameLibary.Script;

namespace Xemio.Testing.Script
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptCompiler compiler = new ScriptCompiler(new List<ICommand>() {new TestCommand()});

            compiler.Assemblies.Add("XGL.dll");
            compiler.OutputAssembly = "test.dll";

            object[] instances = compiler.Compile(@"
                               using System;
                               using System.Collections;
                               using Xemio.GameLibary.Script;
                               using Xemio.GameLibary.Script.Commands;

                               public class Test : IScript
                               {
                                    public IEnumerable Execute()
                                    {
                                        Test(3);
                                        Test(15);
                                        Test(1);
                                    }
                               }");

            if (!compiler.Succeed)
            {
                foreach(CompilerError a in compiler.Errors)
                    Console.WriteLine(a.ErrorText);
            }

            foreach (IScript script in instances)
                foreach (ICommand command in script.Execute())
                    command.Execute();

            Console.ReadLine();
        }
    }
}
