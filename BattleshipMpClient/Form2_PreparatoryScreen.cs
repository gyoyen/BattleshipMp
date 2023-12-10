using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BattleshipMpClient.Entity;
using BattleshipMp.Builder;
using BattleshipMpClient.Factory.Ship;
using BattleshipMpClient.State;
using BattleshipMpClient.Iterator;

namespace BattleshipMpClient
{
    public partial class Form2_PreparatoryScreen : Form
    {
        private IShipFactory _shipFactory;
        private IShipBuilder _builder;
        private ShipsCreator _shipsCreator;

        public Form2_PreparatoryScreen()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetObsticlesUp();
        }

        public void AddShipFactory(IShipFactory shipFactory)
        {
            _shipFactory = shipFactory;
            _builder = new ShipBuilder(_shipFactory);
            _shipsCreator = new ShipsCreator(_builder);
        }

        #region ****************************************************** 2D Drawing ******************************************************
        private Point selectionStart;
        private Point selectionEnd;
        private Rectangle selection;
        private bool mouseDown;
        public List<ShipButtons> icebergTiles = new List<ShipButtons>();
        public List<Control> icebergButtons = new List<Control>();
        public Iceberg iceberg = new Iceberg();



        protected override void OnMouseDown(MouseEventArgs e)
        {
            selectionStart = PointToClient(MousePosition);
            mouseDown = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseDown = false;

            SetSelectionRect();
            Invalidate();

            GetSelectedButtons();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!mouseDown)
            {
                return;
            }

            selectionEnd = PointToClient(MousePosition);
            SetSelectionRect();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (mouseDown)
            {
                using (Pen pen = new Pen(Color.Black, 1F))
                {
                    pen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, selection);
                }
            }
        }

        private void SetObsticlesUp()
        {
            foreach(Control c in Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "E4")
                    {
                        c.BackColor = Color.Blue;
                        icebergButtons.Add(c);
                    }
                    if (c.Name == "F4")
                    {
                        c.BackColor = Color.Blue;
                        icebergButtons.Add(c);
                    }
                }
            }
            
            ShipButtons tiles = new ShipButtons();
            List<string> strings = new List<string>
            {
                "E5",
                "F5"
            };

            tiles.buttonNames = strings;
            icebergTiles.Add(tiles);
            iceberg.ReplaceTiles(icebergTiles);
        }

        private void SetSelectionRect()
        {
            int x, y;
            int width, height;

            x = selectionStart.X > selectionEnd.X ? selectionEnd.X : selectionStart.X;
            y = selectionStart.Y > selectionEnd.Y ? selectionEnd.Y : selectionStart.Y;

            width = selectionStart.X > selectionEnd.X ? selectionStart.X - selectionEnd.X : selectionEnd.X - selectionStart.X;
            height = selectionStart.Y > selectionEnd.Y ? selectionStart.Y - selectionEnd.Y : selectionEnd.Y - selectionStart.Y;

            selection = new Rectangle(x, y, width, height);
        }

        #endregion


        public static List<IShip> shipList = new List<IShip>();
        public static List<ISpecialShip> specialShipList = new List<ISpecialShip>();

        List<(string, Color)> AllSelectedButtonList = new List<(string, Color)>();

        bool isPanelActive = true;

        private void Form2_Load(object sender, EventArgs e)
        {
            shipList = null;
            CreateShipList();
            RemainingShips();
            timer1.Start();
        }

        // 1 // In the first step, create ships derived from the "Ship" model (class) and list these ships.
        private void CreateShipList()
        {
            shipList = _shipsCreator.BuildNormalShips();
            specialShipList = _shipsCreator.BuildSpecialShips();
        }
        private bool ValidateTile(Control button)
        {
            foreach (Control obsticle in icebergButtons)
            {
                if (obsticle.Name == button.Name)
                {
                    return true;
                }
            }

            return false;
        }

        private void ValidateSelection()
        {
            foreach (Control c in Controls)
            {
                if (c is Button && selection.IntersectsWith(c.Bounds) && ValidateTile(c))
                {
                    MessageBox.Show("Can't create ship over obsticle.");
                    selection = new Rectangle();

                    return;
                }
            }
        }

        private void GetSelectedButtons()
        {
            ValidateSelection();
            List<Button> selected = new List<Button>();

            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    if (selection.IntersectsWith(c.Bounds))
                    {
                        selected.Add((Button)c);
                    }
                }
            }

            List<string> buttonsNames = new List<string>();

            foreach (Control c in selected)
            {
                buttonsNames.Add(c.Name);
            }

            buttonsNames.Sort();

            List<int> totalAsciiList = new List<int>();
            foreach (var item in buttonsNames)
            {
                char ch1 = char.Parse(item.Substring(0, 1));
                char ch2 = char.Parse(item.Substring(item.Length - 1));
                totalAsciiList.Add((int)ch1 + (int)ch2);
            }

            if (totalAsciiList.Count > 1)
            {
                for (int i = 0; i < totalAsciiList.Count - 1; i++)
                {
                    if (totalAsciiList[i] + 1 != totalAsciiList[i + 1])
                    {
                        MessageBox.Show("The area where the ship will be located must be consecutive boxes.");
                        return;
                    }
                }
            }

            foreach (var item in selected)
            {
                foreach (var ship in shipList)
                    if (item.BackColor == ship.color)
                    {
                        DeleteShip(selected);
                        return;
                    }
                foreach (var ship in specialShipList)
                    if (item.BackColor == ship.color)
                    {
                        DeleteShip(selected);
                        return;
                    }
            }

            if (selected.Count > 0 && selected.Contains(buttonStart) == false)
            {
                using (var frm3 = new Form3_ShipSelectScreen(buttonsNames))
                {
                    var res = frm3.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        string val = frm3.returnValue;
                        foreach (var item in shipList)
                        {
                            if (item.shipName == val)
                            {
                                item.remShips--;

                                if (item.shipPerButton == null)
                                    item.shipPerButton = new List<ShipButtons>();

                                ShipButtons sb = new ShipButtons() { buttonNames = new List<string>() };
                                foreach (var item2 in selected)
                                {
                                    item2.BackColor = item.color;
                                    //AllSelectedButtonList.Add(item2.Name);
                                    sb.buttonNames.Add(item2.Name);
                                }
                                item.shipPerButton.Add(sb);

                                //AllSelectedButtonList.Sort();
                                RemainingShips();
                                break;
                            }
                        }

                        foreach (var item in specialShipList)
                        {
                            if (item.shipName == val)
                            {
                                item.remShips--;

                                if (item.shipPerButton == null)
                                    item.shipPerButton = new List<ShipButtons>();

                                ShipButtons sb = new ShipButtons() { buttonNames = new List<string>() };
                                foreach (var item2 in selected)
                                {
                                    item2.BackColor = item.color;
                                    sb.buttonNames.Add(item2.Name);
                                }
                                item.shipPerButton.Add(sb);

                                RemainingShips();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void DeleteShip(List<Button> selected)
        {
            DialogResult dres = MessageBox.Show("Are you sure you want to delete the ship?", "Delete Ship", MessageBoxButtons.YesNo);
            if (dres == DialogResult.Yes)
            {
                foreach (var item1 in shipList)
                {
                    if (item1.shipPerButton != null)
                    {
                        foreach (var item2 in item1.shipPerButton)
                        {
                            foreach (var item3 in selected)
                            {
                                if (item2.buttonNames.FirstOrDefault(x => x.Equals(item3.Name)) != null)
                                {
                                    item1.remShips++;
                                    foreach (var item4 in item2.buttonNames)
                                    {
                                        this.Controls.Find(item4, true)[0].BackColor = Color.Transparent;
                                    }
                                    item1.shipPerButton.Remove(item2);
                                    RemainingShips();
                                    return;
                                }
                            }
                        }
                    }
                }

                foreach (var item1 in specialShipList)
                {
                    if (item1.shipPerButton != null)
                    {
                        foreach (var item2 in item1.shipPerButton)
                        {
                            foreach (var item3 in selected)
                            {
                                if (item2.buttonNames.FirstOrDefault(x => x.Equals(item3.Name)) != null)
                                {
                                    item1.remShips++;
                                    foreach (var item4 in item2.buttonNames)
                                    {
                                        this.Controls.Find(item4, true)[0].BackColor = Color.Transparent;
                                    }
                                    item1.shipPerButton.Remove(item2);
                                    RemainingShips();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }


        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1- Hold down the mouse LC and select the location where you want to place the ships.\n\n" +
                "2- \r\nSelect from the page listing the ships that can be placed in the selected location.\n\n" +
                "3- Click 'Ready' when you have placed all your ships.\n\n" +
                "Notice: The game will start after both players 'Continue'.");
        }

        void RemainingShips()
        {
            lblBattleship.Text = lblBattleship.Text.Substring(0, lblBattleship.Text.Length - 1);
            lblBattleship.Text += specialShipList.FirstOrDefault(x => x.shipName == "Battleship").remShips.ToString();

            lblCruiser.Text = lblCruiser.Text.Substring(0, lblCruiser.Text.Length - 1);
            lblCruiser.Text += shipList.FirstOrDefault(x => x.shipName == "Cruiser").remShips.ToString();

            lblDestroyer.Text = lblDestroyer.Text.Substring(0, lblDestroyer.Text.Length - 1);
            lblDestroyer.Text += shipList.FirstOrDefault(x => x.shipName == "Destroyer").remShips.ToString();

            lblSubmarine.Text = lblSubmarine.Text.Substring(0, lblSubmarine.Text.Length - 1);
            lblSubmarine.Text += shipList.FirstOrDefault(x => x.shipName == "Submarine").remShips.ToString();

            lblSpecialSubmarine.Text = lblSpecialSubmarine.Text.Substring(0, lblSpecialSubmarine.Text.Length - 1);
            lblSpecialSubmarine.Text += specialShipList.FirstOrDefault(x => x.shipName == "SpecialSubmarine").remShips.ToString();

            lblSpecialCruiser.Text = lblSpecialCruiser.Text.Substring(0, lblSpecialCruiser.Text.Length - 1);
            lblSpecialCruiser.Text += specialShipList.FirstOrDefault(x => x.shipName == "SpecialCruiser").remShips.ToString();

            lblSpecialDestroyer.Text = lblSpecialDestroyer.Text.Substring(0, lblSpecialDestroyer.Text.Length - 1);
            lblSpecialDestroyer.Text += specialShipList.FirstOrDefault(x => x.shipName == "SpecialDestroyer").remShips.ToString();

            isPanelActive = true;
            ShipCollection shipCollection = new ShipCollection(shipList);
            var shipIterator = shipCollection.CreateIterator();
            SpecialShipCollection specialShipCollection = new SpecialShipCollection(specialShipList);
            var specialShipIterator = specialShipCollection.CreateIterator();

            while (shipIterator.HasNext())
            {
                IShip ship = shipIterator.Next();
                if (ship.remShips != 0)
                {
                    isPanelActive = false;
                    break;
                }
            }

            if (isPanelActive)
            {
                while (specialShipIterator.HasNext())
                {
                    ISpecialShip specialShip = specialShipIterator.Next();
                    if (specialShip.remShips != 0)
                    {
                        isPanelActive = false;
                    }
                }
            }
            if (isPanelActive == true)
            {
                buttonStart.Enabled = true;
                buttonStart.Text = "Ready.\nStart";
            }
            else
            {
                buttonStart.Enabled = false;
                buttonStart.Text = "Preparatory";
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            
            GameContext gameContext = GameContext.Instance;
            var ships = FillAllButtonList();
            gameContext.SetTheme(ships);
            PlayerTurnState playerTurnState = PlayerTurnState.Instance;
            gameContext.TransitionTo(playerTurnState);
            this.Visible = false;
        }

        private List<(string, Color)> FillAllButtonList()
        {
            foreach (var item1 in shipList)
            {
                foreach (var item2 in item1.shipPerButton)
                {
                    foreach (var item3 in item2.buttonNames)
                    {
                        AllSelectedButtonList.Add((item3, item1.color));
                    }
                }
            }
            foreach (var item1 in specialShipList)
            {
                foreach (var item2 in item1.shipPerButton)
                {
                    foreach (var item3 in item2.buttonNames)
                    {
                        AllSelectedButtonList.Add((item3, item1.color));
                    }
                }
            }
            return AllSelectedButtonList;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Client.GetInstance.IsConnected)
            {
                MessageBox.Show("Connection failed.");
                Client.GetInstance.CloseAndDispose();
                timer1.Stop();
                this.Close();
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            Form1_ClientScreen frm1 = new Form1_ClientScreen();
            frm1.Visible = true;
        }
    }
}
