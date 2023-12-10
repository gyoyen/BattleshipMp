using BattleshipMpServer.Factory.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpServer.Iterator
{
    public interface ISpecialShipIterator
    {
        bool HasNext();
        ISpecialShip Next();
    }
}
