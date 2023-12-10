using BattleshipMpServer.Factory.Ship;
using System.Drawing;

namespace BattleshipMp.Builder
{
    public interface IFormBuilder
    {
        void AddFormColor(Color color);
        void AddForegroundColor(Color color);
        void AddShipFactory(IShipFactory shipFactory);
        Form2_PreparatoryScreen Build();
    }
}