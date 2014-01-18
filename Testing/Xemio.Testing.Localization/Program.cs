using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Localization;

namespace Xemio.Testing.Localization
{
    class Program
    {
        static void Main(string[] args)
        {
            Language language = new Language
                                    {
                                        CultureName = "de-DE",
                                        Values = new List<LocalizedValue>
                                                     {
                                                         new LocalizedValue
                                                             {
                                                                 Id = "MyString",
                                                                 Value = "Mein Text" + Environment.NewLine + "Günther"
                                                             },
                                                         new LocalizedValue
                                                             {
                                                                 Id = "AnotherString",
                                                                 Value = "Ein anderer Text"
                                                             }
                                                     }
                                    };
            
            Console.ReadLine();
        }
    }
}
