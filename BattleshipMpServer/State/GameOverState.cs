using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMp.State
{
    class GameOverState : IGameState
    {
        public void EnterState(GameContext context)
        {
            context.DisplayGameOver();
        }

        public void HandleInput(GameContext context, string input)
        {
            // Game over
        }

        public void Update(GameContext context)
        {
            // Game over
        }
    }
}
