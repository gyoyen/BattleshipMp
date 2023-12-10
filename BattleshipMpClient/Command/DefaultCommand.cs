using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient.Command
{
    internal class DefaultCommand : ICommand
    {
        public void Execute()
        {
            // Do nothing
        }

        public void Undo(Button button)
        {
            // Do nothing
        }
    }
}
