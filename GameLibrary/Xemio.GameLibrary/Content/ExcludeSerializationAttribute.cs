﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcludeSerializationAttribute : Attribute
    {
    }
}
