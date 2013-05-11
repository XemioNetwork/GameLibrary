using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.UI.CSS.Namespaces;

namespace Xemio.Testing.CSS
{
    class Program
    {
        static void Main(string[] args)
        {
            Namespace namespace1 = new Namespace(".test:Hover #id #a element");
            Namespace namespace2 = new Namespace(".a .b .c");

            Console.WriteLine(namespace1.Name);
            Console.WriteLine("Depth={0}", namespace1.Depth);
            Console.WriteLine();

            Console.WriteLine(namespace2.Name);
            Console.WriteLine("Depth={0}", namespace2.Depth);

            Console.ReadLine();
        }
    }
}
