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
            this.Factory = new Common.SoundFactory();
        }
        #endregion

        #region Properties
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
            float length = distance.Length;
            float balance = distance.X / length;
            float volume = 1.0f - length / this.Radius;

            if (distance.LengthSquared == 0)
            {
                balance = 0.0f;
                volume = 1.0f;
            }

            sound.Balance = balance;
            sound.Volume = volume;
        }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public void Play(ISound sound)
        {
            this.Play(sound, Vector2.Zero);
        }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        /// <param name="distance">The distance.</param>
        public void Play(ISound sound, Vector2 distance)
        {
            this.Locate(sound, distance);
            sound.Play();
        }
        /// <summary>
        /// Plays the sound looping.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public void PlayLooping(ISound sound)
        {
            this.PlayLooping(sound, Vector2.Zero);
        }
        /// <summary>
        /// Plays the sound looping.
        /// </summary>
        /// <param name="sound">The sound.</param>
        /// <param name="distance">The distance.</param>
        public void PlayLooping(ISound sound, Vector2 distance)
        {
            this.Locate(sound, distance);
            sound.PlayLooping();
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
