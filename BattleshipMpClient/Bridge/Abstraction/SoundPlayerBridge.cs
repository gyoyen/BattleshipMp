using System;
using System.IO;
using System.Windows.Forms;
using BattleshipMpClient.Bridge.Concrete;

namespace BattleshipMpClient.Bridge.Abstraction {
  public abstract class SoundPlayerBridge {

    protected ISoundImplementation soundImplementation;

    public SoundPlayerBridge(ISoundImplementation soundImplementation) 
    {
      this.soundImplementation = soundImplementation;
    }

    public abstract void Play();

    public abstract void Stop();
  }
}