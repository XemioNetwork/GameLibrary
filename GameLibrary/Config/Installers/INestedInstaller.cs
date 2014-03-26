using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Config.Installers
{
    public interface INestedInstaller : IInstaller
    {
        /// <summary>
        /// Gets the child.
        /// </summary>
        IInstaller Child { get; }
    }

    public static class NestedInstallerExtensions
    {
        public static IInstaller Unwrap(this IInstaller installer)
        {
            IInstaller current = installer;
            while (current is INestedInstaller)
            {
                var currentNested = (INestedInstaller)current;
                current = currentNested.Child;
            }

            return current;
        }
    }
}
