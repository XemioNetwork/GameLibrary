namespace Xemio.GameLibrary.Network.Nested
{
    public interface INestedServerConnection : IServerConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        IServerConnection Connection { get; }
    }
}
