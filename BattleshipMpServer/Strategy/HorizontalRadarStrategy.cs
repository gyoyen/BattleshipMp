using BattleshipMp;
using BattleshipMpServer.Constants;
using BattleshipMpServer.Factory.Ship;
using System.Collections.Generic;

namespace BattleshipMpServer.Strategy
{
    public class HorizontalRadarStrategy : IRadarStrategy
    {
        public string ScanGrid(string buttonName)
        {
            var cellsToCheck = GetHorizontalCells(buttonName);
            bool shipFound = DetectWithRadarIfShipFoundHorizontally(Form2_PreparatoryScreen.shipList, cellsToCheck)
                             || DetectWithRadarIfShipFoundHorizontally(Form2_PreparatoryScreen.specialShipList, cellsToCheck);

            char letterPart = buttonName[0];
            int numberPart = int.Parse(buttonName.Substring(1)) + 1;
            string updatedButtonName = $"{letterPart}{numberPart}";

            string message = shipFound
                ? $"Horizontally from position '{updatedButtonName}' radar found ship"
                : $"Horizontally from position '{updatedButtonName}' radar did not find ship";

            return message;
        }

        private List<string> GetHorizontalCells(string selectedCell)
        {
            List<string> horizontalCells = new List<string>();
            int row = int.Parse(selectedCell.Substring(1));

            for (char column = 'A'; column <= BoardSize.WIDTH_LETTER; column++)
            {
                string cell = $"{column}{row}";
                horizontalCells.Add(cell);
            }

            return horizontalCells;
        }

        private bool DetectWithRadarIfShipFoundHorizontally(List<IShip> ships, List<string> cellsToCheck)
        {
            foreach (var ship in ships)
            {
                foreach (var certainTypeShip in ship.shipPerButton)
                {
                    if (certainTypeShip.buttonNames.Exists(cellsToCheck.Contains))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DetectWithRadarIfShipFoundHorizontally(List<ISpecialShip> ships, List<string> cellsToCheck)
        {
            foreach (var ship in ships)
            {
                foreach (var certainTypeShip in ship.shipPerButton)
                {
                    if (certainTypeShip.buttonNames.Exists(cellsToCheck.Contains))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}