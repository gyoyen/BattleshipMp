using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Ship
{
    public interface IShipFactory
    {
        IShip CreateSubmarine();
        ISpecialShip CreateSpecialSubmarine();
        IShip CreateDestroyer();
        IShip CreateCruiser();
        ISpecialShip CreateBattleship();

        ISpecialShip CreateSpecialDestroyer();

        ISpecialShip CreateSpecialCruiser();
    }
}
