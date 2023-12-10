using BattleshipMpClient.Factory.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Iterator
{
    public interface IShipIterator
    {
        bool HasNext();
        IShip Next();
    }
}
