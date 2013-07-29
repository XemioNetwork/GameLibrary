using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.GameLibrary.Localization
{
    internal class LanguageNotFoundLoggingEvent : LoggingEvent
    {
        public LanguageNotFoundLoggingEvent(string language) 
            : base(LoggingLevel.Information, string.Format("Tried loading the language '{0}' but it was not found. Maybe you forgot to load the directory containing it.", language))
        {
        }
    }
}
