using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Sound
{
    public class SoundManager : IComponent, ISoundFactory
    {
        #region Fields
        private ISoundProvider _provider;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public ISoundProvider Provider
        {
            get
            {
                if (this._provider == null)
                {
                    throw new InvalidOperationException("No sound provider defined.");
                }

                return this._provider;
            }
            set { this._provider = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public ISound CreateSound(string fileName)
        {
            return this.Provider.Factory.CreateSound(fileName);
        }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public IDisposable Play(ISound sound)
        {
            return this.Play(sound, PlayMode.ResetLocation);
        }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        /// <param name="mode">The mode.</param>
        public IDisposable Play(ISound sound, PlayMode mode)
        {
            switch (mode)
            {
                case PlayMode.ResetLocation: return this.Play(sound, Vector2.Zero);
                case PlayMode.KeepLocation: return this.Provider.Play(sound);
            }

            return null;
        }
        /// <summary>
        /// Plays the specified sound according to the specified position in 2d space.
        /// </summary>
        /// <param name="sound">The sound.</param>
        /// <param name="distance">The distance.</param>
        public IDisposable Play(ISound sound, Vector2 distance)
        {
            this.Locate(sound, distance);
            return this.Provider.Play(sound);
        }
        /// <summary>
        /// Locates the specified sound.
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
                float dot = Vector2.Dot(distance, new Vector2(0, -1));

                balance = distance.X / length;
                volume = 1.0f - length / sound.Radius;

                if (dot < 0)
                {
                    float angle = MathHelper.ToAngle(distance) - MathHelper.PiOver2;
                    float degrees = MathHelper.ToDegrees(angle);

                    if (degrees >= 180)
                    {
                        degrees -= (degrees - 180);
                    }

                    volume *= 1.0f - (degrees - 90) / 360.0f;
                }
            }

            sound.Balance = balance;
            sound.Volume = volume;
        }
        #endregion
    }
}
