using System;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace BattleshipMpClient.Bridge.Concrete {
    public class HitSound : ISoundImplementation
    {
        SoundPlayer simpleSound = new SoundPlayer(Application.StartupPath + @"\Media\hit.wav");
        public void PlaySound()
        {
            Console.WriteLine("Playing Hit Sound");
            simpleSound.Play();
        }

        public void StopSound()
        {
            Console.WriteLine("Stopping Hit Sound");
            simpleSound.Stop();
        }
    }
}