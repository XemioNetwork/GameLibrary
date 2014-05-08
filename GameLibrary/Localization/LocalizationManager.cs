using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Logging;

namespace Xemio.GameLibrary.Localization
{
    [Require(typeof(IEventManager))]
    [Require(typeof(IFileSystem))]
    [Require(typeof(ContentManager))]

    public class LocalizationManager : IConstructable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationManager" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public LocalizationManager(string directory)
        {
            this.Languages = new List<Language>();
            this.Directory = directory;
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
        /// <summary>
        /// Gets the directory.
        /// </summary>
        public string Directory { get; private set; }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            this.LoadLanguages(this.Directory);
            this.ChangeToSystemLanguage();
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Changes the current language to the users language.
        /// </summary>
        public void ChangeToSystemLanguage()
        {
            this.ChangeLanguage(Thread.CurrentThread.CurrentUICulture.Name);
        }
        /// <summary>
        /// Changes the current language.
        /// </summary>
        /// <param name="language">The language.</param>
        public void ChangeLanguage(string language)
        {
            Language currentLanguage = this.Languages.FirstOrDefault(f => f.CultureName == language);
            if (currentLanguage == null)
            {
                logger.Warn("Language {0} does not exist. Could not change language.", language);
                return;
            }

            this.CurrentLanguage = currentLanguage;
        }
        /// <summary>
        /// Gets the localized string for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public string Get(string id)
        {
            if (this.CurrentLanguage == null || !this.CurrentLanguage.Values.ContainsKey(id))
            {
                return id;
            }

            return this.CurrentLanguage.Values[id];
        }
        /// <summary>
        /// Gets the localized format for the specified id and appends format parameters.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="parameters">The parameters.</param>
        public string Get(string id, params object[] parameters)
        {
            return string.Format(this.Get(id), parameters);
        }
        /// <summary>
        /// Adds the directory to the root directories and loads all languages inside it.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void LoadLanguages(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                logger.Warn("Language directory is not defined.");
                return;
            }

            var content = XGL.Components.Get<ContentManager>();
            var fileSystem = XGL.Components.Get<IFileSystem>();

            if (!fileSystem.DirectoryExists(directory))
            {
                logger.Warn("Language directory {0} does not exist.", directory);
                return;
            }

            logger.Info("Loading languages from {0}.", directory);

            string[] localizationFiles = fileSystem.GetFiles(directory);
            foreach (Language language in localizationFiles.Select(content.Query<Language>))
            {
                if (this.Languages.Contains(language))
                {
                    this.MergeLanguage(language);
                }
                else
                {
                    this.Languages.Add(language);
                }
            }
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Merges the given language with the already loaded.
        /// </summary>
        /// <param name="language">The language.</param>
        private void MergeLanguage(Language language)
        {
            Language existentLanguage = this.Languages.First(f => f.Equals(language));
            foreach (KeyValuePair<string, string> pair in language.Values)
            {
                if (existentLanguage.Values.ContainsKey(pair.Key))
                {
                    existentLanguage.Values[pair.Key] = pair.Value;
                }
                else
                {
                    existentLanguage.Values.Add(pair.Key, pair.Value);
                }
            }
        }
        #endregion
    }
}
