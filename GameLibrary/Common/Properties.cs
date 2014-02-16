using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public class Properties
    {
        #region Fields
        private readonly string[] _lines;

        private readonly Dictionary<int, string> _indexMappings;
        private readonly Dictionary<string, Property> _propertyMappings;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Properties"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public Properties(Stream stream) : this(new StreamReader(stream).ReadToEnd())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Properties"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public Properties(string content)
        {
            this._lines = content.Replace("\r", string.Empty).Split("\n".ToCharArray());

            this._indexMappings = new Dictionary<int, string>();
            this._propertyMappings = new Dictionary<string, Property>();

            for (int i = 0; i < this._lines.Length; i++)
            {
                if (this._lines[i].StartsWith("#")) //Comment
                    continue;

                if (string.IsNullOrWhiteSpace(this._lines[i])) //Released line
                    continue;

                int seperatorIndex = this._lines[i].IndexOf("=", StringComparison.Ordinal);
                if (seperatorIndex > -1)
                {
                    string key = this._lines[i].Substring(0, seperatorIndex);
                    string value = this._lines[i].Substring(++seperatorIndex);

                    this._indexMappings.Add(i, key);
                    this._propertyMappings.Add(key, new Property(key, value, i));
                }
            }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static Properties Load(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var reader = new StreamReader(stream, Encoding.Default);
                return new Properties(reader.ReadToEnd());
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the keys.
        /// </summary>
        public IEnumerable<string> GetKeys()
        {
            return this._propertyMappings.Select(pair => pair.Key);
        }
        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="key">The key.</param>
        public string GetProperty(string key)
        {
            return this.GetProperty(key, null);
        }
        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        public string GetProperty(string key, string defaultValue)
        {
            if (!this._propertyMappings.ContainsKey(key))
                return defaultValue;

            return this._propertyMappings[key].Value;
        }
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetProperty(string key, string value)
        {
            if (!this._propertyMappings.ContainsKey(key))
                this._propertyMappings.Add(key, new Property(key, value, -1));
            else
                this._propertyMappings[key].Value = value;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < this._lines.Length; i++)
            {
                if (this._indexMappings.ContainsKey(i))
                {
                    Property property = this._propertyMappings[this._indexMappings[i]];

                    builder.Append(property.Key)
                        .Append('=')
                        .AppendLine(property.Value);
                }
                else
                {
                    builder.AppendLine(this._lines[i]);
                }
            }

            var query = from pair in this._propertyMappings
                        where pair.Value.LineNumber < 0
                        select pair.Value;

            foreach (Property property in query)
            {
                builder.Append(property.Key)
                    .Append('=')
                    .AppendLine(property.Value);
            }
            
            return builder.ToString();
        }
        #endregion
    }
}
