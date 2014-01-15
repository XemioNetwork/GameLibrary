using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Localization
{
    [DebuggerDisplay("{Id}={Localized}")]
    public class LanguageValue
    {
        public string Id { get; set; }
        public string Localized { get; set; }
    }
}
