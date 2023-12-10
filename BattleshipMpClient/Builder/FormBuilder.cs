using BattleshipMpClient;
using BattleshipMpClient.Factory.Ship;
using System.Drawing;

namespace BattleshipMp.Builder
{
    public class FormBuilder : IFormBuilder
    {
        private readonly Form2_PreparatoryScreen _form;

        public FormBuilder()
        {
            _form = new Form2_PreparatoryScreen();
        }

        public void AddFormColor(Color color)
        {
            _form.BackColor = color;
        }

        public void AddForegroundColor(Color color)
        {
            _form.ForeColor = color;
        }

        public void AddShipFactory(IShipFactory shipFactory)
        {
            _form.AddShipFactory(shipFactory);
        }

        public Form2_PreparatoryScreen Build()
        {
            return _form;
        }
    }
}