using System;
using System.IO;
using System.Windows.Forms;

namespace SharedFiles.Facade.FacadeClasses
{
    public class GameCommunication
	{
        private StreamReader STR;
        private StreamWriter STW;

        public GameCommunication(StreamReader str, StreamWriter stw)
        {
            STR = str;
            STW = stw;
        }

        public void StartGameCommunication(ITcpStreamProvider tcpStreamProvider)
        {
            try
            {
                Stream tcpStream = tcpStreamProvider.GetTcpStream();
                STR = new StreamReader(tcpStream);
                STW = new StreamWriter(tcpStream);
                STW.AutoFlush = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
