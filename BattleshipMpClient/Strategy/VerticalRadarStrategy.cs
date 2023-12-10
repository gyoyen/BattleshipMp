using BattleshipMpClient.Constants;
using BattleshipMpClient.Factory.Ship;
using System.Collections.Generic;

namespace BattleshipMpClient.Strategy
{
    public class VerticalRadarStrategy : IRadarStrategy
    {
        public string ScanGrid(string buttonName)
        {
            var cellsToCheck = GetVerticalCells(buttonName);
            bool shipFound = DetectWithRadarIfShipFoundVertically(Form2_PreparatoryScreen.shipList, cellsToCheck)
                             || DetectWithRadarIfShipFoundVertically(Form2_PreparatoryScreen.specialShipList, cellsToCheck);
            char letterPart = buttonName[0];
            int numberPart = int.Parse(buttonName.Substring(1)) + 1;
            string updatedButtonName = $"{letterPart}{numberPart}";

            string message = shipFound
                ? $"Vertically from position '{updatedButtonName}' radar found ship"
                : $"Vertically from position '{updatedButtonName}' radar did not find ship";

            return message;
        }

        private List<string> GetVerticalCells(string selectedCell)
        {
            List<string> verticalCells = new List<string>();
            char column = selectedCell[0];

            for (int row = 0; row < BoardSize.HEIGHT; row++)
            {
                string cell = $"{column}{row}";
                verticalCells.Add(cell);
            }

            return verticalCells;
        }

        private bool DetectWithRadarIfShipFoundVertically(List<IShip> ships, List<string> cellsToCheck)
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

        private bool DetectWithRadarIfShipFoundVertically(List<ISpecialShip> ships, List<string> cellsToCheck)
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
