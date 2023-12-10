using System.Collections.Generic;
using System.Drawing;

namespace BattleshipMpServer.Factory.Ship
{
    public class SpecialSubmarine : ISpecialShip
    {
        public string shipName => "SpecialSubmarine";
        public int remShips { get; set; } = 1;
        public List<ShipButtons> shipPerButton { get; set; } = new List<ShipButtons>();
        public Color color { get; set; }
        public int remShields { get; set; } = 1;
    }
}
