using BattleshipMpClient.Factory.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Iterator
{
    public class ShipIterator : IShipIterator
    {
        private readonly List<IShip> _ships;
        private int _currentIndex = 0;

        public ShipIterator(List<IShip> ships)
        {
            _ships = ships;
        }

        public bool HasNext()
        {
            return _currentIndex < _ships.Count;
        }

        IShip IShipIterator.Next()
        {
            return HasNext() ? _ships[_currentIndex++] : null;
        }
    }
}
