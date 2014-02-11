﻿using System;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats.Corrupted
{
    public class CorruptedReader : IFormatReader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CorruptedReader" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="exception">The inner exception.</param>
        public CorruptedReader(Stream stream, Exception exception)
        {
            this.Stream = stream;
            this.Exception = exception;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Exception { get; private set; }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
        #endregion

        #region Implementation of IFormatReader
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        /// <summary>
        /// Begins the specified section.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public IDisposable Section(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads an unsigned integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public uint ReadUnsignedInteger(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads an unsigned short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ushort ReadUnsignedShort(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads an unsigned long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ulong ReadUnsignedLong(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads an integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public int ReadInteger(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public short ReadShort(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public long ReadLong(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a float value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public float ReadFloat(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a double value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public double ReadDouble(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a decimal value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public decimal ReadDecimal(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public bool ReadBoolean(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a byte value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public byte ReadByte(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a byte array value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public byte[] ReadBytes(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a character value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public char ReadCharacter(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public string ReadString(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a vector2 value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public Vector2 ReadVector2(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        /// <summary>
        /// Reads a rectangle value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public Rectangle ReadRectangle(string tag)
        {
            throw new CorruptedException(this.Exception);
        }
        #endregion
    }
}