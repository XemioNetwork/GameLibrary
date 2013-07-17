using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Script
{
    public interface ICommand : ILinkable<string>
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ICommand"/> is active.
        /// </summary>
        bool Active { get; }
        /// <summary>
        /// Executes this command.
        /// </summary>
        void Execute();
    }
}
