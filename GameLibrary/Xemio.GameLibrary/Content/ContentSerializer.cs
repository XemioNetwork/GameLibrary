using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content
{
    public abstract class ContentSerializer<T> : IContentSerializer where T : class
    {
        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type
        {
            get { return typeof (T); }
        }
        /// <summary>
        /// Gets the content.
        /// </summary>
        public ContentManager Content
        {
            get { return XGL.GetComponent<ContentManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public abstract T Read(BinaryReader reader);
        /// <summary>
        /// Reads a value out of the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public T Read(string fileName)
        {
            using (Stream stream = this.Content.FileSystem.Open(fileName))
            {
                BinaryReader reader = new BinaryReader(stream);
                return this.Read(reader);
            }
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tileset">The value.</param>
        public abstract void Write(BinaryWriter writer, T tileset);
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="tileset">The value.</param>
        public void Write(string fileName, T tileset)
        {
            using (Stream stream = this.Content.FileSystem.Create(fileName))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                this.Write(writer, tileset);
            }
        }
        #endregion

        #region IContentReader Member
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        object IContentReader.Read(BinaryReader reader)
        {
            return this.Read(reader);
        }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        object IContentReader.Read(string fileName)
        {
            return this.Read(fileName);
        }
        #endregion

        #region IContentWriter Member
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        void IContentWriter.Write(BinaryWriter writer, object value)
        {
            this.Write(writer, value as T);
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="value">The value.</param>
        void IContentWriter.Write(string fileName, object value)
        {
            this.Write(fileName, value as T);
        }
        #endregion
    }
}
