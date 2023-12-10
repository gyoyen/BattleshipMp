using BattleshipMpClient.Factory.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Iterator
{
    public class SpecialShipIterator : ISpecialShipIterator
    {
        private readonly List<ISpecialShip> _specialShips;
        private int _currentIndex = 0;

        public SpecialShipIterator(List<ISpecialShip> specialShips)
        {
            _specialShips = specialShips;
        }

        public bool HasNext()
        {
            return _currentIndex < _specialShips.Count;
        }

        ISpecialShip ISpecialShipIterator.Next()
        {
            return HasNext() ? _specialShips[_currentIndex++] : null;
        }
    }
}
