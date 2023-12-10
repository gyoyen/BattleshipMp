using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipMpClient.Factory.Item
{
    public class Jam : IItem
    {
        public string itemName => "Jam";
        public int remItems { get; set; } = 1;

        public string Activate()
        {
            Form4_GameScreen.setRemainingJams(3);
            return $"Enemy's ability to use items was jammed";
        }
    }
}
