using BattleshipMpServer.Bridge.Abstraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpServer.Command
{
    public class MissCommand : ICommand
    {
        private readonly List<Button> _gameBoardButtons;
        private readonly RichTextBox _richTextBox;
        private readonly SoundPlayerBridge _missSoundPlayer;
        private readonly string _receive;

        private Image _previousBackgroundImage; // Store the previous state for undo

        public MissCommand(List<Button> gameBoardButtons, RichTextBox richTextBox, SoundPlayerBridge missSoundPlayer, string receive)
        {
            _gameBoardButtons = gameBoardButtons;
            _richTextBox = richTextBox;
            _missSoundPlayer = missSoundPlayer;
            _receive = receive;
        }

        public void Execute()
        {
            string result = _receive.Substring(_receive.Length - 2, 2);
            result = result + result.Substring(result.Length - 1);
            var button = _gameBoardButtons.FirstOrDefault(x => x.Name == result);

            if (button != null)
            {
                // Store the previous state for undo
                _previousBackgroundImage = button.BackgroundImage;
                try
                {
                    button.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Images\o.png");
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it or show a message to the user)
                    Console.WriteLine($"Error loading image: {ex.Message}");
                }
            }

            _richTextBox.AppendText("Miss\n");
            _missSoundPlayer.Play();
        }

        public void Undo(Button button)
        {
            Console.WriteLine("Undoing MissCommand");
            string result = _receive.Substring(_receive.Length - 2, 2);
            result = result + result.Substring(result.Length - 1);
            var button1 = _gameBoardButtons.FirstOrDefault(x => x.Name == result);

            if (button != null)
            {
                Console.WriteLine("Restoring previous state");
                button1.BackgroundImage = _previousBackgroundImage; // Restore the previous state
                button.BackgroundImage = null;
            }
            else
            {
                Console.WriteLine("Button or previous background image is null");
            }
        }
    }
}
