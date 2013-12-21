namespace Xemio.GameLibrary.Common
{
    public interface IConverter<in TInput, out TOutput>
    {
        /// <summary>
        /// Converts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        TOutput Convert(TInput input);
    }
}
