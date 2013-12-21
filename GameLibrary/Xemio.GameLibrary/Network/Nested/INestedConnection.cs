namespace Xemio.GameLibrary.Network.Nested
{
    public interface INestedConnection : IConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        IConnection Connection { get; }
    }
}
