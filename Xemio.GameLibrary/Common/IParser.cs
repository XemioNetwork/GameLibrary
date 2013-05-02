namespace Xemio.GameLibrary.Common
{
    public interface IParser<in TInput, out TOutput>
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        TOutput Parse(TInput input);
    }
}
