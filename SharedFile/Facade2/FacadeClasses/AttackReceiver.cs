using System;
using System.IO;
using System.Windows.Forms;


namespace SharedFiles.Facade.FacadeClasses
{
    public class AttackReceiver
	{
        private StreamReader STR;

        public AttackReceiver(StreamReader str)
        {
            STR = str;
        }

        public string ReceiveAttack()
        {
            return STR.ReadLine();
        }
    }
}
