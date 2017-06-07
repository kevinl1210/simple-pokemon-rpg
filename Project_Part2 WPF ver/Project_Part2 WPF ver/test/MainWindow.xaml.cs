using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;

namespace test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player x = Player.Instance;
        PokeMap map = PokeMap.Instance;
        private static MainWindow instance;
        public static MainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }
        static bool battled = false;
        PokemonGenerator pkmGenerator = PokemonGenerator.Instance;
        private static int gymFlag = 0;
        public static DispatcherTimer gymTimer;
        static int characterMoveImageIndex = 1;
        private MainWindow()
        {
            InitializeComponent();
            gymTimer = new DispatcherTimer();
            gymTimer.Interval = new TimeSpan(0, 0, 1);
            gymTimer.Tick += Tick;
            Canvas.SetLeft(player, Convert.ToDouble(x.x_pos));
            Canvas.SetTop(player, Convert.ToDouble(x.y_pos));
            Canvas.SetLeft(gym, Convert.ToDouble(map.GymPositionRaw(0)[0]));
            Canvas.SetTop(gym, Convert.ToDouble(map.GymPositionRaw(0)[1]));
            //Canvas.SetLeft(gymArea, Convert.ToDouble(map.GymPosition(0)[0]-46));
            //Canvas.SetTop(gymArea, Convert.ToDouble(map.GymPosition(0)[1]-42));
            Canvas.SetLeft(gymArea, 1261);
            Canvas.SetTop(gymArea, 220);
        }

        private void Tick(object sender, EventArgs e)
        {
            if (gymFlag > 0 && gymFlag % 40 == 0)
            {
                //MessageBox.Show(string.Format("gym refreshed! come battle again!, flag is {0}",gymFlag));
                battled = false;
                gymFlag = 0;
            }
            gymFlag++;
        }

        private void characterMove(object sender, KeyEventArgs e)
        {
            pkmGenerator.stepsToEncounter++;
            if (pkmGenerator.wildPokemon != "")
            {
                TypingGameScreen.OpenCatchScreen(pkmGenerator.wildPokemon);
            }

            Point forestCoordinate = forest.TranslatePoint(new Point(0, 0), container);
            Point playerCoordinate = player.TranslatePoint(new Point(0, 0), forest);
            Point bagCoordinate = bagButton.TranslatePoint(new Point(0, 0), forest);
            //Point moneyCoordinate = moneyLabel.TranslatePoint(new Point(0, 0), forest);
            //Point expCoordinate = expLabel.TranslatePoint(new Point(0, 0), forest);
            x.PositionRecording(playerCoordinate.X, playerCoordinate.Y);
            if (e.Key == Key.Left)
            {
                player.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/media/left" + (characterMoveImageIndex / 2 + 1) + ".png")));
                characterMoveImageIndex++;
                if (characterMoveImageIndex == 8)
                {
                    characterMoveImageIndex = 1;
                }
                if (playerCoordinate.X >= 1159)
                {
                    Canvas.SetLeft(player, playerCoordinate.X - 3);
                }
                else if (forestCoordinate.X >= 747)
                {
                    if (playerCoordinate.X >= -1)
                    {
                        Canvas.SetLeft(player, playerCoordinate.X - 3);
                    }
                }
                else
                {
                    Canvas.SetLeft(player, playerCoordinate.X - 3);
                    Canvas.SetLeft(bagButton, bagCoordinate.X - 3);
                    //Canvas.SetLeft(moneyLabel, moneyCoordinate.X - 3);
                    //Canvas.SetLeft(expLabel, expCoordinate.X - 3);
                    Canvas.SetLeft(forest, forestCoordinate.X + 3);
                }
            }
            if (e.Key == Key.Right)
            {
                player.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/media/right" + (characterMoveImageIndex / 2 + 1) + ".png")));
                characterMoveImageIndex++;
                if (characterMoveImageIndex == 8)
                {
                    characterMoveImageIndex = 1;
                }
                if (playerCoordinate.X <= 260)
                {
                    Canvas.SetLeft(player, playerCoordinate.X + 3);
                    x.x_pos += 3;
                }
                else if (forestCoordinate.X <= -123)
                {
                    if (playerCoordinate.X <= 1432)
                    {
                        Canvas.SetLeft(player, playerCoordinate.X + 3);
                        x.x_pos += 3;
                    }
                }
                else
                {
                    Canvas.SetLeft(player, playerCoordinate.X + 3);
                    Canvas.SetLeft(bagButton, bagCoordinate.X + 3);
                    //Canvas.SetLeft(moneyLabel, moneyCoordinate.X + 3);
                    //Canvas.SetLeft(expLabel, expCoordinate.X + 3);
                    Canvas.SetLeft(forest, forestCoordinate.X - 3);
                }
            }
            if (e.Key == Key.Up)
            {
                player.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/media/up" + (characterMoveImageIndex / 2 + 1) + ".png")));
                characterMoveImageIndex++;
                if (characterMoveImageIndex == 8)
                {
                    characterMoveImageIndex = 1;
                }
                if (playerCoordinate.Y >= 799)
                {
                    Canvas.SetTop(player, playerCoordinate.Y - 3);
                }
                else if (forestCoordinate.Y >= 584)
                {
                    if (playerCoordinate.Y >= 18)
                    {
                        Canvas.SetTop(player, playerCoordinate.Y - 3);
                    }
                }
                else
                {
                    Canvas.SetTop(player, playerCoordinate.Y - 3);
                    Canvas.SetTop(bagButton, bagCoordinate.Y - 3);
                    //Canvas.SetTop(moneyLabel, moneyCoordinate.Y - 3);
                    //Canvas.SetTop(expLabel, expCoordinate.Y - 3);
                    Canvas.SetTop(forest, forestCoordinate.Y + 3);
                }
            }
            if (e.Key == Key.Down)
            {
                player.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/media/down" + (characterMoveImageIndex / 2 + 1) + ".png")));
                characterMoveImageIndex++;
                if (characterMoveImageIndex == 8)
                {
                    characterMoveImageIndex = 1;
                }
                if (playerCoordinate.Y <= 211)
                {
                    Canvas.SetTop(player, playerCoordinate.Y + 3);
                }
                else if (forestCoordinate.Y <= -8)
                {
                    if (playerCoordinate.Y <= 988)
                    {
                        Canvas.SetTop(player, playerCoordinate.Y + 3);
                    }
                }
                else
                {
                    Canvas.SetTop(player, playerCoordinate.Y + 3);
                    Canvas.SetTop(bagButton, bagCoordinate.Y + 3);
                    //Canvas.SetTop(moneyLabel, moneyCoordinate.Y + 3);
                    //Canvas.SetTop(expLabel, expCoordinate.Y + 3);
                    Canvas.SetTop(forest, forestCoordinate.Y - 3);
                }
            }

            Rect playerRec = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            Rect gymRec = new Rect(Canvas.GetLeft(gymArea), Canvas.GetTop(gymArea), gymArea.Width, gymArea.Height);
            if (playerRec.IntersectsWith(gymRec))
            {
                if (battled == false)
                {
                    on_Gym_encounter();
                }
                battled = true;
            }
        }

        private void bagButton_Click(object sender, RoutedEventArgs e)
        {
            ItemScreen.OpenItemScreen();
        }

        public void on_Gym_encounter()
        {
            MessageBox.Show("you encounter a pokemon gym!!");
            //Gym_old.Battle();
            GymBattleVS.OpenBattleScreen();
            this.Hide();
        }

        private class PokemonGenerator
        {
            private Random rnd;
            public string wildPokemon;
            private static List<string> normal;
            private static List<string> evolve;
            private static List<string> evolve2;
            private static List<string> RandomPokemonList;
            private static int _stepsToEncounter;
            private static int currentStep;
            public int stepsToEncounter
            {
                get
                {
                    return _stepsToEncounter;
                }
                set
                {
                    _stepsToEncounter = value;
                    wildPokemon = "";
                    if (_stepsToEncounter == currentStep)
                    {
                        if (RandomPokemonList.Count > 0)
                        {
                            wildPokemon = RandomPokemonList[0];
                            RandomPokemonList.RemoveAt(0);
                            MessageBox.Show(string.Format("Wild {0} appears!!!!!", wildPokemon));
                            _stepsToEncounter = 0;
                        }
                        else MessageBox.Show("You have encountered all 8 pokemons!!!!");
                    }
                }
            }
            private static PokemonGenerator instance;
            public static PokemonGenerator Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new PokemonGenerator();
                    }
                    return instance;
                }
            }

            private PokemonGenerator()
            {
                RandomPokemonList = new List<string>();
                normal = new List<string>();
                evolve = new List<string>();
                evolve2 = new List<string>();
                string line;
                string[] pokemons;
                try
                {
                    using (StreamReader sr = new StreamReader("evolve_list.txt"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            pokemons = line.Split(' ');
                            normal.Add(pokemons[0]);
                            if (pokemons.Length == 2)
                            {
                                evolve.Add(pokemons[1]);
                            }
                            else
                            {
                                evolve.Add(pokemons[1]);
                                evolve2.Add(pokemons[2]);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Environment.Exit(0);
                }

                rnd = new Random();

                int pokemonStateCount = 0;
                for (int i = 0; i < 8; i++)
                {
                    int rarity = rnd.Next(1, 101);
                    if (rarity < 80)
                    {
                        pokemonStateCount = normal.Count;
                        RandomPokemonList.Add(normal[rnd.Next(0, pokemonStateCount)]);
                    }
                    else if (rarity < 95)
                    {
                        pokemonStateCount = evolve.Count;
                        RandomPokemonList.Add(evolve[rnd.Next(0, pokemonStateCount)]);
                    }
                    else
                    {
                        pokemonStateCount = evolve2.Count;
                        RandomPokemonList.Add(evolve2[rnd.Next(0, pokemonStateCount)]);
                    }
                }
                currentStep = rnd.Next(80, 200);
            }
        }
    }
}
