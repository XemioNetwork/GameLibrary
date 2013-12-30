using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats
{
    public interface IFormatReader : IDisposable
    {
        /// <summary>
        /// Gets the stream.
        /// </summary>
        Stream Stream { get; }
        /// <summary>
        /// Begins the specified section.
        /// </summary>
        /// <param name="tag">The tag.</param>
        IDisposable Section(string tag);
        /// <summary>
        /// Reads an unsigned integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        uint ReadUnsignedInteger(string tag);
        /// <summary>
        /// Reads an unsigned short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        ushort ReadUnsignedShort(string tag);
        /// <summary>
        /// Reads an unsigned long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        ulong ReadUnsignedLong(string tag);
        /// <summary>
        /// Reads an integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        int ReadInteger(string tag);
        /// <summary>
        /// Reads a short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        short ReadShort(string tag);
        /// <summary>
        /// Reads a long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        long ReadLong(string tag);
        /// <summary>
        /// Reads a float value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        float ReadFloat(string tag);
        /// <summary>
        /// Reads a double value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        double ReadDouble(string tag);
        /// <summary>
        /// Reads a decimal value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        decimal ReadDecimal(string tag);
        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        bool ReadBoolean(string tag);
        /// <summary>
        /// Reads a byte value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        byte ReadByte(string tag);
        /// <summary>
        /// Reads a byte array value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        byte[] ReadBytes(string tag);
        /// <summary>
        /// Reads a character value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        char ReadCharacter(string tag);
        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        string ReadString(string tag);
        /// <summary>
        /// Reads a vector2 value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        Vector2 ReadVector2(string tag);
        /// <summary>
        /// Reads a rectangle value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        Rectangle ReadRectangle(string tag);
    }
}
