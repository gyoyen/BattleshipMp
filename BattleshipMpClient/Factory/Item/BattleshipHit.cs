using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Item
{
    public class BattleshipHit : IItem
    {
        public string itemName => "BattleshipHit";
        public int remItems { get; set; } = 1;

        public string Activate()
        {
            string hitStatus = "Enemy battleship is still not hit";
            foreach (var ship in Form2_PreparatoryScreen.specialShipList)
            {
                if (ship.shipName == "Battleship")
                {
                    Console.WriteLine("bs item");
                    Console.WriteLine(ship.remShields);
                    Console.WriteLine(ship.shipPerButton.Count);
                    if (ship.shipPerButton[0].buttonNames.Count < 4)
                    {
                        hitStatus = "Enemy battleship is hit";
                    }
                }
            }

            return $"{hitStatus}";
        }
    }
}
