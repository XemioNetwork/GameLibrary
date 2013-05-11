﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xemio.GameLibrary.Script;

namespace Xemio.Testing.Script
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptCompiler compiler = new ScriptCompiler(new List<ICommand>() {new TestCommand()});

            compiler.Assemblies.Add("XGL.dll");
            compiler.OutputAssembly = "test.dll";

            CompilerResult result = compiler.Compile(@"
                               using System;
                               using System.Collections;
                               using Xemio.GameLibrary.Script;

                               public class Test : IScript
                               {
                                    public IEnumerable Execute()
                                    {
                                        Test(3);
                                        Test(15);
                                        Test(1);
                                    }
                               }");

            if (!result.Succeed)
            {
                foreach(CompilerError a in result.Errors)
                    Console.WriteLine(a.Message);
            }

            foreach (IScript script in result.Scripts)
                foreach (ICommand command in script.Execute())
                    command.Execute();

            Console.ReadLine();
        }
    }
}