using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.GameLibrary.Localization
{
    internal class LanguageMissingLoggingEvent : LoggingEvent
    {
        public LanguageMissingLoggingEvent(string language) 
            : base(LoggingLevel.Information, string.Format("Tried loading the language '{0}'. Maybe you forgot to load the directory containing it.", language))
        {
        }
    }
}
