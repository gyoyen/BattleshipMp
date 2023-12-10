﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpServer.Command
{
    internal interface ICommand
    {
        void Execute();
        void Undo(Button button);
    }
}
