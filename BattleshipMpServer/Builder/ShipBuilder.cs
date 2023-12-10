using BattleshipMpServer.Factory.Ship;
using System.Drawing;

namespace BattleshipMp.Builder
{
    public class ShipBuilder : IShipBuilder
    {
        private readonly IShipFactory _factory;

        public ShipBuilder(IShipFactory factory)
        {
            _factory = factory;
        }

        public ISpecialShip CreateBattleship()
        {
            return _factory.CreateBattleship();
        }

        public IShip CreateCruiser()
        {
            return _factory.CreateCruiser();
        }

        public IShip CreateDestroyer()
        {
            return _factory.CreateDestroyer();
        }

        public ISpecialShip CreateSpecialSubmarine()
        {
            return _factory.CreateSpecialSubmarine();
        }

        public IShip CreateSubmarine()
        {
            return _factory.CreateSubmarine();
        }

        public ISpecialShip CreateSpecialDestroyer()
        {
            return _factory.CreateSpecialDestroyer();
        }

        public ISpecialShip CreateSpecialCruiser()
        {
            return _factory.CreateSpecialCruiser();
        }
    }
}