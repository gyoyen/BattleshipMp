﻿using System;
using System.IO;
using System.Windows.Forms;

namespace SharedFile.Facade.FacadeClasses
{
    public class AttackSender
    {
        private StreamWriter STW;

        public AttackSender(StreamWriter stw)
        {
            STW = stw;
        }

        public void SendAttack(string buttonName, ITcpStreamProvider tcpStreamProvider)
        {
            if (tcpStreamProvider.GetConnection())
            {
                STW.WriteLine(buttonName);
            }
            else
            {
                MessageBox.Show("Message could not be sent!!");
            }
        }
    }
}