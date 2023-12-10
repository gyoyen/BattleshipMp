using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpServer.Factory.Ship
{
    public interface IShip
    {
        string shipName { get; }
        int remShips { get; set; }
        List<ShipButtons> shipPerButton { get; set; }
        Color color { get; set; }
    }
}
