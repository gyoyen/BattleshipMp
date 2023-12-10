using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace BattleshipMpServer.Bridge.Concrete {
    public class BackgroundMusic : ISoundImplementation
    {
        SoundPlayer simpleSound = new SoundPlayer(Application.StartupPath + @"\Media\background.wav");
        public void PlaySound()
        {
            Console.WriteLine("Playing Background Music");
            simpleSound.PlayLooping();
        }

        public void StopSound()
        {
            Console.WriteLine("Stopping Background Music");
            simpleSound.Stop();
        }
    }
}