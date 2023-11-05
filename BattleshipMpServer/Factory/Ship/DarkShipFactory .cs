﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpServer.Factory.Ship
{
    public class DarkShipFactory : IShipFactory
    {
        public IShip CreateSubmarine()
        {
            return new Submarine() { color = Color.DarkSlateBlue };
        }

        public ISpecialShip CreateSpecialSubmarine()
        {
            return new SpecialSubmarine() { color = Color.FromArgb(82, 61, 129) };
        }

        public IShip CreateDestroyer()
        {
            return new Destroyer() { color = Color.DarkOliveGreen };
        }

        public IShip CreateCruiser()
        {
            return new Cruiser() { color = Color.DarkCyan };
        }

        public IShip CreateBattleship()
        {
            return new Battleship() { color = Color.DarkMagenta };
        }
    }
}