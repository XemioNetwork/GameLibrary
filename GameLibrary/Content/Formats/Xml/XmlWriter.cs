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
    public class XmlWriter : IFormatWriter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public XmlWriter(Stream stream)
        {
            this.Stream = stream;

            this._currentElement = new XElement("xml");
            this._elements = new Stack<XElement>();
        }
        #endregion

        #region Fields
        private XElement _currentElement;
        private readonly Stack<XElement> _elements; 
        #endregion

        #region Implementation of IFormatWriter
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
            this._currentElement = new XElement(tag);

            return new ActionDisposable(() =>
            {
                var element = this._currentElement;

                this._currentElement = this._elements.Pop();
                this._currentElement.Add(element);
            });
        }
        /// <summary>
        /// Writes the specified unsigned integer value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedInteger(string tag, uint value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified unsigned short value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedShort(string tag, ushort value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified unsigned long value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteUnsignedLong(string tag, ulong value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified integer value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteInteger(string tag, int value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified short value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteShort(string tag, short value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified long value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteLong(string tag, long value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified float value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteFloat(string tag, float value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified double value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteDouble(string tag, double value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified decimal value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteDecimal(string tag, decimal value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified boolean value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteBoolean(string tag, bool value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified byte value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteByte(string tag, byte value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified byte array value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteBytes(string tag, byte[] value)
        {
            this._currentElement.Add(new XElement(tag, string.Join(",", value)));
        }
        /// <summary>
        /// Writes the specified character value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteCharacter(string tag, char value)
        {
            this._currentElement.Add(new XElement(tag, value.ToString()));
        }
        /// <summary>
        /// Writes the specified string value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteString(string tag, string value)
        {
            this._currentElement.Add(new XElement(tag, value));
        }
        /// <summary>
        /// Writes the specified vector value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteVector2(string tag, Vector2 value)
        {
            using (this.Section(tag))
            {
                this.WriteFloat("X", value.X);
                this.WriteFloat("Y", value.Y);
            }
        }
        /// <summary>
        /// Writes the specified rectangle value.
        /// </summary>
        /// <param name="tag">The tag that is used for better readability in plain text formats.</param>
        /// <param name="value">The value.</param>
        public void WriteRectangle(string tag, Rectangle value)
        {
            using (this.Section(tag))
            {
                this.WriteFloat("X", value.X);
                this.WriteFloat("Y", value.Y);
                this.WriteFloat("Width", value.Width);
                this.WriteFloat("Height", value.Height);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks and throws an invalid operation exception.
        /// </summary>
        private void CheckAndThrowDisposeException()
        {
            if (this._elements.Count > 0)
            {
                var sectionBuilder = new StringBuilder();
                sectionBuilder.Append("[");

                while (this._elements.Count > 0)
                {
                    sectionBuilder.Append(this._elements.Pop().Name.LocalName);
                    if (this._elements.Count > 1)
                        sectionBuilder.Append(", ");
                }

                sectionBuilder.Append("]");

                throw new InvalidOperationException(
                    "There are unclosed sections, make sure you dispose all calls to writer.Section(tag). " +
                    "Remaining sections: " + sectionBuilder);
            }
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.CheckAndThrowDisposeException();
            this._currentElement.Save(this.Stream);
        }
        #endregion
    }
}
