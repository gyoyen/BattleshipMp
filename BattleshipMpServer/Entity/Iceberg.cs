using BattleshipMpServer.Entity;
using BattleshipMpServer.Factory.Ship;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BattleshipMpServer.Entity
{
    public class Iceberg : Obsticle
    {
        public Iceberg() : base()
        {
            this.obsticlePerButton = new List<Control>();
        }

        public Iceberg(List<Control> obsticleButtons)
        {
            this.obsticlePerButton = obsticleButtons;
        }

        public void ReplaceTiles(List<ShipButtons> obsticleButtons)
        {
            this.obsticlePerButton = obsticlePerButton;
        }

        public void AddTiles(Control obsticleButtons)
        {
            this.obsticlePerButton.Add(obsticleButtons);
        }

        public override Obsticle ShallowCopy()
        {
            return (Obsticle)this.MemberwiseClone();
        }

        public override Obsticle DeepCopy()
        {
            Iceberg newIceberg = (Iceberg)this.MemberwiseClone();
            newIceberg.obsticlePerButton = new List<Control>(this.obsticlePerButton);
            return newIceberg;
        }

        public string GenerateRandomTile()
        {
            Random rnd = new Random();

            int randomNumber = rnd.Next(1, 10);
            char randomLetter = (char)('A' + rnd.Next(0, 10));
            string combined = randomLetter + randomNumber.ToString();

            return combined;
        }

        internal Color getColor()
        {
            return Color.Blue;
        }
    }
}
