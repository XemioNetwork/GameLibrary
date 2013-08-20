using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats
{
    public interface IFormatReader
    {
        /// <summary>
        /// Gets the stream.
        /// </summary>
        Stream Stream { get; }
        /// <summary>
        /// Begins a new section.
        /// </summary>
        void BeginSection();
        /// <summary>
        /// Ends the current section.
        /// </summary>
        void EndSection();
        /// <summary>
        /// Reads an unsigned integer value.
        /// </summary>
        uint ReadUnsignedInteger();
        /// <summary>
        /// Reads an unsigned short value.
        /// </summary>
        ushort ReadUnsignedShort();
        /// <summary>
        /// Reads an unsigned long value.
        /// </summary>
        ulong ReadUnsignedLong();
        /// <summary>
        /// Reads an integer value.
        /// </summary>
        int ReadInteger();
        /// <summary>
        /// Reads a short value.
        /// </summary>
        short ReadShort();
        /// <summary>
        /// Reads a long value.
        /// </summary>
        long ReadLong();
        /// <summary>
        /// Reads a float value.
        /// </summary>
        float ReadFloat();
        /// <summary>
        /// Reads a double value.
        /// </summary>
        double ReadDouble();
        /// <summary>
        /// Reads a decimal value.
        /// </summary>
        decimal ReadDecimal();
        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        bool ReadBoolean();
        /// <summary>
        /// Reads a byte value.
        /// </summary>
        byte ReadByte();
        /// <summary>
        /// Reads a byte array value.
        /// </summary>
        /// <param name="length">The length.</param>
        byte[] ReadBytes(int length);
        /// <summary>
        /// Reads a character value.
        /// </summary>
        char ReadCharacter();
        /// <summary>
        /// Reads a string value.
        /// </summary>
        string ReadString();
        /// <summary>
        /// Reads a vector2 value.
        /// </summary>
        Vector2 ReadVector2();
        /// <summary>
        /// Reads a rectangle value.
        /// </summary>
        Rectangle ReadRectangle();
    }
}
