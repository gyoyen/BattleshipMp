using System.Collections.Generic;
using System.Drawing;

namespace BattleshipMpClient.Factory.Ship
{
    public class SpecialDestroyer : ISpecialShip
    {
        public string shipName => "SpecialDestroyer";
        public int remShips { get; set; } = 1;
        public List<ShipButtons> shipPerButton { get; set; } = new List<ShipButtons>();
        public Color color { get; set; }
        public int remShields { get; set; } = 2;
    }
}