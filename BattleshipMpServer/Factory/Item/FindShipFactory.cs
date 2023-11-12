﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMp.Factory.Item
{
    public class FindShipFactory : IItemFactory
    {
        public IItem CreateItem()
        {
            return new FindShip();
        }
    }
}