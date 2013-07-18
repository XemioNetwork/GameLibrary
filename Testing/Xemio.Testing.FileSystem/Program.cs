using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.FileSystem.Containers;
using Xemio.GameLibrary.Content.FileSystem.Virtualization;

namespace Xemio.Testing.FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerFileSystem fileSystem = new ContainerFileSystem();
            fileSystem.RootDirectory = "./Resources/";

            fileSystem.Create("test.dat://a/test.txt");
            fileSystem.Create("test.dat://a/data.txt");

            using (Stream stream = fileSystem.Open("test.dat://a/data.txt"))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write("Hallo Welt");
            }

            Console.WriteLine("Files and RootDirectories:");
            ListFiles(fileSystem, "test.dat://.");

            Console.WriteLine();
            using (Stream stream = fileSystem.Open("test.dat://a/data.txt"))
            {
                BinaryReader reader = new BinaryReader(stream);

                Console.WriteLine("File content for 'a/data.txt':");
                Console.WriteLine(reader.ReadString());
            }

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
