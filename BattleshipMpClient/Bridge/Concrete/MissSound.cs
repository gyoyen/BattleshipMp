using System;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace BattleshipMpClient.Bridge.Concrete {
    public class MissSound : ISoundImplementation
    {
        SoundPlayer simpleSound = new SoundPlayer(Application.StartupPath + @"\Media\miss.wav");
        //Application.StartupPath + @"\Images\shield.png"
        public void PlaySound()
        {
            Console.WriteLine("Playing Miss Sound");
            simpleSound.Play();
        }

        public void StopSound()
        {
            Console.WriteLine("Stopping Miss Sound");
            simpleSound.Stop();
        }
    }
}