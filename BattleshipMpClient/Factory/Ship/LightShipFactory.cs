using System.Drawing;

namespace BattleshipMpClient.Factory.Ship
{
    public class LightShipFactory : IShipFactory
    {
        public IShip CreateSubmarine()
        {
            return new Submarine() { color = Color.LightCoral };
        }

        public ISpecialShip CreateSpecialSubmarine()
        {
            return new SpecialSubmarine() { color = Color.FromArgb(220, 128, 138) };
        }
        public IShip CreateDestroyer()
        {
            return new Destroyer() { color = Color.LightSkyBlue };
        }

        public IShip CreateCruiser()
        {
            return new Cruiser() { color = Color.LightGreen };
        }

        public ISpecialShip CreateBattleship()
        {
            return new Battleship() { color = Color.LightGoldenrodYellow };
        }

        public ISpecialShip CreateSpecialCruiser()
        {
            return new SpecialCruiser() { color = Color.Green };
        }

        public ISpecialShip CreateSpecialDestroyer()
        {
            return new SpecialDestroyer() { color = Color.SkyBlue };
        }
    }
}
