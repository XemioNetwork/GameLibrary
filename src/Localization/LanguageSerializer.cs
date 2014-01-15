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
        /// <returns></returns>
        public override Language Read(IFormatReader reader)
        {
            var document = XDocument.Load(reader.Stream);
            var language = new Language();

            language.CultureName = document.Root.Attribute("name").Value;
            IEnumerable<LanguageValue> values = from element in document.Root.Descendants()
                                                select new LanguageValue
                                                           {
                                                               Id = element.Attribute("id").Value,
                                                               Localized = element.Value
                                                           };
            language.Values.AddRange(values);

            return language;
        }
        /// <summary>
        /// Writes the specified language as XML to the writers stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, Language value)
        {
            var rootElement = new XElement("language", new XAttribute("name", value.CultureName));

            foreach (LanguageValue languageValue in value.Values)
            {
                var element = new XElement("value", new XAttribute("id", languageValue.Id));
                element.Add(languageValue.Localized);

                rootElement.Add(element);
            }

            var document = new XDocument(rootElement);
            document.Save(writer.Stream);
        }
        #endregion
    }
}
