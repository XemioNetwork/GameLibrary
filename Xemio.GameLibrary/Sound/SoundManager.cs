using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Sound
{
    public class SoundManager : IComponent, ISoundFactory
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundManager"/> class.
        /// </summary>
        public SoundManager()
        {
            this.Radius = float.MaxValue;
            this.Factory = new Internal.SoundFactory();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the loop manager.
        /// </summary>
        public LoopManager LoopManager
        {
            get { return XGL.GetComponent<LoopManager>(); }
        }
        /// <summary>
        /// Gets or sets the sound factory.
        /// </summary>
        public ISoundFactory Factory { get; set; }
        /// <summary>
        /// Gets or sets the radius for hearable sounds.
        /// </summary>
        public float Radius { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Applies distant calculations to the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        /// <param name="distance">The distance.</param>
        public void Locate(ISound sound, Vector2 distance)
        {
            float balance = 0.0f;
            float volume = 1.0f;

            if (distance.LengthSquared > 0)
            {
                float length = distance.Length;

                balance = distance.X / length;
                volume = 1.0f - length / this.Radius;
            }

            sound.Balance = balance;
            sound.Volume = volume;
        }
        /// <summary>
        /// Enables looped playback for the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public void EnableLooping(ISound sound)
        {
            this.LoopManager.Register(sound);
        }
        /// <summary>
        /// Disables looped playback for the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public void DisableLooping(ISound sound)
        {
            this.LoopManager.Remove(sound);
        }
        #endregion

        #region ISoundFactory Member
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public ISound CreateSound(string fileName)
        {
            return this.Factory.CreateSound(fileName);
        }
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public ISound CreateSound(Stream stream)
        {
            return this.Factory.CreateSound(stream);
        }
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ISound CreateSound(byte[] data)
        {
            return this.Factory.CreateSound(data);
        }
        #endregion
    }
}
