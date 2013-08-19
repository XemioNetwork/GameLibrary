using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats
{
    public class FormatlessReader : IFormatReader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatlessWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public FormatlessReader(Stream stream)
        {
            this.Stream = stream;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the exception.
        /// </summary>
        private Exception GetException()
        {
            return new NotSupportedException("The formatless reader doesn't support direct read- and write methods. Use the stream to access data instead.");
        }
        #endregion

        #region Implementation of IFormatReader
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        /// <summary>
        /// Begins a new section.
        /// </summary>
        public void BeginSection()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Ends the current section.
        /// </summary>
        public void EndSection()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads an unsigned integer value.
        /// </summary>
        public uint ReadUnsignedInteger()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads an unsigned short value.
        /// </summary>
        public ushort ReadUnsignedShort()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads an unsigned long value.
        /// </summary>
        public ulong ReadUnsignedLong()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads an integer value.
        /// </summary>
        public int ReadInteger()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a short value.
        /// </summary>
        public short ReadShort()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a long value.
        /// </summary>
        public long ReadLong()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a float value.
        /// </summary>
        public float ReadFloat()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a double value.
        /// </summary>
        public double ReadDouble()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a decimal value.
        /// </summary>
        public decimal ReadDecimal()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        public bool ReadBoolean()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a byte value.
        /// </summary>
        public byte ReadByte()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a byte array value.
        /// </summary>
        /// <param name="length">The length.</param>
        public byte[] ReadBytes(int length)
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a character value.
        /// </summary>
        public char ReadCharacter()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a string value.
        /// </summary>
        public string ReadString()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a vector2 value.
        /// </summary>
        public Vector2 ReadVector2()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads a rectangle value.
        /// </summary>
        public Rectangle ReadRectangle()
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="type">The type.</param>
        public object ReadInstance(Type type)
        {
            throw this.GetException();
        }
        /// <summary>
        /// Reads all properties for the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public object ReadProperties(object instance)
        {
            throw this.GetException();
        }
        #endregion
    }
}
