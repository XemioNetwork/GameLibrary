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
            ISound sound = soundManager.CreateSound(@"Sounds\bell.wav");

            soundManager.Play(sound, new Vector2(-10, -10));
            Console.ReadLine();
        }
    }
}
