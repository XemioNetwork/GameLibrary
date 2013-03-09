using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Sound.Internal
{
    internal class Sound : ISound
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Sound"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public Sound(string fileName)
        {
            string fullPath = Path.GetFullPath(fileName);
            UriBuilder builder = new UriBuilder(fullPath);

            ThreadInvoker.Invoke(() =>
            {
                //Safely create the media player inside the main thread to prevent
                //invoker exceptions using the play method.

                this.MediaPlayer = new MediaPlayer();
                this.MediaPlayer.Volume = 1.0f;
                this.MediaPlayer.Open(new Uri(builder.ToString()));

                while (this.MediaPlayer.IsBuffering);
            });
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the media player.
        /// </summary>
        public MediaPlayer MediaPlayer { get; private set; }
        #endregion

        #region ISound Member
        /// <summary>
        /// Plays the sound.
        /// </summary>
        public void Play()
        {
            this.MediaPlayer.Stop();
            this.MediaPlayer.Play();
        }
        /// <summary>
        /// Stops the current playing sound.
        /// </summary>
        public void Stop()
        {
            this.MediaPlayer.Stop();
        }
        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public float Volume
        {
            get { return (float)this.MediaPlayer.Volume; }
            set { this.MediaPlayer.Volume = value; }
        }
        /// <summary>
        /// Gets or sets the speed ratio.
        /// </summary>
        public float SpeedRatio
        {
            get { return (float)this.MediaPlayer.SpeedRatio; }
            set { this.MediaPlayer.SpeedRatio = value; }
        }
        /// <summary>
        /// Gets or sets the position in seconds.
        /// </summary>
        public float Position
        {
            get { return (float)this.MediaPlayer.Position.TotalSeconds; }
            set { this.MediaPlayer.Position = TimeSpan.FromSeconds(value); }
        }
        /// <summary>
        /// Gets the duration in seconds.
        /// </summary>
        public float Duration
        {
            get
            {
                if (this.MediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    return (float)this.MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                }

                return 0;
            }
        }
        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        public float Balance
        {
            get { return (float)this.MediaPlayer.Balance; }
            set { this.MediaPlayer.Balance = value; }
        }
        #endregion
    }
}
