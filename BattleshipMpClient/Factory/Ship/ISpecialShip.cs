using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Ship
{
    public interface ISpecialShip
    {
        string shipName { get; }
        int remShips { get; set; }
        List<ShipButtons> shipPerButton { get; set; }
        Color color { get; set; }
        int remShields { get; set; }
    }
}
