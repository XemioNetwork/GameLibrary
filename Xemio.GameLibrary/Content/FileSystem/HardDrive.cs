﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content.FileSystem
{
    public class HardDrive : IFileSystem
    {
        #region IFileSystem Member
        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public virtual Stream Open(string fileName)
        {
            return new FileStream(fileName, FileMode.Open);
        }
        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public virtual Stream Create(string fileName)
        {
            return new FileStream(fileName, FileMode.Create);
        }
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public virtual void Delete(string fileName)
        {
            File.Delete(fileName);
        }
        /// <summary>
        /// Creates the specified directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public virtual void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
        /// <summary>
        /// Deletes the specified directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public virtual void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }
        /// <summary>
        /// Determines wether the specified file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public virtual bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }
        #endregion
    }
}