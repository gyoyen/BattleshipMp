using System;
using System.Windows.Forms;
using BattleshipMp.Builder;

namespace BattleshipMp
{
    public partial class Form12_ShipThemeSelection : Form
    {
        private readonly FormCreator _formCreator;

        public Form12_ShipThemeSelection()
        {
            IFormBuilder builder = new FormBuilder();
            _formCreator = new FormCreator(builder);
            InitializeComponent();
        }

        private void btnLightTheme_Click(object sender, EventArgs e)
        {
            var frm2 = _formCreator.BuildLightForm();
            frm2.Show();
            this.Close();
        }

        private void btnDarkTheme_Click(object sender, EventArgs e)
        {
            var frm2 = _formCreator.BuildDarkForm();
            frm2.Show();
            this.Close();
        }
    }
}