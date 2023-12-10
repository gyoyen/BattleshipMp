using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpServer.Factory.Ship
{
    public class Destroyer : IShip
    {
        public string shipName => "Destroyer";
        public int remShips { get; set; } = 2;
        public List<ShipButtons> shipPerButton { get; set; } = new List<ShipButtons>();
        public Color color { get; set; }
    }
}
