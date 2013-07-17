using System;
using System.Linq;

namespace Xemio.GameLibrary.Content.FileSystem.Containers
{
    public class ContainerPath
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerPath"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public ContainerPath(string path)
        {
            string[] pathSeparation = path.Split(new[] {"://"}, StringSplitOptions.None);

            if (pathSeparation.Length != 2)
            {
                throw new InvalidOperationException(
                    "Invalid container path: You have to define a file container.");
            }

            this.Container = pathSeparation.FirstOrDefault();
            this.FileName = pathSeparation.LastOrDefault();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the container.
        /// </summary>
        public string Container { get; private set; }
        /// <summary>
        /// Gets the virtual path.
        /// </summary>
        public string FileName { get; private set; }
        #endregion
    }
}
