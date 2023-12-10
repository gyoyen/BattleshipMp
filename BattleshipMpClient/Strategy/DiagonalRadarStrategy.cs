using BattleshipMpClient.Constants;
using BattleshipMpClient.Factory.Ship;
using System.Collections.Generic;

namespace BattleshipMpClient.Strategy
{
    public class DiagonalRadarStrategy : IRadarStrategy
    {
        public string ScanGrid(string buttonName)
        {
            var cellsToCheck = GetDiagonalCells(buttonName);
            bool shipFound = DetectWithRadarIfShipFoundDiagonally(Form2_PreparatoryScreen.shipList, cellsToCheck)
                             || DetectWithRadarIfShipFoundDiagonally(Form2_PreparatoryScreen.specialShipList, cellsToCheck);

            char letterPart = buttonName[0];
            int numberPart = int.Parse(buttonName.Substring(1)) + 1;
            string updatedButtonName = $"{letterPart}{numberPart}";

            string message = shipFound
                ? $"Diagonally from position '{updatedButtonName}' radar found ship"
                : $"Diagonally from position '{updatedButtonName}' radar did not find ship";

            return message;
        }

        private List<string> GetDiagonalCells(string selectedCell)
        {
            List<string> diagonalCells = new List<string>();
            char column = selectedCell[0];
            int row = int.Parse(selectedCell.Substring(1));

            char currentColumn = column;
            int currentRow = row;
            while (currentColumn >= 'A' && currentRow >= 0)
            {
                AddDiagonalCell(diagonalCells, currentColumn, currentRow);
                currentColumn--;
                currentRow--;
            }
            currentColumn = (char)(column + 1);
            currentRow = row + 1;
            while (currentColumn <= BoardSize.WIDTH_LETTER && currentRow < BoardSize.HEIGHT)
            {
                AddDiagonalCell(diagonalCells, currentColumn, currentRow);
                currentColumn++;
                currentRow++;
            }

            currentColumn = column;
            currentRow = row;
            while (currentColumn <= BoardSize.WIDTH_LETTER && currentRow >= 0)
            {
                AddDiagonalCell(diagonalCells, currentColumn, currentRow);
                currentColumn++;
                currentRow--;
            }
            currentColumn = (char)(column - 1);
            currentRow = row + 1;
            while (currentColumn >= 'A' && currentRow < BoardSize.HEIGHT)
            {
                AddDiagonalCell(diagonalCells, currentColumn, currentRow);
                currentColumn--;
                currentRow++;
            }

            return diagonalCells;
        }


        private void AddDiagonalCell(List<string> cells, char column, int row)
        {
            if (column >= 'A' && column <= BoardSize.WIDTH_LETTER && row >= 0 && row < BoardSize.HEIGHT)
            {
                cells.Add($"{column}{row}");
            }
        }

        private bool DetectWithRadarIfShipFoundDiagonally(List<IShip> ships, List<string> cellsToCheck)
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

        private bool DetectWithRadarIfShipFoundDiagonally(List<ISpecialShip> ships, List<string> cellsToCheck)
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