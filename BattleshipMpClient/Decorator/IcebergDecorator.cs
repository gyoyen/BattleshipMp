using BattleshipMpClient.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient.Decorator
{
    public class IcebergDecorator : Iceberg
    {
        private Iceberg baseIceberg;
        private int decorationLevel;
        private Color color = Color.Blue;
        private bool ExtraSpawn = false;

        public IcebergDecorator(Iceberg iceberg, int initialDecorationLevel = 0)
        {
            this.baseIceberg = iceberg;
            this.decorationLevel = initialDecorationLevel;
        }

        public void SetDecorationLevel(int level)
        {
            decorationLevel = level;
        }

        public void PerformEnhancedBehavior()
        {

            switch (decorationLevel)
            {
                case 1:
                    ChangeColor();
                    break;
                case 2:
                    MoveToOtherTile();
                    break;
                case 3:
                    DestroySelf();
                    break;
                default:
                    break;
            }
        }
        public Color getColor()
        {
            return color;
        }
        public void ChangeColor()
        {
            this.color = Color.Gold;
        }

        public void MoveToOtherTile()
        {
            this.ExtraSpawn = true;
        }

        public void DestroySelf()
        {
            this.baseIceberg = null;
        }

        public Iceberg getIceberg()
        {
            return this.baseIceberg;
        }

        public bool GetExtraSpawn()
        {
            return this.ExtraSpawn;
        }
    }
}
