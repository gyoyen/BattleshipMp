using System.IO;
using SharedFile.Facade;
using BattleshipMp;

namespace BattleshipMpServer.Facade
{
    public class TcpStreamProviderServer : ITcpStreamProvider
    {
        public Stream GetTcpStream()
        {
            return Server.GetInstance.Client.GetStream();
        }

        public bool GetConnection()
        {
            return Server.GetInstance.IsClientConnected;
        }
    }
}
