using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpServer.Factory.Ship
{
    public class Battleship : ISpecialShip
    {
        public string shipName => "Battleship";
        public int remShips { get; set; } = 1;
        public List<ShipButtons> shipPerButton { get; set; } = new List<ShipButtons>();
        public Color color { get; set; }
        public int remShields { get; set; } = 3;
    }
}
