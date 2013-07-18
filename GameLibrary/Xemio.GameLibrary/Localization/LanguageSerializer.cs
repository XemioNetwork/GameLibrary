using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Localization
{
    public class LanguageSerializer : ContentSerializer<Language>
    {
        public override Language Read(BinaryReader reader)
        {
            XDocument document = XDocument.Load(reader.BaseStream);

            Language language = new Language();

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

        public override void Write(BinaryWriter writer, Language value)
        {
            var rootElement = new XElement("language", new XAttribute("name", value.CultureName));

            foreach (LanguageValue languageValue in value.Values)
            {
                XElement element = new XElement("value", new XAttribute("id", languageValue.Id));
                element.Add(languageValue.Localized);

                rootElement.Add(element);
            }

            var document = new XDocument(rootElement);
            document.Save(writer.BaseStream);
        }
    }
}
