using BattleshipMp;
using BattleshipMpServer.Constants;
using BattleshipMpServer.Factory.Ship;
using System.Collections.Generic;

namespace BattleshipMpServer.Strategy
{
    public class VerticalHorizontalRadarStrategy : IRadarStrategy
    {
        public string ScanGrid(string buttonName)
        {
            var verticalCells = GetVerticalCells(buttonName);
            var horizontalCells = GetHorizontalCells(buttonName);

            var cellsToCheck = new List<string>(verticalCells);
            cellsToCheck.AddRange(horizontalCells);

            bool shipFound = DetectWithRadarIfShipFound(Form2_PreparatoryScreen.shipList, cellsToCheck)
                             || DetectWithRadarIfShipFound(Form2_PreparatoryScreen.specialShipList, cellsToCheck);

            char letterPart = buttonName[0];
            int numberPart = int.Parse(buttonName.Substring(1)) + 1;
            string updatedButtonName = $"{letterPart}{numberPart}";

            string message = shipFound
                ? $"Vertically and horizontally from position '{updatedButtonName}' radar found ship"
                : $"Vertically and horizontally from position '{updatedButtonName}' radar did not find ship";

            return message;
        }

        private List<string> GetVerticalCells(string selectedCell)
        {
            List<string> verticalCells = new List<string>();
            char column = selectedCell[0];

            for (int row = 0; row < BoardSize.HEIGHT; row++)
            {
                verticalCells.Add($"{column}{row}");
            }

            return verticalCells;
        }

        private List<string> GetHorizontalCells(string selectedCell)
        {
            List<string> horizontalCells = new List<string>();
            int row = int.Parse(selectedCell.Substring(1));

            for (char column = 'A'; column <= BoardSize.WIDTH_LETTER; column++)
            {
                horizontalCells.Add($"{column}{row}");
            }

            return horizontalCells;
        }

        private bool DetectWithRadarIfShipFound(List<IShip> ships, List<string> cellsToCheck)
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

        private bool DetectWithRadarIfShipFound(List<ISpecialShip> ships, List<string> cellsToCheck)
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