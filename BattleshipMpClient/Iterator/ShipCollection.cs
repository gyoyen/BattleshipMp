using BattleshipMpClient.Factory.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Iterator
{
    public class ShipCollection : IShipAggregate
    {
        private List<IShip> _ships;

        public ShipCollection(List<IShip> ships)
        {
            _ships = ships;
        }

        public IShipIterator CreateIterator()
        {
            return new ShipIterator(_ships);
        }
    }
}
