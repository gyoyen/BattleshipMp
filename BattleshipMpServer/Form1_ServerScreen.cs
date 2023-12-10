using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using BattleshipMpServer.Factory.Ship;
using BattleshipMp.State;

namespace BattleshipMp
{
    public partial class Form1_ServerScreen : Form
    {

        public Form1_ServerScreen()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            textBoxIpAddress.Text = FillIpTextBox();
        }


        //  Fill in the "ip" field with the computer's ipv4 address.
        string FillIpTextBox()
        {
            IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());

            string ipadr = "";

            foreach (var address in localIp)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipadr = address.ToString();
                }
            }
            return ipadr;
        }

        //  Not active.
        private void textBoxIpAddress_Leave(object sender, EventArgs e)
        {
            //if (textBoxIpAddress.Text != "127.0.0.1" && textBoxIpAddress.Text != FillIpTextBox())
            //{
            //    textBoxIpAddress.Text = FillIpTextBox();
            //}
        }

        //  Call the "ServerStart" method in the Server class.
        private void buttonServerStart_Click(object sender, EventArgs e)
        {
            Server.GetInstance.ServerStart(textBoxIpAddress.Text, textBoxPort.Text);
        }

        //  Check if the client connects every 1 second. Activate the "Continue" button according to the result.
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Server.GetInstance.IsClientConnected)
            {
                labelServerState.Text = "Player successfully connected.";
                buttonGoToBoard.Enabled = true;
            }
            else if (Server.GetInstance.IsListenerActive)
            {
                labelServerState.Text = "The server is started. The player is awaited..";
            }
            else
            {
                labelServerState.Text = "Waiting for the server to start...";
            }
        }

        //  Go to ship theme selection form, Form12
        private void buttonGoToBoard_Click(object sender, EventArgs e)
        {

            timer1.Stop();

            GameContext gameContext = GameContext.Instance;
            gameContext.TransitionTo(new SetupState());

            this.Visible = false;
        }



        private void Form1_ServerScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_ServerScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Server.GetInstance.CloseAndDispose();

            Environment.Exit(1);
        }
    }
}
