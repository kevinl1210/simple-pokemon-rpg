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
using System.Windows.Media.Animation;

namespace test
{
    /// <summary>
    /// Interaction logic for TypingGameScreen.xaml
    /// </summary>
    public partial class TypingGameScreen : Window
    {
        private List<string> PokemonText;
        private string PokemonName;
        private TypingGame game;
        private int sentencesToType = 5;
        private Random rnd;
        private int animationIndex;
        private TypingGameScreen()
        {
            this.game = TypingGame.Instance;
            InitializeComponent();
            this.Show();
            MainWindow.Instance.Hide();
            generatePokemonText();
        }

        public static void OpenCatchScreen(string PokemonName)
        {
            TypingGameScreen game = new TypingGameScreen();
            game.PokemonName = PokemonName;
            game.pokemon.Source = new BitmapImage(new Uri(@"pack://application:,,,/media/pokemon/" + PokemonName.ToLower() + ".png"));
        }

        private void on_Close(object sender, EventArgs e)
        {
            MainWindow.Instance.Show();
            MainWindow.Instance.Activate();
        }

        private void on_Escaped(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have escaped!!!!");
            this.Close();
        }

        private void on_Caught(object sender, RoutedEventArgs e)
        {
            animationIndex = 0;
            rnd = new Random();
            MessageBox.Show("Win the typing game to catch " + PokemonName + ", good luck!!!!");
            this.escapeButton.Visibility = Visibility.Hidden;
            this.catchButton.Visibility = Visibility.Hidden;
            this.catchText.Visibility = Visibility.Hidden;
            this.inputText.Visibility = Visibility.Visible;
            this.typeText.Visibility = Visibility.Visible;
            this.inputText.Visibility = Visibility.Visible;
            this.progress.Visibility = Visibility.Visible;
            this.time.Visibility = Visibility.Visible;
            this.back.Visibility = Visibility.Visible;

            this.inputText.Focus();
            this.typeText.Text = PokemonText[rnd.Next(0, PokemonText.Count)];

            game.Timer.Tick += (sender0, EventArgs) => { remain_Tick(sender, EventArgs); };
            game.GameTimer.Tick += (sender0, EventArgs) => { game_Tick(sender, EventArgs); };

            game.startGame();
        }

        private void generatePokemonText()
        {
            PokemonText = new List<string>();
            string line;

            using (StreamReader sr = new StreamReader("pokemon.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    PokemonText.Add(line);
                }
            }
        }

        private void remain_Tick(object sender, EventArgs e)
        {
            game.TimeRemaining--;
            if (game.TimeRemaining < 0)
            {
                this.pokemon.Source = null;
                MessageBox.Show(PokemonName + " has escaped, try catch another again!!!!");
                game.destroyGame();
                this.Close();
            }
            this.time.Text = "Time remaining: " + game.TimeRemaining + "s";
        }

        private void game_Tick(object sender, EventArgs e)
        {
            int progressBar = 0;
            if (this.inputText.Text == this.typeText.Text)  // correctly typed
            {
                this.inputText.Text = "";
                game.TimeRemaining = 7;
                game.Progress++;
                progressBar = (int)((double)game.Progress / sentencesToType * 100);
                this.progress.Text = progressBar + "%";
                this.typeText.Text = PokemonText[rnd.Next(0, PokemonText.Count)];
            }
            // win
            if (progressBar >= 100)
            {
                game.destroyGame();
                MessageBox.Show("You caught " + PokemonName);
                // Player add caught pokemon to his list
                Pokemon x = new Pokemon(PokemonName);
                Player.Instance.PETS.Add(x);
                Player.Instance.SwitchPoke(x);
                //this.pokemon.Source = new BitmapImage(new Uri(@"pack://application:,,,/media/pokeball1.png"));
                // animation of pokeball
                DispatcherTimer animation = new DispatcherTimer();
                animation.Interval = new TimeSpan(0,0,0,0,500);
                animation.Tick += Animation;
                animation.Start();
                this.back.Visibility = Visibility.Hidden;
                //this.Close();
            }
        }

        private void Animation(object sender, EventArgs e)
        {
            int[] arr = new int[9]{1,3,1,2,1,3,1,2,1};
            if (animationIndex > arr.Length - 1)
            {
                MessageBox.Show("Caught!!!!");
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Stop();
                timer.Tick -= Animation;
                this.Close();
                return;
            }
            this.pokemon.Height = 50;
            this.pokemon.Width = 50;
            this.pokemon.Source = new BitmapImage(new Uri(@"pack://application:,,,/media/pokeball" + arr[animationIndex] + ".png"));
            animationIndex++;
        }

        private void on_Back(object sender, RoutedEventArgs e)
        {
            game.destroyGame();
            MessageBox.Show("You have escaped!!!!");
            this.Close();
        }

        private class TypingGame
        {
            private int _progress = 0;
            public int Progress
            {
                get { return _progress; }
                set { _progress = value; }
            }
            private int timeRemaining = 7;
            public int TimeRemaining
            {
                get { return timeRemaining; }
                set { timeRemaining = value; }
            }
            private static TypingGame instance;
            public static TypingGame Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new TypingGame();
                    }
                    return instance;
                }
            }
            private DispatcherTimer _timer;
            public DispatcherTimer Timer
            {
                get 
                {
                    _timer = new DispatcherTimer();
                    _timer.Interval = new TimeSpan(0, 0, 1);
                    return _timer; 
                }
            }
            private DispatcherTimer _gametimer;
            public DispatcherTimer GameTimer
            {
                get
                {
                    _gametimer = new DispatcherTimer();
                    _gametimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                    return _gametimer;
                }
            }
            private TypingGame()
            {
                //
            }
            public void startGame()
            {
                _timer.Start();
                _gametimer.Start();
            }
            public void destroyGame()
            {
                _timer.Stop();
                _gametimer.Stop();
                instance = null;
            }
        }
    }
}
