﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Ship
{
    public interface IShipFactory
    {
        IShip CreateSubmarine();
        IShip CreateDestroyer();
        IShip CreateCruiser();
        IShip CreateBattleship();
    }
}
