using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient.State
{
    class EnemyTurnState : IGameState
    {
        private Form4_GameScreen form4_GameScreen;

        private static EnemyTurnState _instance;

        private EnemyTurnState() { }

        public static EnemyTurnState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EnemyTurnState();
                }
                return _instance;
            }
        }

        public void EnterState(GameContext context)
        {
            form4_GameScreen = new Form4_GameScreen(context.GetTheme());
        }

        public void HandleInput(GameContext context, string input)
        {
            form4_GameScreen.AttackFromEnemy(input);
        }

        public void Update(GameContext context)
        {
            if (form4_GameScreen.selectedShipList.Count > 0)
            {
                PlayerTurnState nextState = PlayerTurnState.Instance;
                context.TransitionTo(nextState);
            }
        }
    }
}
