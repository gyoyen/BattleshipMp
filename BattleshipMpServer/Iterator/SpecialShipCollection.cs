using BattleshipMpServer.Factory.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpServer.Iterator
{
    public class SpecialShipCollection : ISpecialShipAggregate
    {
        private List<ISpecialShip> _specialShips;

        public SpecialShipCollection(List<ISpecialShip> specialShips)
        {
            _specialShips = specialShips;
        }

        public ISpecialShipIterator CreateIterator()
        {
            return new SpecialShipIterator(_specialShips);
        }
    }
}
