using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Xemio.GameLibrary.Content.FileSystem.Virtualization
{
    internal class VirtualDirectory
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDirectory"/> class.
        /// </summary>
        protected VirtualDirectory()
        {
            this.Directories = new List<VirtualDirectory>();
            this.Files = new List<VirtualFile>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDirectory"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public VirtualDirectory(string name) : this()
        {
            this.Name = name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the full path.
        /// </summary>
        public string FullPath
        {
            get
            {
                if (this.Name == ".")
                {
                    return this.Name;
                }

                return this.Parent.FullPath + "/" + this.Name;
            }
        }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public VirtualDirectory Parent { get; set; }
        /// <summary>
        /// Gets the directories.
        /// </summary>
        public List<VirtualDirectory> Directories { get; private set; }
        /// <summary>
        /// Gets the files.
        /// </summary>
        public List<VirtualFile> Files { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void Add(VirtualDirectory directory)
        {
            directory.Parent = this;
            this.Directories.Add(directory);
        }
        /// <summary>
        /// Adds the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Add(VirtualFile file)
        {
            file.Parent = this;
            this.Files.Add(file);
        }
        /// <summary>
        /// Gets a directory.
        /// </summary>
        /// <param name="name">The name.</param>
        public VirtualDirectory GetDirectory(string name)
        {
            return this.Directories.FirstOrDefault(dir => dir.Name == name);
        }
        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public VirtualFile GetFile(string fileName)
        {
            return this.Files.FirstOrDefault(file => file.Name == fileName);
        }
        /// <summary>
        /// Writes all directories and files to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Save(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(this.Name);
            writer.Write(this.Files.Count);
            writer.Write(this.Directories.Count);

            foreach (VirtualFile file in this.Files)
            {
                file.Save(stream);
            }
            foreach (VirtualDirectory directory in this.Directories)
            {
                directory.Save(stream);
            }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a directory from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public static VirtualDirectory FromStream(Stream stream)
        {
            VirtualDirectory directory = new VirtualDirectory();
            BinaryReader reader = new BinaryReader(stream);

            directory.Name = reader.ReadString();

            int fileCount = reader.ReadInt32();
            int directoryCount = reader.ReadInt32();
            
            for (int i = 0; i < fileCount; i++)
            {
                directory.Add(VirtualFile.FromStream(stream));
            }
            for (int i = 0; i < directoryCount; i++)
            {
                directory.Add(VirtualDirectory.FromStream(stream));
            }

            return directory;
        }
        #endregion

        #region Object Member
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return this.FullPath;
        }
        #endregion
    }
}
