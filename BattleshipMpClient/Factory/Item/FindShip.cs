using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Item
{
    public class FindShip : IItem
    {
        public string itemName => "FindShip";
        public int remItems { get; set; } = 1;

        public string Activate()
        {
            List<string> allButtons = new List<string>();

            foreach (var ship in Form2_PreparatoryScreen.shipList)
            {
                foreach (var certainTypeShip in ship.shipPerButton)
                {
                    allButtons.AddRange(certainTypeShip.buttonNames);
                }
            }

            foreach (var ship in Form2_PreparatoryScreen.specialShipList)
            {
                foreach (var certainTypeShip in ship.shipPerButton)
                {
                    allButtons.AddRange(certainTypeShip.buttonNames);
                }
            }

            Random random = new Random();
            int randomIndex = random.Next(allButtons.Count);
            string foundShip = allButtons[randomIndex];

            char letterPart = foundShip[0];
            int numberPart = int.Parse(foundShip.Substring(1)) + 1;
            string updatedFoundShip = $"{letterPart}{numberPart}";

            return $"Found a ship at {updatedFoundShip}";
        }
    }
}
