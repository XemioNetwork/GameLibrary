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
                                        Values = new List<LanguageValue>
                                                     {
                                                         new LanguageValue
                                                             {
                                                                 Id = "MyString",
                                                                 Localized = "Mein Text" + Environment.NewLine + "Günther"
                                                             },
                                                         new LanguageValue
                                                             {
                                                                 Id = "AnotherString",
                                                                 Localized = "Ein anderer Text"
                                                             }
                                                     }
                                    };

            var serializer = new LanguageSerializer();

            using (BinaryReader writer = new BinaryReader(File.OpenRead("testfile.dat")))
            {
                var anotherLanguage = serializer.Read(writer);
            }

            Console.ReadLine();
        }
    }
}
