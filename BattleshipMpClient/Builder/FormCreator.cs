using BattleshipMpClient;
using BattleshipMpClient.Factory.Ship;
using System.Drawing;

namespace BattleshipMp.Builder
{
    public class FormCreator
    {
        private readonly IFormBuilder _builder;

        public FormCreator(IFormBuilder builder)
        {
            _builder = builder;
        }

        public Form2_PreparatoryScreen BuildDarkForm()
        {
            _builder.AddFormColor(Color.Gray);
            _builder.AddForegroundColor(Color.White);
            _builder.AddShipFactory(new DarkShipFactory());

            return _builder.Build();
        }

        public Form2_PreparatoryScreen BuildLightForm()
        {
            _builder.AddFormColor(Color.White);
            _builder.AddForegroundColor(Color.Black);
            _builder.AddShipFactory(new LightShipFactory());

            return _builder.Build();
        }
    }
}