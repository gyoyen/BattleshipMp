using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.State
{
    internal class SetupState : IGameState
    {
        public void EnterState(GameContext context)
        {
            Form12_ShipThemeSelection frmThemeSelection = new Form12_ShipThemeSelection();
            frmThemeSelection.Show();
        }

        public void HandleInput(GameContext context, string input)
        {
            // setup
        }

        public void Update(GameContext context)
        {
            // setup
        }
    }
}
