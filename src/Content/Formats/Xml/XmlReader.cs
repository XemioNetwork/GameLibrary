using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Content.Formats.Xml
{
    public class XmlReader : IFormatReader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlReader"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public XmlReader(Stream stream)
        {
            this.Stream = stream;
            
            this._currentElement = new XmlReaderElement(XDocument.Load(stream).Root);
            this._elements = new Stack<XmlReaderElement>();
        }
        #endregion

        #region Fields
        private XmlReaderElement _currentElement;
        private readonly Stack<XmlReaderElement> _elements;
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
            this._elements.Push(this._currentElement);
            this._currentElement = this._currentElement.Next(tag);

            return new ActionDisposable(() =>
            {
                this._currentElement = this._elements.Pop();
            });
        }
        /// <summary>
        /// Reads an unsigned integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public uint ReadUnsignedInteger(string tag)
        {
            return (uint)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads an unsigned short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ushort ReadUnsignedShort(string tag)
        {
            return (ushort)(short)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads an unsigned long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public ulong ReadUnsignedLong(string tag)
        {
            return (ulong)(long)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads an integer value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public int ReadInteger(string tag)
        {
            return (int)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a short value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public short ReadShort(string tag)
        {
            return (short)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a long value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public long ReadLong(string tag)
        {
            return (long)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a float value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public float ReadFloat(string tag)
        {
            return (float)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a double value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public double ReadDouble(string tag)
        {
            return (double)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a decimal value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public decimal ReadDecimal(string tag)
        {
            return (decimal)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public bool ReadBoolean(string tag)
        {
            return (bool)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a byte value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public byte ReadByte(string tag)
        {
            return (byte)(int)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a byte array value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public byte[] ReadBytes(string tag)
        {
            var base64 = (string)_currentElement.Next(tag).Element;
            byte[] data = Convert.FromBase64String(base64);

            return data;
        }
        /// <summary>
        /// Reads a character value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public char ReadCharacter(string tag)
        {
            return ((string)_currentElement.Next(tag).Element)[0];
        }
        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public string ReadString(string tag)
        {
            return (string)_currentElement.Next(tag).Element;
        }
        /// <summary>
        /// Reads a vector2 value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public Vector2 ReadVector2(string tag)
        {
            using (this.Section(tag))
            {
                float x = this.ReadFloat("X");
                float y = this.ReadFloat("Y");

                return new Vector2(x, y);
            }
        }
        /// <summary>
        /// Reads a rectangle value.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public Rectangle ReadRectangle(string tag)
        {
            using (this.Section(tag))
            {
                float x = this.ReadFloat("X");
                float y = this.ReadFloat("Y");
                float width = this.ReadFloat("Width");
                float height = this.ReadFloat("Height");

                return new Rectangle(x, y, width, height);
            }
        }
        #endregion

        #region Classes
        private class XmlReaderElement
        {
            #region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="XmlReaderElement"/> class.
            /// </summary>
            /// <param name="element">The element.</param>
            public XmlReaderElement(XElement element)
            {
                this.Element = element;
                this._childCounterMappings = new Dictionary<string, int>();
            }
            #endregion

            #region Fields
            private readonly Dictionary<string, int> _childCounterMappings; 
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the element.
            /// </summary>
            public XElement Element { get; private set; }
            #endregion

            #region Methods
            /// <summary>
            /// Increments the counter for the specified tag.
            /// </summary>
            /// <param name="tag">The tag.</param>
            private void IncrementResolveCount(string tag)
            {
                if (!this._childCounterMappings.ContainsKey(tag))
                    this._childCounterMappings.Add(tag, 0);

                this._childCounterMappings[tag]++;
            }
            /// <summary>
            /// Gets the resolve count.
            /// </summary>
            /// <param name="tag">The tag.</param>
            private int GetResolveCount(string tag)
            {
                if (!this._childCounterMappings.ContainsKey(tag))
                    return 0;

                return this._childCounterMappings[tag];
            }
            /// <summary>
            /// Returns the next xml element with the specified name.
            /// </summary>
            /// <param name="tag">The tag.</param>
            public XmlReaderElement Next(string tag)
            {
                int elementIndex = this.GetResolveCount(tag);
                var element = this.Element.Elements(tag).Skip(elementIndex).FirstOrDefault();

                this.IncrementResolveCount(tag);

                return new XmlReaderElement(element);
            }
            #endregion
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}
