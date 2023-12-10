using System;
using System.IO;
using System.Windows.Forms;
using SharedFiles.Facade.FacadeClasses;

namespace SharedFiles.Facade
{
    public class GameFacade
    {
        private StreamReader STR;
        private StreamWriter STW;
        private AttackSender attackSender;
        private AttackReceiver attackReceiver;
        private GameCommunication gameCommunication;

        public GameFacade(ITcpStreamProvider tcpStreamProvider)
        {
            try
            {
                Stream tcpStream = tcpStreamProvider.GetTcpStream();
                STR = new StreamReader(tcpStream);
                STW = new StreamWriter(tcpStream);
                STW.AutoFlush = true;

                attackSender = new AttackSender(STW);
                attackReceiver = new AttackReceiver(STR);
                gameCommunication = new GameCommunication(STR, STW);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public AttackSender GetAttackSender()
        {
            return attackSender;
        }

        public AttackReceiver GetAttackReceiver()
        {
            return attackReceiver;
        }

        public GameCommunication GetGameCommunication()
        {
            return gameCommunication;
        }
    }
}
