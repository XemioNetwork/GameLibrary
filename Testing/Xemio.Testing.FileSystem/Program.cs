using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.FileSystem.Virtualization;

namespace Xemio.Testing.FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            VirtualFileSystem fileSystem = new VirtualFileSystem();

            fileSystem.Create("a/test.txt");
            //fileSystem.CreateDirectory("a");
            fileSystem.Create("a/data.txt");

            using (Stream stream = fileSystem.Open("a/data.txt"))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write("Hallo Welt");
            }

            Console.WriteLine("Files and Directories:");
            ListFiles(fileSystem, ".");

            Console.WriteLine();
            using (Stream stream = fileSystem.Open("./a/data.txt"))
            {
                BinaryReader reader = new BinaryReader(stream);

                Console.WriteLine("File content for 'a/data.txt':");
                Console.WriteLine(reader.ReadString());
            }

            fileSystem.Save("main.fcon");
            Console.ReadLine();
        }

        static void ListFiles(IFileSystem fileSystem, string dir)
        {
            foreach (string fileName in fileSystem.GetFiles(dir))
            {
                Console.WriteLine(fileName);
            }
            foreach (string directory in fileSystem.GetDirectories(dir))
            {
                Console.WriteLine(directory);
                ListFiles(fileSystem, directory);
            }
        }
    }
}
