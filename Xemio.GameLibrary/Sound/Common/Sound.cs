using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;

namespace Xemio.GameLibrary.Sound.Common
{
    public class Sound : ISound
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

            this.MediaPlayer = new MediaPlayer();
            this.MediaPlayer.Open(new Uri(builder.ToString()));

            this.MediaPlayer.MediaEnded += new EventHandler(MediaPlayerMediaEnded);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the media player.
        /// </summary>
        public MediaPlayer MediaPlayer { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is looped.
        /// </summary>
        public bool IsLooped { get; private set; }
        #endregion

        #region ISound Member
        /// <summary>
        /// Plays the sound.
        /// </summary>
        public void Play()
        {
            this.MediaPlayer.Play();
            this.IsLooped = false;
        }
        /// <summary>
        /// Plays the sound looped.
        /// </summary>
        public void PlayLooping()
        {
            this.MediaPlayer.Play();
            this.IsLooped = true;
        }
        /// <summary>
        /// Stops the current playing sound.
        /// </summary>
        public void Stop()
        {
            this.IsLooped = false;
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
        /// Gets the duration.
        /// </summary>
        public float Duration
        {
            get { return (float)this.MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds; }
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

        #region Event Handlers
        /// <summary>
        /// Handles the media ended event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MediaPlayerMediaEnded(object sender, EventArgs e)
        {
            if (this.IsLooped)
            {
                this.PlayLooping();
            }
        }
        #endregion
    }
}
