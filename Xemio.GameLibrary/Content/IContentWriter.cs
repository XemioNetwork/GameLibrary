﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content
{
    public interface IContentWriter
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        Type Type { get; }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        void Write(BinaryWriter writer, object value);
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="value">The value.</param>
        void Write(string fileName, object value);
    }
}
