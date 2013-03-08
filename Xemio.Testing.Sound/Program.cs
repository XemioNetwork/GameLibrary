using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary.Math;

namespace Xemio.Testing.Sound
{
    class Program
    {
        static void Main(string[] args)
        {
            SoundManager soundManager = new SoundManager();
            ISound sound = soundManager.CreateSound(@"Resources\bell.wav");

            soundManager.Locate(sound, new Vector2(-10, -10));
            sound.Play();

            Console.ReadLine();
        }
    }
}
