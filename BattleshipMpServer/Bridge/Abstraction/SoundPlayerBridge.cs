using System;
using System.IO;
using System.Windows.Forms;
using BattleshipMpServer.Bridge.Concrete;

namespace BattleshipMpServer.Bridge.Abstraction {
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