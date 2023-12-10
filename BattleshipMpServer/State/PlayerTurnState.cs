using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMp.State
{
    class PlayerTurnState : IGameState
    {
        private Form4_GameScreen preparatoryScreen;

        private static PlayerTurnState _instance;

        private PlayerTurnState() { }

        public static PlayerTurnState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerTurnState();
                }
                return _instance;
            }
        }

        public void EnterState(GameContext context)
        {
            preparatoryScreen = new Form4_GameScreen(context.GetTheme());
            preparatoryScreen.Show();
        }

        public void HandleInput(GameContext context, string input)
        {
            preparatoryScreen.AttackToEnemy(input);
        }

        public void Update(GameContext context)
        {
            if (preparatoryScreen.selectedShipList.Count > 0)
            {
                EnemyTurnState nextState = EnemyTurnState.Instance;
                context.TransitionTo(EnemyTurnState.Instance);
            }
        }
    }
}
