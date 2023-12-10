using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleshipMpClient.Factory.Ship;
using BattleshipMpClient.State;

namespace BattleshipMpClient
{
    public partial class Form1_ClientScreen : Form
    {
        public Form1_ClientScreen()
        {
            InitializeComponent();
        }

        private void buttonConnectToServer_Click(object sender, EventArgs e)
        {
            Client.GetInstance.ConnectToServer(textBoxIpAddress.Text, textBoxPort.Text);

            if (Client.GetInstance.TcpClient != null)
            {
                buttonGoToBoard.Enabled = true;
                labelServerState.Text = "Connection successful. You can continue.";
            }
            else
                labelServerState.Text = "You must connect to the server.";
        }

        //  Go to ship theme selection form, Form12
        private void buttonGoToBoard_Click(object sender, EventArgs e)
        {
            GameContext gameContext = GameContext.Instance;
            gameContext.TransitionTo(new SetupState());
            this.Visible = false;
        }

        private void Form1_ClientScreen_Load(object sender, EventArgs e)
        {
            labelServerState.Text = "You must connect to the server.";
        }

        private void Form1_ClientScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Client.GetInstance.CloseAndDispose();
            Environment.Exit(1);
        }
    }
}
