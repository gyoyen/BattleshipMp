using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Ship
{
    public class DarkShipFactory : IShipFactory
    {
        public IShip CreateSubmarine()
        {
            return new Submarine() { color = Color.DarkSlateBlue };
        }

        public ISpecialShip CreateSpecialSubmarine()
        {
            return new SpecialSubmarine() { color = Color.FromArgb(82, 61, 129) };
        }
        public IShip CreateDestroyer()
        {
            return new Destroyer() { color = Color.DarkOliveGreen };
        }

        public IShip CreateCruiser()
        {
            return new Cruiser() { color = Color.DarkCyan };
        }

        public ISpecialShip CreateBattleship()
        {
            return new Battleship() { color = Color.DarkMagenta };
        }

        public ISpecialShip CreateSpecialDestroyer()
        {
            return new SpecialDestroyer() { color = Color.OliveDrab };
        }

        public ISpecialShip CreateSpecialCruiser()
        {
            return new SpecialCruiser() { color = Color.Cyan };
        }
    }
}
