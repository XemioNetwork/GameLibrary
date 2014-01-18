using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Localization
{
    public class LanguageSerializer : Serializer<Language>
    {
        #region Overrides of Serializer<Language>
        /// <summary>
        /// Reads a XML formatted langauage from the readers stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Language Read(IFormatReader reader)
        {
            var document = XDocument.Load(reader.Stream);

            var language = new Language();
            language.CultureName = document.Root.Attribute("name").Value;

            foreach (XElement element in document.Root.Descendants())
            {
                language.Values.Add(element.Attribute("id").Value, element.Value);
            }


            return language;
        }
        /// <summary>
        /// Writes the specified language as XML to the writers stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="language">The language.</param>
        public override void Write(IFormatWriter writer, Language language)
        {
            var rootElement = new XElement("language", new XAttribute("name", language.CultureName));

            foreach (KeyValuePair<string, string> pair in language.Values)
            {
                var element = new XElement("value", new XAttribute("id", pair.Key));
                element.Add(pair.Value);

                rootElement.Add(element);
            }

            var document = new XDocument(rootElement);
            document.Save(writer.Stream);
        }
        #endregion
    }
}
