using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient
{
    public partial class Form3_ShipSelectScreen : Form
    {
        List<string> buttonsNames;

        public string returnValue { get; set; }

        Dictionary<string, int> squarePerShips = new Dictionary<string, int>()
        {
            {"Cruiser", 3}, {"Destroyer", 2}, {"Submarine", 1}
        };

        Dictionary<string, int> squarePerSpecialShips = new Dictionary<string, int>()
        {
            {"Battleship", 4}, {"SpecialCruiser", 3}, {"SpecialDestroyer", 2}, {"SpecialSubmarine", 1}
        };


        public Form3_ShipSelectScreen(List<string> buttonsNames)
        {
            this.buttonsNames = buttonsNames;
            InitializeComponent();
        }

        private bool AreAllShipsOfSquareSizePlaced(int squareSize)
        {
            int totalRemShips = 0;

            foreach (var ship in Form2_PreparatoryScreen.shipList)
            {
                if (squarePerShips.ContainsKey(ship.shipName) && squarePerShips[ship.shipName] == squareSize)
                {
                    totalRemShips += ship.remShips;
                }
            }

            foreach (var specialShip in Form2_PreparatoryScreen.specialShipList)
            {
                if (squarePerSpecialShips.ContainsKey(specialShip.shipName) && squarePerSpecialShips[specialShip.shipName] == squareSize)
                {
                    totalRemShips += specialShip.remShips;
                }
            }

            return totalRemShips <= 0;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (var item1 in Form2_PreparatoryScreen.shipList)
            {
                foreach (var item2 in squarePerShips)
                {
                    if (item1.shipName == item2.Key)
                    {
                        if (item1.remShips != 0 && item2.Value == buttonsNames.Count)
                        {
                            if (AreAllShipsOfSquareSizePlaced(item2.Value))
                            {
                                MessageBox.Show("There are no ships to place in the selected area.");
                                this.Close();
                                return;
                            }
                            listBox1.Items.Add(item1.shipName);
                        }
                    }
                }

            }

            foreach (var item1 in Form2_PreparatoryScreen.specialShipList)
            {
                foreach (var item2 in squarePerSpecialShips)
                {
                    if (item1.shipName == item2.Key)
                    {
                        if (item1.remShips != 0 && item2.Value == buttonsNames.Count)
                        {
                            if (AreAllShipsOfSquareSizePlaced(item2.Value))
                            {
                                MessageBox.Show("There are no ships to place in the selected area.");
                                this.Close();
                                return;
                            }
                            listBox1.Items.Add(item1.shipName);
                        }
                    }
                }

            }

            if (listBox1.Items.Count != 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                this.returnValue = listBox1.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
