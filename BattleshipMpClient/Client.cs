using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient
{
    public class Client
    {
        private static readonly object padlock = new object();
        private static Client _instance;
        private TcpClient client = new TcpClient();

        private Client() { }

        public static Client GetInstance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Client();
                    }
                    return _instance;
                }
            }
        }

        public void ConnectToServer(string ip, string port)
        {
            //client = new TcpClient();
            //IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
            //client.Connect(ipEnd);

            try
            {
                client.BeginConnect(IPAddress.Parse(ip), int.Parse(port), ConnectCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                client.Dispose();
                client = null;
            }
        }

        public void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                client.EndConnect(asyncResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public TcpClient TcpClient => client;

        public void CloseAndDispose()
        {
            if (client != null)
            {
                client.Close();
                client.Dispose();
                client = null;
            }
        }

        public bool IsConnected
        {
            get { return client != null && client.Connected; }
        }
    }
}
