using BattleshipMpClient.Entity;
using BattleshipMpClient.Iterator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient.Adapter
{
    public interface IIcebergShipInteractionAdapter
    {
        void ProcessIcebergShipCollision(Iceberg iceberg, List<(string, Color)> shipButtons, Form4_GameScreen screen, out bool isIceberg, IIcebergIterator icebergIterator);
    }
}
