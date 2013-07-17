using System.IO;

namespace Xemio.GameLibrary.Content.FileSystem.Virtualization
{
    internal class VirtualFile
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualFile"/> class.
        /// </summary>
        protected VirtualFile()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualFile"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public VirtualFile(string name)
        {
            this.Name = name;
            this.Data = new byte[] {};
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the full path.
        /// </summary>
        public string FullPath
        {
            get { return this.Parent.FullPath + "/" + this.Name; }
        }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public VirtualDirectory Parent { get; set; }
        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] Data { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the stream.
        /// </summary>
        public Stream CreateStream()
        {
            return new VirtualFileStream(this);
        }
        /// <summary>
        /// Saves the specified file.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Save(Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(this.Name);
            writer.Write(this.Data.Length);
            writer.Write(this.Data);
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a file from stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public static VirtualFile FromStream(Stream stream)
        {
            VirtualFile file = new VirtualFile();
            BinaryReader reader = new BinaryReader(stream);
            
            file.Name = reader.ReadString();

            int length = reader.ReadInt32();
            file.Data = reader.ReadBytes(length);

            return file;
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
