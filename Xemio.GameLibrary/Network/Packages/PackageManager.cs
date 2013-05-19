using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Common.Extensions;

namespace Xemio.GameLibrary.Network.Packages
{
    public class PackageManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageManager"/> class.
        /// </summary>
        public PackageManager()
        {
            this.Linker = new GenericLinker<int, Package>();
            this.Linker.CreationType = CreationType.None;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the package linker.
        /// </summary>
        public GenericLinker<int, Package> Linker { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Serializes the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="writer">The writer.</param>
        public void Serialize(Package package, BinaryWriter writer)
        {
            writer.Write(package.Id);
            writer.WriteInstance(package);
        }
        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public Package Deserialize(BinaryReader reader)
        {
            int identifier = reader.ReadInt32();
            Package package = this.Linker.Resolve(identifier);

            return (Package)reader.ReadInstance(package.GetType());
        }
        #endregion
    }
}
