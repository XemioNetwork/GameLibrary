using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary.Math;
using System.Threading;

namespace Xemio.Testing.Sound
{
    class Program
    {
        static void Main(string[] args)
        {
            SoundManager soundManager = new SoundManager();
            ISound sound = soundManager.CreateSound(@"Resources\bell.wav");
            
            soundManager.Play(sound, new Vector2(-10, -10));

            Vector2 distance = new Vector2(-10, -10);
            while (true)
            {
                distance = Vector2.Rotate(distance, MathHelper.ToRadians(1));
                soundManager.Locate(sound, distance);

                Console.Clear();
                Console.WriteLine(MathHelper.ToDegrees(MathHelper.ToAngle(distance)));

                Thread.Sleep(20);
            }
        }
    }
}
