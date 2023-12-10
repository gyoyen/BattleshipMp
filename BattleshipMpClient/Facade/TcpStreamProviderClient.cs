using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedFile;
using SharedFile.Facade;

namespace BattleshipMpClient.Facade
{
    public class TcpStreamProviderClient : ITcpStreamProvider
    {
        public Stream GetTcpStream()
        {
            return Client.GetInstance.TcpClient.GetStream();
        }

        public bool GetConnection()
        {
            return Client.GetInstance.IsConnected;
        }
    }
}
