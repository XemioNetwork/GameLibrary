namespace Xemio.GameLibrary.Game.Scenes
{
    public interface ISceneProvider
    {
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        void Add(Scene scene);
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        void Remove(Scene scene);
    }
}
