namespace Xemio.GameLibrary.Network
{
    public interface INestedServerConnection : IServerConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        IServerConnection Connection { get; }
    }
}
