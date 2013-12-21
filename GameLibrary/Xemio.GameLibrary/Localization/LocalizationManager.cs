using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.GameLibrary.Localization
{
    [Require(typeof(EventManager))]
    [Require(typeof(IFileSystem))]
    [Require(typeof(SerializationManager))]

    public class LocalizationManager : IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationManager"/> class.
        /// </summary>
        public LocalizationManager()
        {
            this.Languages = new List<Language>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current language.
        /// </summary>
        public Language CurrentLanguage { get; private set; }
        /// <summary>
        /// Gets the languages.
        /// </summary>
        public List<Language> Languages { get; private set; }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            this.LoadLanguagesFrom("/Languages/");
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Changes the current language.
        /// </summary>
        /// <param name="language">The language.</param>
        public void ChangeLanguage(string language)
        {
            Language currentLanguage = this.Languages.FirstOrDefault(f => f.CultureName == language);
            if (currentLanguage != null)
            {
                this.CurrentLanguage = currentLanguage;
            }
            else
            {
                this.LogLanguageNotFound(language);   
            }
        }
        /// <summary>
        /// Gets the localized string.
        /// </summary>
        /// <param name="id">The id.</param>
        public string GetLocalizedString(string id)
        {
            LanguageValue value = this.CurrentLanguage.Values.FirstOrDefault(f => f.Id == id);
            if (value == null)
                return id;
            
            return value.Localized;
        }
        /// <summary>
        /// Adds the directory to the root directories and loads all languages inside it.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void LoadLanguagesFrom(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
                return;

            this.LoadLocalizations(directory);
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Loads the localizations in the given directory.
        /// </summary>
        /// <param name="localizationDirectory">The localization directory.</param>
        private void LoadLocalizations(string localizationDirectory)
        {
            var serializer = XGL.Components.Get<SerializationManager>();
            var fileSystem = XGL.Components.Get<IFileSystem>();

            if (!fileSystem.DirectoryExists(localizationDirectory))
                return;

            string[] localizationFiles = fileSystem.GetFiles(localizationDirectory);
            foreach (string file in localizationFiles)
            {
                var language = serializer.Load<Language>(file, Format.Binary);

                if (this.Languages.Contains(language))
                {
                    this.MergeLanguage(language);
                    continue;
                }

                this.Languages.Add(language);
            }
        }
        /// <summary>
        /// Merges the given language with the already loaded.
        /// </summary>
        /// <param name="language">The language.</param>
        private void MergeLanguage(Language language)
        {
            Language existentLanguage = this.Languages.First(f => f.Equals(language));
            foreach (LanguageValue value in language.Values)
            {
                LanguageValue existingValue = existentLanguage.Values.FirstOrDefault(f => f.Id == value.Id);
                if (existingValue != null)
                {
                    existingValue.Localized = value.Localized;
                }
                else
                {
                    existentLanguage.Values.Add(value);
                }
            }
        }
        /// <summary>
        /// Logs that the given language was not found.
        /// </summary>
        /// <param name="language">The language.</param>
        private void LogLanguageNotFound(string language)
        {
            var eventManager = XGL.Components.Get<EventManager>();
            eventManager.Publish(new LanguageNotFoundLoggingEvent(language));
        }
        #endregion
    }
}
