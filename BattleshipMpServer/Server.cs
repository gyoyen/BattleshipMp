using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMp
{
    public class Server
    {
        //  Create socket and listen for incoming requests.
        //  The "ip" parameter is not used.

        // thread-safety
        private static readonly object padlock = new object();

        private static Server _instance;

        private TcpListener listener;
        private TcpClient client;

        private Server() { }

        // for accessing the instance of the server
        public static Server GetInstance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Server();
                    }
                    return _instance;
                }
            }
        }

        public TcpClient Client => client;

        public void ServerStart(string ip, string port)
        {
            try
            {
                //listener = new TcpListener(IPAddress.Parse(ip), int.Parse(port));
                listener = new TcpListener(IPAddress.Any, int.Parse(port));
                listener.Start();
                //client = listener.AcceptTcpClient();
                listener.BeginAcceptTcpClient(AcceptClientCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AcceptClientCallback(IAsyncResult asyncResult)
        {
            client = listener.EndAcceptTcpClient(asyncResult);
        }

        public bool IsClientConnected
        {
            get { return client != null && client.Connected; }
        }

        public bool IsListenerActive
        {
            get { return listener != null; }
        }

        public void CloseAndDispose()
        {
            client?.Close();
            client?.Dispose();
            client = null;

            listener?.Stop();
            listener = null;
        }

    }
}
