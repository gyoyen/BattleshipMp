using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient.State
{
    class GameContext
    {
        private IGameState _state;
        private String victoryMessage;
        List<(String, System.Drawing.Color)> theme;

        private static GameContext _instance;

        private GameContext() { }

        public static GameContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameContext();
                }
                return _instance;
            }
        }

        public GameContext(IGameState initialState)
        {
            TransitionTo(initialState);
        }

        public void TransitionTo(IGameState newState)
        {
            _state = newState;
            _state.EnterState(this);
        }

        public void HandleInput(string input)
        {
            _state.HandleInput(this, input);
        }

        public void Update()
        {
            _state.Update(this);
        }

        public void DisplayGameOver()
        {
            DialogResult res = MessageBox.Show(victoryMessage, "Client - Game Result", MessageBoxButtons.OK);
            {
                if (res == DialogResult.Yes)
                {
                    Environment.Exit(1);
                }
                else
                    Environment.Exit(1);
            }
        }

        public void SetTheme(List<(String, System.Drawing.Color)> theme)
        {
            this.theme = theme;
        }

        public List<(String, System.Drawing.Color)> GetTheme()
        {
            return this.theme;
        }

        public void SetVictoryMessage(string message)
        {
            this.victoryMessage = message;
        }

        public string GetVictoryMessage()
        {
            return this.victoryMessage;
        }
    }
}
