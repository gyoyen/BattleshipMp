using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Item
{
    public class ItemFactory : IItemFactory
    {
        public IItem CreateFindShipItem()
        {
            return new FindShip();
        }
        public IItem CreateBattleshipHitItem()
        {
            return new BattleshipHit();
        }
        public IItem CreateJamItem()
        {
            return new Jam();
        }
    }
}
