using BattleshipMpServer.Factory.Ship;
using System.Collections.Generic;

namespace BattleshipMp.Builder
{
    public class ShipsCreator
    {
        private readonly IShipBuilder _builder;
        private readonly List<IShip> _normalShips;
        private readonly List<ISpecialShip> _specialShips;

        public ShipsCreator(IShipBuilder builder)
        {
            _builder = builder;
            _normalShips = new List<IShip>();
            _specialShips = new List<ISpecialShip>();
        }

        public List<IShip> BuildNormalShips()
        {
            _normalShips.Add(_builder.CreateCruiser());
            _normalShips.Add(_builder.CreateSubmarine());
            _normalShips.Add(_builder.CreateDestroyer());

            return _normalShips;
        }

        public List<ISpecialShip> BuildSpecialShips()
        {
            _specialShips.Add(_builder.CreateSpecialSubmarine());
            _specialShips.Add(_builder.CreateSpecialCruiser());
            _specialShips.Add(_builder.CreateSpecialDestroyer());
            _specialShips.Add(_builder.CreateBattleship());

            return _specialShips;
        }
    }
}