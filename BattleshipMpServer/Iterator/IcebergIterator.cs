using BattleshipMpServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpServer.Iterator
{
    public class IcebergIterator : IIcebergIterator
    {
        private readonly List<Iceberg> _icebergs;
        private int _currentIndex = 0;

        public IcebergIterator(List<Iceberg> icebergs)
        {
            _icebergs = icebergs;
        }

        public bool HasNext()
        {
            return _currentIndex < _icebergs.Count;
        }

        public Iceberg Next()
        {
            return HasNext() ? _icebergs[_currentIndex++] : null;
        }
    }
}
