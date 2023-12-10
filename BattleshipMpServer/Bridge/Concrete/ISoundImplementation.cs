using System;
using System.IO;
using System.Windows.Forms;

namespace BattleshipMpServer.Bridge.Concrete {
    public interface ISoundImplementation
    {
        void PlaySound();
        void StopSound();
    }
}