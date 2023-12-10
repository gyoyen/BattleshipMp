using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BattleshipMpClient.Factory.Ship;
using BattleshipMpClient.Factory.Item;
using SharedFile.Facade;
using BattleshipMpClient.Facade;
using BattleshipMpClient.Entity;
using BattleshipMpClient.Strategy;
using BattleshipMpClient.Observer;
using BattleshipMpClient.Bridge.Abstraction;
using BattleshipMpClient.Bridge.Concrete;
using BattleshipMp.Builder;
using BattleshipMpClient.Adapter;
using BattleshipMpClient.Decorator;
using BattleshipMpClient.Command;
using System.Threading.Tasks;
using BattleshipMpClient.State;
using BattleshipMpClient.Iterator;

namespace BattleshipMpClient
{
    public partial class Form4_GameScreen : Form, IIcebergAggregate
    {
        private GameFacade gameFacade;
        private int currentPlayer;
        public List<(string, Color)> selectedShipList;
        //public StreamReader STR;
        //public StreamWriter STW;
        public string recieve;
        public string TextToSend;
        List<Button> gameBoardButtons;
        List<Button> myBoardButtons;
        bool areEnabledButtons = true;
        bool myExit = false;
        List<Control> icebergButtons = new List<Control>();
        List<ShipButtons> icebergTiles = new List<ShipButtons>();
        List<Iceberg> icebergs = new List<Iceberg>();
        Iceberg motherIceberg = new Iceberg();
        bool enemyHasUsedRadarUse = false;
        bool hasRadarUse = true;
        bool enemyReceivedExtraRound = false;
        bool weHaveReceivedExtraRound = false;
        private readonly RadarStrategyGenerator _radarStrategyGenerator;
        private readonly ExtraRoundSubscriberMap _extraRoundSubscriberMap;
        private readonly ExtraRoundPublisher _extraRoundPublisher;
        private IcebergShipInteractionAdapter icebergShipInteractionAdapter = new IcebergShipInteractionAdapter();
        private const int PERCENTAGE_MAX = 100;
        private HashSet<string> clickedButtons = new HashSet<string>();
        private readonly IFormBuilder _formBuilder;
        private readonly FormCreator _formCreator;
        bool isIceberg = false;
        bool skipIcebergChange = false;
        int turns = 0;
        IItem playerItem;
        IItem playerItem2;
        IItem playerItem3;
        static private int remainingJams = 0;
        private Stack<ICommand> commandHistory = new Stack<ICommand>();
        private delegate bool StateChecker();
        private IIcebergIterator icebergIterator;
        public Button myBoardButtonToUndo;

        public Form4_GameScreen(List<(string, Color)> list)
        {
            InitializeComponent();
            GameContext gameContext = GameContext.Instance;
            this.selectedShipList = list;
            gameContext.SetTheme(this.selectedShipList);

            Control.CheckForIllegalCrossThreadCalls = false;

            _formBuilder = new FormBuilder();
            _formCreator = new FormCreator(_formBuilder);
            _radarStrategyGenerator = new RadarStrategyGenerator();
            _extraRoundSubscriberMap = new ExtraRoundSubscriberMap();
            _extraRoundPublisher = new ExtraRoundPublisher();

            foreach (var subscriber in _extraRoundSubscriberMap.GetSubscribers())
            {
                _extraRoundPublisher.Subscribe(subscriber);
            }

            ITcpStreamProvider tcpStreamProvider;
            tcpStreamProvider = new TcpStreamProviderClient();

            gameFacade = new GameFacade(tcpStreamProvider);

            IItemFactory itemFactory = new ItemFactory();
            playerItem = itemFactory.CreateFindShipItem();
            playerItem2 = itemFactory.CreateBattleshipHitItem();
            playerItem3 = itemFactory.CreateJamItem();
            icebergIterator = CreateIterator();
        }
        public IIcebergIterator CreateIterator()
        {
            return new IcebergIterator(icebergs);
        }

        public void AddIceberg(Iceberg iceberg)
        {
            icebergs.Add(iceberg);
        }

        private void button_mousehover(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                Bitmap bm = new Bitmap(new Bitmap(Application.StartupPath + @"\Images\target.png"), 20, 20);
                button.Cursor = new Cursor(bm.GetHicon());
            }
        }

        private void button_mouseleave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.Cursor = Cursors.Default;
            }
        }

        private void SetObsticlesUp()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "E4")
                    {
                        c.BackColor = Color.Blue;
                        icebergButtons.Add(c);
                        motherIceberg.AddTiles(c);
                    }
                    if (c.Name == "F4")
                    {
                        c.BackColor = Color.Blue;
                        icebergButtons.Add(c);
                        motherIceberg.AddTiles(c);
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
            icebergs.Add(motherIceberg);
            AddIceberg(motherIceberg);
        }

        private void ExpandObsticle()
        {
            Iceberg iceberg = new Iceberg();
            Random rnd = new Random();
            int randomNum = rnd.Next(0, 4);
            IcebergDecorator icebergDecorator = new IcebergDecorator(iceberg, randomNum);
            string randomTileName = icebergDecorator.GenerateRandomTile();
            icebergDecorator.PerformEnhancedBehavior();
            iceberg = (Iceberg)motherIceberg.DeepCopy();

            switch (randomNum)
            {
                case 1:
                    richTextBox1.AppendText("Iceberg changed color\n");
                    break;
                case 2:
                    richTextBox1.AppendText("Another iceberg was created\n");
                    break;
                case 3:
                    richTextBox1.AppendText("Iceberg has melted\n");
                    break;
                default:
                    richTextBox1.AppendText("Iceberg\n");
                    break;
            }

            if (icebergDecorator.getIceberg() != null)
            {
                if (icebergDecorator.GetExtraSpawn())
                {
                    string secondRandomTileName = icebergDecorator.GenerateRandomTile();

                    foreach (Control c in groupBox1.Controls)
                    {
                        if (c is Button && c.Name == secondRandomTileName)
                        {
                            Iceberg extraIceberg = (Iceberg)icebergDecorator.ShallowCopy();
                            c.BackColor = extraIceberg.getColor();
                            icebergButtons.Add(c);
                            extraIceberg.AddTiles(c);
                            AddIceberg(extraIceberg);
                            icebergShipInteractionAdapter.ProcessIcebergShipCollision(extraIceberg, selectedShipList, this, out isIceberg, icebergIterator);
                        }
                    }

                    secondRandomTileName = icebergDecorator.GenerateRandomTile();

                    foreach (Control c in groupBox1.Controls)
                    {
                        if (c is Button && c.Name == secondRandomTileName)
                        {
                            Iceberg extraIceberg = (Iceberg)icebergDecorator.ShallowCopy();
                            c.BackColor = extraIceberg.getColor();
                            icebergButtons.Add(c);
                            extraIceberg.AddTiles(c);
                            AddIceberg(extraIceberg);
                            icebergShipInteractionAdapter.ProcessIcebergShipCollision(extraIceberg, selectedShipList, this, out isIceberg, icebergIterator);
                        }
                    }
                }

                foreach (Control c in groupBox1.Controls)
                {
                    if (c is Button && c.Name == randomTileName)
                    {
                        c.BackColor = icebergDecorator.getColor();
                        icebergButtons.Add(c);
                        icebergDecorator.AddTiles(c);
                        icebergShipInteractionAdapter.ProcessIcebergShipCollision(icebergDecorator, selectedShipList, this, out isIceberg, icebergIterator);
                    }
                }
            }
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            gameBoardButtons = groupBox2.Controls.OfType<Button>().ToList();
            myBoardButtons = groupBox1.Controls.OfType<Button>().ToList();

            GameContext gameContext = GameContext.Instance;

            gameContext.SetTheme(this.selectedShipList);
            foreach (var item in gameContext.GetTheme())
            {
                groupBox1.Controls.Find(item.Item1, true)[0].BackColor = item.Item2;
            }

            try
            {
                ITcpStreamProvider tcpStreamProvider;
                tcpStreamProvider = new TcpStreamProviderClient();
                gameFacade.GetGameCommunication().StartGameCommunication(tcpStreamProvider);
                //STR = new StreamReader(Client.GetInstance.TcpClient.GetStream());
                //STW = new StreamWriter(Client.GetInstance.TcpClient.GetStream());
                //STW.AutoFlush = true;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.WorkerSupportsCancellation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            timer1.Start();
            SetObsticlesUp();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Client.GetInstance.IsConnected)
            {
                try
                {
                    string recieve = gameFacade.GetAttackReceiver().ReceiveAttack();
                    if (!string.IsNullOrEmpty(recieve))
                    {
                        AttackFromEnemy(recieve);

                        if (!enemyReceivedExtraRound && !weHaveReceivedExtraRound)
                        {
                            if (turns % 2 == 0)
                            {
                                ExpandObsticle();
                            }
                        }
                    }
                    //recieve = STR.ReadLine();

                    //if (recieve != "")
                    //{
                    //AttackFromEnemy(recieve);
                    //}
                    //recieve = "";
                    turns++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (Client.GetInstance.IsConnected)
            //{
            //    STW.WriteLine(TextToSend);
            //}
            //else
            //{
            //    MessageBox.Show("Message could not be sent!!");
            //}

            //gameFacade.SendAttack(TextToSend);
            gameFacade.GetAttackSender().SendAttack(TextToSend, new TcpStreamProviderClient());
            backgroundWorker2.CancelAsync();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (clickedButton == null || !clickedButton.Enabled)
            {
                return;
            }

            GameContext gameContext = GameContext.Instance;
            gameContext.HandleInput(clickedButton.Name);
            //AttackToEnemy(clickedButton.Name);
        }

        public void UndoLastMove(Button button)
        {
            if (commandHistory.Count > 0)
            {
                ICommand lastCommand = commandHistory.Pop();
                lastCommand.Undo(button);
            }
        }


        public void AttackFromEnemy(string recieve)
        {
            ISoundImplementation hitSoundImplementation = new HitSound();
            ISoundImplementation missSoundImplementation = new MissSound();
            SoundPlayerBridge hitSoundPlayer = new HitSoundPlayer(hitSoundImplementation);
            SoundPlayerBridge missSoundPlayer = new MissSoundPlayer(missSoundImplementation);
            ICommand command = CreateCommand(recieve, gameBoardButtons, richTextBox1, hitSoundPlayer, missSoundPlayer);
            command.Execute();
            if (command is HitCommand || command is MissCommand)
            {
                commandHistory.Push(command);
                return;
            }
            
            if (recieve == "0")
            {
                areEnabledButtons = true;
                hasRadarUse = true;
                SwitchGameButtonsEnabled();
                return;
            }
            else if (recieve == "1")
            {
                areEnabledButtons = false;
                SwitchGameButtonsEnabled();
                return;
            }
            else if (recieve.Contains("hitShielded:"))
            {
                string result = recieve.Substring(recieve.Length - 2, 2);
                result = result + result.Substring(result.Length - 1);
                gameBoardButtons.FirstOrDefault(x => x.Name == result).BackgroundImage = Image.FromFile(Application.StartupPath + @"\Images\shield.png");
                richTextBox1.AppendText("Hit shield\n");
                hitSoundPlayer.Play();
                return;
            }
            else if (recieve.Contains("youwin"))
            {
                //DialogResult res = MessageBox.Show("Victory.", "Client - Game Result", MessageBoxButtons.OK);
                //{
                //    if (res == DialogResult.Yes)
                //    {
                //        Environment.Exit(1);
                //    }
                //    else
                //        Environment.Exit(1);
                //}
                GameContext gameContext = GameContext.Instance;
                gameContext.SetVictoryMessage("Victory");
                gameContext.TransitionTo(new GameOverState());
                return;
            }
            else if (recieve.Contains("exit"))
            {
                MessageBox.Show("\r\nThe opponent has left the game. You are being directed to the preparation phase.");
                this.Close();
                return;
            } 
            else if (recieve.Contains("[Radar]"))
            {
                richTextBox1.AppendText($"{recieve}\n");
                hasRadarUse = false;
                return;
            }
            else if (recieve.Contains("Extra round"))
            {
                weHaveReceivedExtraRound = true;
                richTextBox1.AppendText($"[Lucky] The block you selected gave you extra round!\n");
                SwitchGameButtonsEnabled();
                return;
            } 
            else if (recieve.Contains("iceberg"))
            {
                isIceberg = true;
                return;
            }

            else if (recieve.Contains("[Item]"))
            {
                richTextBox1.AppendText($"{recieve}\n");
                return;
            }

            else if (recieve.Contains("[EnemyItem]"))
            {
                var message = playerItem.Activate();

                AttackToEnemy($"[Item] {message}");

                return;
            }
            else if (recieve.Contains("[BsItem]"))
            {
                richTextBox1.AppendText($"{recieve}\n");
                return;
            }

            else if (recieve.Contains("[EnemyBsItem]"))
            {
                var message = playerItem2.Activate();

                AttackToEnemy($"[BsItem] {message}");

                return;
            }
            else if (recieve.Contains("[JamItem]"))
            {
                richTextBox1.AppendText($"{recieve}\n");
                return;
            }

            else if (recieve.Contains("[EnemyJamItem]"))
            {
                var message = playerItem3.Activate();

                richTextBox1.AppendText($"[JamItem] Your ability to use items was jammed!\n");
                AttackToEnemy($"[JamItem] {message}");

                return;
            }
            if (!enemyHasUsedRadarUse)
            {
                var radar = new Radar(_radarStrategyGenerator);

                var buttonToShoot = recieve.Substring(0, recieve.Length - 1);
                var message = radar.ScanAreaWithRandomStrategy(buttonToShoot);

                AttackToEnemy($"[Radar] {message}");

                enemyHasUsedRadarUse = true;
                return;
            }
            else if (recieve.Contains("[Item]"))
            {
                richTextBox1.AppendText($"{recieve}\n");
                return;
            }
            var extraSubscriberToGet = recieve.Substring(0, recieve.Length - 1);
            var rnd = new Random();
            var extraSubscriberOnClickedButton = _extraRoundSubscriberMap.GetExtraRoundSubscriber(extraSubscriberToGet);
            enemyReceivedExtraRound = extraSubscriberOnClickedButton.GetExtraRoundChancePercentages() > rnd.Next(PERCENTAGE_MAX + 1) && !isIceberg && extraSubscriberOnClickedButton.Enabled;
            if (enemyReceivedExtraRound)
            {
                AttackToEnemy("Extra round");
                SwitchGameButtonsEnabled();
            }

            bool hasShield = false;
            bool isShot = false;
            string shotButtonName = "";
            string shottedShip = "";
            ShipButtons deletingButton = null;


            if (Form2_PreparatoryScreen.shipList[0].shipPerButton == null)
            {
                return;
            }
            if (Form2_PreparatoryScreen.specialShipList[0].shipPerButton == null)
            {
                return;
            }

            foreach (var item1 in Form2_PreparatoryScreen.shipList)
            {
                foreach (var item2 in item1.shipPerButton)
                {
                    foreach (string item3 in item2.buttonNames)
                    {
                        if (item3 == recieve.Substring(0, recieve.Length - 1))
                        {

                            isShot = true;
                            shotButtonName = item3;
                            shottedShip = item1.shipName;
                            deletingButton = item2;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("shipList");
                            string xxx = recieve.Substring(0, recieve.Length - 1);
                            shotButtonName = xxx;
                        }
                    }
                }
            }
            foreach (var item1 in Form2_PreparatoryScreen.specialShipList)
            {
                foreach (var item2 in item1.shipPerButton)
                {
                    foreach (string item3 in item2.buttonNames)
                    {
                        if (item3 == recieve.Substring(0, recieve.Length - 1))
                        {

                            isShot = true;
                            shotButtonName = item3;
                            shottedShip = item1.shipName;
                            if (item1.remShields > 0)
                            {
                                hasShield = true;
                                item1.remShields--;
                            }
                            else
                            {
                                hasShield = false;
                            }

                            deletingButton = item2;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("specialShipList");
                            string xxx = recieve.Substring(0, recieve.Length - 1);
                            shotButtonName = xxx;
                        }
                    }
                }
            }

            _extraRoundPublisher.NotifySubscribers();

            if (isShot)
            {
                if (shottedShip == "SpecialSubmarine" || shottedShip == "SpecialCruiser" || shottedShip == "SpecialDestroyer" || shottedShip == "Battleship")
                {
                    if (hasShield)
                    {
                        myBoardButtons.FirstOrDefault(x => x.Name == shotButtonName).BackgroundImage = Image.FromFile(Application.StartupPath + @"\Images\shield.png");
                        AttackToEnemy("hitShielded:" + shotButtonName);
                        return;
                    }
                    else
                    {
                        _extraRoundPublisher.Unsubscribe(_extraRoundSubscriberMap.GetExtraRoundSubscriber(extraSubscriberToGet));
                        myBoardButtonToUndo = myBoardButtons.FirstOrDefault(x => x.Name == shotButtonName);
                        myBoardButtonToUndo.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Images\x.png");

                        AttackToEnemy("hit:" + shotButtonName);

                        deletingButton.buttonNames.Remove(shotButtonName);

                                                //  If the hit ship is "Battleship", all buttons of "Battleship" are checked. If they're all hit, it's game over.
                        if (shottedShip == "Battleship")
                        {
                            foreach (var item in Form2_PreparatoryScreen.specialShipList.FirstOrDefault(x => x.shipName == "Battleship").shipPerButton)
                            {
                                if (item.buttonNames.Count > 0)
                                {
                                    return;
                                }
                                AttackToEnemy("youwin");
                                GameContext gameContext = GameContext.Instance;
                                gameContext.SetVictoryMessage("You lost.");
                                gameContext.TransitionTo(new GameOverState());
                                //DialogResult res = MessageBox.Show("You lost.", "Client - Game Result", MessageBoxButtons.OK);
                                //{
                                //    if (res == DialogResult.Yes)
                                //    {
                                //        Environment.Exit(1);
                                //    }
                                //    else
                                //        Environment.Exit(1);
                                //}
                                return;
                            }
                        }

                        return;
                    }
                }
                else
                {
                    _extraRoundPublisher.Unsubscribe(_extraRoundSubscriberMap.GetExtraRoundSubscriber(extraSubscriberToGet));

                    myBoardButtonToUndo = myBoardButtons.FirstOrDefault(x => x.Name == shotButtonName);
                    myBoardButtonToUndo.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Images\x.png");

                    AttackToEnemy("hit:" + shotButtonName);

                    deletingButton.buttonNames.Remove(shotButtonName);

                    //  If the hit ship is "Battleship", all buttons of "Battleship" are checked. If they're all hit, it's game over.
                    if (shottedShip == "Battleship")
                    {
                        foreach (var item in Form2_PreparatoryScreen.shipList.FirstOrDefault(x => x.shipName == "Battleship").shipPerButton)
                        {
                            if (item.buttonNames.Count > 0)
                            {
                                return;
                            }
                            AttackToEnemy("youwin");
                            GameContext gameContext = GameContext.Instance;
                            gameContext.SetVictoryMessage("You lost.");
                            gameContext.TransitionTo(new GameOverState());
                            //DialogResult res = MessageBox.Show("You lost.", "Client - Game Result", MessageBoxButtons.OK);
                            //{
                            //    if (res == DialogResult.Yes)
                            //    {
                            //        Environment.Exit(1);
                            //    }
                            //    else
                            //        Environment.Exit(1);
                            //}
                            return;
                        }
                    }
                }
            }
            else
            {
                if (myBoardButtons == null)
                {
                    return;
                }

                _extraRoundPublisher.Unsubscribe(_extraRoundSubscriberMap.GetExtraRoundSubscriber(extraSubscriberToGet));
                myBoardButtonToUndo = myBoardButtons.FirstOrDefault(x => x.Name == shotButtonName);
                myBoardButtonToUndo.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Images\o.png");
                AttackToEnemy("miss:" + shotButtonName);
            }
        }

        private ICommand CreateCommand(string receive, List<Button> gameBoardButtons, RichTextBox richTextBox, SoundPlayerBridge hitSoundPlayer, SoundPlayerBridge missSoundPlayer)
        {
            if (receive.Contains("miss:"))
            {
                return new MissCommand(gameBoardButtons, richTextBox, missSoundPlayer, receive);
            }
            else if (receive.Contains("hit:"))
            {
                return new HitCommand(gameBoardButtons, richTextBox, hitSoundPlayer, receive);
            }

            // If no specific command matches, return a default or null command
            return new DefaultCommand();
        }

        public void AttackToEnemy(string buttonName)
        {
            if (buttonName == null)
            {
                return;
            }

            else if (buttonName.Length == 3)
            {
                TextToSend = buttonName.Substring(0, 2);
            }

            TextToSend = buttonName;
            backgroundWorker2.RunWorkerAsync();

            if (isIceberg)
            {
                isIceberg = false;
                return;
            }

            SwitchGameButtonsEnabled();
        }

        private void SwitchGameButtonsEnabled()
        {
            if (weHaveReceivedExtraRound && !isIceberg)
            {
                foreach (var item in gameBoardButtons)
                {
                    if (!clickedButtons.Contains(item.Name))
                    {
                        item.Enabled = true;
                    }
                }

                labelAttackTurn.Text = "ATTACK";
                areEnabledButtons = true;
                weHaveReceivedExtraRound = false;

                return;
            }

            if (enemyReceivedExtraRound && !isIceberg)
            {
                foreach (var item in gameBoardButtons)
                {
                    item.Enabled = false;
                }
                labelAttackTurn.Text = "WAIT...";
                areEnabledButtons = false;
                enemyReceivedExtraRound = false;

                richTextBox1.AppendText("Enemy extra round\n");
                return;
            }

            if (areEnabledButtons)
            {
                foreach (var item in gameBoardButtons)
                {
                    item.Enabled = false;
                }
                labelAttackTurn.Text = "WAIT...";
                areEnabledButtons = false;
            }
            else
            {
                foreach (var item in gameBoardButtons)
                {
                    if (!clickedButtons.Contains(item.Name))
                    {
                        if (item.Name == "itemButton" && (playerItem.remItems <= 0 || remainingJams > 0))
                        {
                            item.Enabled = false;
                        }
                        else if (item.Name == "itemButton2" && (playerItem2.remItems <= 0 || remainingJams > 0))
                        {
                            item.Enabled = false;
                        }
                        else if (item.Name == "itemButton3" && (playerItem3.remItems <= 0 || remainingJams > 0))
                        {
                            item.Enabled = false;
                            remainingJams--;
                        }
                        else
                        {
                            item.Enabled = true;
                        }
                    }
                }

                labelAttackTurn.Text = hasRadarUse ? "RANDOM RADAR" : "ATTACK";
                if (labelAttackTurn.Text == "RANDOM RADAR")
                {
                    itemButton.Enabled = false;
                    itemButton2.Enabled = false;
                    itemButton3.Enabled = false;
                }
                areEnabledButtons = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Client.GetInstance.IsConnected)
            {
                MessageBox.Show("Connection failed.");
                Form2_PreparatoryScreen frm2 = _formCreator.BuildDarkForm();
                frm2.Show();
                this.Close();
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!myExit)
            {
                AttackToEnemy("exitt");
            }
            Form2_PreparatoryScreen frm2 = _formCreator.BuildDarkForm();
            frm2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISoundImplementation backgroudSoundImplementation = new BackgroundMusic();
            SoundPlayerBridge backgroundSoundPlayer = new BackgroundMusicPlayer(backgroudSoundImplementation);
            backgroundSoundPlayer.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ISoundImplementation backgroudSoundImplementation = new BackgroundMusic();
            SoundPlayerBridge backgroundSoundPlayer = new BackgroundMusicPlayer(backgroudSoundImplementation);
            backgroundSoundPlayer.Stop();
        }

        private void itemButton_Click(object sender, EventArgs e)
        {
            AttackToEnemy("[EnemyItem]");
            playerItem.remItems--;
            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UndoLastMove(myBoardButtonToUndo);
            button3.Enabled = false;
        }

        private void itemButton2_Click(object sender, EventArgs e)
        {
            AttackToEnemy("[EnemyBsItem]");

            playerItem2.remItems--;
            return;
        }

        private void itemButton3_Click(object sender, EventArgs e)
        {
            AttackToEnemy("[EnemyJamItem]");

            playerItem3.remItems--;
            return;
        }

        public static int getRemainingJams()
        {
            return remainingJams;
        }

        public static void setRemainingJams(int jams)
        {
            remainingJams = jams;
        }
    }
}
