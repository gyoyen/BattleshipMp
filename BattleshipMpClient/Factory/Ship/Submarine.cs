using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Ship
{
    public class Submarine : IShip
    {
        public string shipName => "Submarine";
        public int remShips { get; set; } = 3;
        public List<ShipButtons> shipPerButton { get; set; } = new List<ShipButtons>();
        public Color color { get; set; }
    }
}
