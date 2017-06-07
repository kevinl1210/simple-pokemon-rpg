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
using System.Windows.Shapes;
using System.Reflection;
using System.Threading;

namespace test
{
    /// <summary>
    /// Interaction logic for GymBattleVS.xaml
    /// </summary>
    



    public partial class GymBattleVS : Window
    {
        private int pointer_pos;
        Gym play = PokeMap.Instance.GetTypedGym(Gym.na_type.water);
        Player x = Player.Instance;
        Rectangle graphic;

        private static GymBattleVS instance;
        public static GymBattleVS Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GymBattleVS();
                }
                return instance;
            }
        }
        private GymBattleVS()
        {
            //test use
            //this.navigation = navigation;
            //test use

            InitializeComponent();
            Player x = Player.Instance;
            pointer_pos = 0;
            InitialWrap();
            Pointer.Margin = new Thickness(400, 350, 0, 0);
            //Display(play.dialogue(Gym.diag.startend, true));
            PokeBallDisplay();
            choice_ava.Visibility = Visibility.Hidden;
            choice_bg.Visibility = Visibility.Hidden;
            //Console.WriteLine(x.POKENOW);
            EnPokeDisplay(play.POKENOW);
            MePokeDisplay(x.POKENOW);
        }
        private void InitialWrap()
        {
            for (int i = 0; i < 48; i++)
            {
                graphic = new Rectangle();
                graphic.Height = 8;
                graphic.Width = 12;
                WordDisplay.Children.Add(graphic);
            }
            for (int i = 0; i < 48; i++)
            {
                graphic = new Rectangle();
                graphic.Height = 22;
                graphic.Width = 12;
                WordDisplay.Children.Add(graphic);
            }
            for (int i = 0; i < 48; i++)
            {
                graphic = new Rectangle();
                graphic.Height = 8;
                graphic.Width = 12;
                WordDisplay.Children.Add(graphic);
            }
            for (int i = 0; i < 48; i++)
            {
                graphic = new Rectangle();
                graphic.Height = 22;
                graphic.Width = 12;
                WordDisplay.Children.Add(graphic);
            }
            for (int i = 0; i < 48; i++)
            {
                graphic = new Rectangle();
                graphic.Height = 8;
                graphic.Width = 12;
                WordDisplay.Children.Add(graphic);
            }
        }
        private async Task Display(string [] text)
        {
            int time_wait = 20;
            char[] txtchar;
            int[] pixel;
            text_clear();

            //WebControl.Enabled = false;
            for (int i = 0; i < text.Length; i++)
            {
                txtchar = text[i].ToCharArray();
                if (i % 2 == 0)
                {                    
                    text_clear();
                    for (int j = 0; j < txtchar.Length; j++)
                    {
                        graphic = WordDisplay.Children[j + 48] as Rectangle;
                        pixel = Pixel_Position(txtchar[j]);
                        graphic.Fill = ImagePainting("33690.png", pixel[0], pixel[1], 6, 11);
                        await Task.Delay(time_wait);
                    }                                        
                }
                else
                {
                    for (int j = 0; j < txtchar.Length; j++)
                    {
                        graphic = WordDisplay.Children[j + 144] as Rectangle;
                        pixel = Pixel_Position(txtchar[j]);
                        graphic.Fill = ImagePainting("33690.png", pixel[0], pixel[1], 6, 11); ;
                        await Task.Delay(time_wait);
                    }
                    await Task.Delay(2000);
                }
                
            }
        }
        private void text_clear()
        {
            ImageBrush ib;
            int[] pixel;

            pixel = Pixel_Position(' ');
            ib = ImagePainting("33690.png", pixel[0], pixel[1], 6, 11);
            
            for (int i = 0; i < 48; i++)
            {
                graphic = WordDisplay.Children[i + 48] as Rectangle;
                graphic.Fill = ib;
                graphic = WordDisplay.Children[i + 144] as Rectangle;
                graphic.Fill = ib;
            }
        }
        private static int[] Pixel_Position(char x)
        {
            int[] position = new int[2];
            if (x <= 'Z' && x >= 'A')
            {
                //position[2]=1;          //1 indicates blue
                position[1] = 90 + ((x - 'A') / 6 - (x - 'A') / 18 + (x - 'A') / 19 - (x - 'A') / 24) * 16;
                //too troublesome, so direct calculating is abandoned
                if (x <= 'R')
                {
                    position[0] = 18 + ((x - 'A') % 6) / 3 * 56 + ((x - 'A') % 3) * 12;
                }
                else if (x <= 'Y' && x >= 'T')
                {
                    position[0] = 18 + ((x - 'T') % 6) / 3 * 56 + ((x - 'T') % 3) * 12;
                }
                else
                {
                    position[0] = 110;
                }
            }
            else if (x <= 'z' && x >= 'a')
            {
                //position[2]=2;          //2 indicates red
                position[1] = 160 + ((x - 'a') / 6 - (x - 'a') / 18 + (x - 'a') / 19 - (x - 'a') / 24) * 16;
                if (x <= 'r')
                {
                    position[0] = 18 + ((x - 'a') % 6) / 3 * 56 + ((x - 'a') % 3) * 12;
                }
                else if (x <= 'y' && x >= 't')
                {
                    position[0] = 18 + ((x - 't') % 6) / 3 * 56 + ((x - 't') % 3) * 12;
                }
                else
                {
                    position[0] = 110;
                }
            }
            else if (x <= '9' && x >= '0')
            {
                //position[2]=3;          //3 indicates green
                position[1] = 229 + (x - '0') / 5 * 16;
                position[0] = 18 + ((x - '0') % 5) * 22;
            }
            else switch (x)
                {
                    case '?': position[0] = 40; position[1] = 260; break;
                    case '!': position[0] = 18; position[1] = 260; break;
                    case '/': position[0] = 106; position[1] = 260; break;
                    case '.': position[0] = 142; position[1] = 90; break;
                    case ',': position[0] = 142; position[1] = 106; break;
                    case ' ': position[0] = 142; position[1] = 122; break;
                    case '\'': position[0] = 105; position[1] = 275; break;
                    case '-': position[0] = 127; position[1] = 260; break;
                    case '_': position[0] = 142; position[1] = 122; break;
                    default: position[0] = 125; position[1] = 250; break;
                }
            return position;
        }
        private void HPDisplay(Pokemon dmg, WrapPanel target)
        {
            int hpeachbar = dmg.MAXHP / 8;
            int firstbar = dmg.MAXHP - 7 * hpeachbar;
            int hposition = 8;
            int hp = dmg.HP;
            if (hp == 0)
            {
                hposition = 0;
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    if (hp >= (firstbar + i * hpeachbar) && hp < (firstbar + (i + 1) * hpeachbar))
                    {
                        hposition = i + 1;
                        break;
                    }
                }
            }
            //int hposition = 0;
            switch (hposition)
            {
                case 0:
                    HPDisplayAssist(117, 21, 0, 8, target);
                    break;
                case 1:
                    HPDisplayAssist(117, 17, 0, 1, target);
                    HPDisplayAssist(117, 21, 1, 8, target);
                    break;
                case 2:
                case 3:
                case 4:
                    HPDisplayAssist(117, 13, 0, hposition, target);
                    HPDisplayAssist(117, 21, hposition, 8, target);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    HPDisplayAssist(117, 9, 0, hposition, target);
                    HPDisplayAssist(117, 21, hposition, 8, target);
                    break;
            }
        }
        private void HPDisplayAssist(int xstart, int ystart, int startbar, int endbar, WrapPanel target)
        {
            for (int i = startbar; i < endbar; i++)
            {                
                graphic = target.Children[i] as Rectangle;
                graphic.Fill = ImagePainting("33710.png", xstart, ystart, 6, 3); ;
            }
        }
        //Display Use
        private ImageBrush ImagePainting(string source)
        {
            ImageBrush ib;
            ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/media/" + source));
            return ib;
        }
        private ImageBrush ImagePainting(string source, int x, int y, int width, int height)
        {
            ImageBrush ib;
            ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/media/"+source));
            ib.ViewboxUnits = BrushMappingMode.Absolute;
            ib.Viewbox = new Rect(x, y, width, height);
            return ib;
        }
        private void PokeBallDisplay()
        {            
            for (int i = 0; i < 6 - play.TOTAL; i++)
            {
                graphic = PokeBallBar.Children[i] as Rectangle;
                graphic.Fill = ImagePainting("33710.png", 123, 65, 9, 7); ;
            }
            for (int i = 6 - play.TOTAL; i < 6 - play.TOTAL + play.KO; i++)
            {
                graphic = PokeBallBar.Children[i] as Rectangle;
                graphic.Fill = ImagePainting("33710.png", 143, 65, 9, 7);
            }
            for (int i = 6 - play.TOTAL + play.KO; i < 6; i++)
            {
                graphic = PokeBallBar.Children[i] as Rectangle;
                graphic.Fill = graphic.Fill = ImagePainting("33710.png", 133, 65, 9, 7); ;
            }
        }
        private void MePokeDisplay(Pokemon Me)
        {
            int hp = Me.HP;
            int maxhp = Me.MAXHP;
            HPDisplay(Me, PokeHP);
            
            //Display Health in XX/XX style
            #region
            string s;
            if (hp < 10)
            {
                s = "00" + Convert.ToString(hp);
            }
            else if (hp < 100)
            {
                s = "0" + Convert.ToString(hp);
            }
            else
            {
                s = Convert.ToString(hp);
            }
            s = s + "/";
            if (maxhp < 10)
            {
                s = s + "00" + Convert.ToString(maxhp);
            }
            else if (hp < 100)
            {
                s = s + "0" + Convert.ToString(maxhp);
            }
            else
            {
                s = s + Convert.ToString(maxhp);
            }
            #endregion

            
            char[] txtchar;
            int[] pixel;
            txtchar = s.ToCharArray();
            for (int i = 0; i < txtchar.Length; i++)
            {
                graphic = PokeHP.Children[i + 9] as Rectangle;
                pixel = Pixel_Position(txtchar[i]);
                graphic.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
            }

            string temp = Me.NAME;
            temp = char.ToLower(temp[0]).ToString() + temp.Substring(1);
            MyPoke.Fill = ImagePainting("pokeBack/"+temp+".png");

            PokeDisplayReset(MyPoke_info);
            //Display Pokemon Name and LV
            #region 
            Rectangle txtBox;
            txtchar = temp.ToCharArray();
            for (int i = 0; i < txtchar.Length; i++)
            {
                txtBox = MyPoke_info.Children[i] as Rectangle;
                pixel = Pixel_Position(txtchar[i]);
                txtBox.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
            }
            if (Me.LV < 10)
            {
                txtBox = MyPoke_info.Children[16] as Rectangle;
                pixel = Pixel_Position(' ');
                txtBox.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
                
                txtBox = MyPoke_info.Children[17] as Rectangle;
                pixel = Pixel_Position(Convert.ToChar(Me.LV + 48));
                txtBox.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
            }
            else
            {
                txtBox = EnPoke_info.Children[16] as Rectangle;
                pixel = Pixel_Position(Convert.ToChar(Me.LV / 10 + 48));
                txtBox.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);

                txtBox = EnPoke_info.Children[17] as Rectangle;
                pixel = Pixel_Position(Convert.ToChar(Me.LV % 10 + 48));
                txtBox.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
            }
            #endregion
        }
        private void EnPokeDisplay(Pokemon En)
        {
            int[] pixel;

            HPDisplay(En, EnHPBar);
            string temp = En.NAME;
            temp = char.ToLower(temp[0]).ToString() + temp.Substring(1);
            EnPoke.Fill = ImagePainting("pokeBack/"+temp + ".png");

            PokeDisplayReset(EnPoke_info);
            //Display Enemy Name, Lv
            #region 
            char[] txtchar;
            txtchar = temp.ToCharArray();
            for (int i = 0; i < txtchar.Length; i++)
            {
                graphic = EnPoke_info.Children[i] as Rectangle;
                pixel = Pixel_Position(txtchar[i]);
                graphic.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
            }
            if (En.LV < 10)
            {
                graphic = EnPoke_info.Children[16] as Rectangle;
                pixel = Pixel_Position(' ');
                graphic.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);

                graphic = EnPoke_info.Children[17] as Rectangle;
                pixel = Pixel_Position(Convert.ToChar(En.LV+48));
                graphic.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
            }
            else
            {
                graphic = EnPoke_info.Children[16] as Rectangle;
                pixel = Pixel_Position(Convert.ToChar(En.LV/10+48));
                graphic.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);

                graphic = EnPoke_info.Children[17] as Rectangle;
                pixel = Pixel_Position(Convert.ToChar(En.LV%10 + 48));
                graphic.Fill = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);
            }
            #endregion       
        }
        private void PokeDisplayReset(WrapPanel target)
        {
            ImageBrush ib;
            int[] pixel;

            pixel = Pixel_Position(' ');
            ib = ImagePainting("33710.png", pixel[0], pixel[1], 6, 11);

            for (int i = 0; i < target.Children.Count; i++)
            {
                graphic = target.Children[i] as Rectangle;
                graphic.Fill = ib;
            }
        }
        private async Task BattlePoke()
        {
            string fight = x.POKENOW.SKILLS[pointer_pos - 5]; 
            int dmg = x.POKENOW.Attack(fight, play.POKENOW);
            pointer_pos = 20;//Disable spacebar input
            await Display(play.dialogue(dmg, fight));
        }                                          
        private async Task HitSpEffect()
        {            
            int time_delay = 50;

            MyPoke.Margin = new Thickness(MyPoke.Margin.Left + 10, MyPoke.Margin.Top - 5, 0, 0);
            SpEffect.Fill = ImagePainting("effect.png", 0, 0, 161, 173);
            await Task.Delay(time_delay);

            MyPoke.Margin = new Thickness(MyPoke.Margin.Left + 10, MyPoke.Margin.Top - 5, 0, 0);
            SpEffect.Fill = ImagePainting("effect.png", 164, 0, 161, 173);
            await Task.Delay(time_delay);

            MyPoke.Margin = new Thickness(MyPoke.Margin.Left - 10, MyPoke.Margin.Top + 5, 0, 0);
            SpEffect.Fill = ImagePainting("effect.png", 327, 0, 161, 173);
            await Task.Delay(time_delay);

            MyPoke.Margin = new Thickness(MyPoke.Margin.Left - 10, MyPoke.Margin.Top + 5, 0, 0);
            SpEffect.Fill = null;
            await Task.Delay(time_delay);
        }

        private void pointerMove(object sender, KeyEventArgs e)
        {
            List<Pokemon> poke = x.PETS;
            #region                     //pointer movement
            switch (pointer_pos)
            {
                case 0:
                    if (e.Key == Key.Right)
                    {
                        pointer_pos = 1;
                        Pointer.Margin = new Thickness(510, 350, 0, 0);
                    }
                    else if (e.Key == Key.Down)
                    {
                        pointer_pos = 2;
                        Pointer.Margin = new Thickness(400, 380, 0, 0);
                    }
                    break;
                case 1:
                    if (e.Key == Key.Left)
                    {
                        pointer_pos = 0;
                        Pointer.Margin = new Thickness(400, 350, 0, 0);
                    }
                    else if (e.Key == Key.Down)
                    {
                        pointer_pos = 3;
                        Pointer.Margin = new Thickness(510, 380, 0, 0);
                    }
                    break;
                case 2:
                    if (e.Key == Key.Right)
                    {
                        pointer_pos = 3;
                        Pointer.Margin = new Thickness(510, 380, 0, 0);
                    }
                    else if (e.Key == Key.Up)
                    {
                        pointer_pos = 0;
                        Pointer.Margin = new Thickness(400, 350, 0, 0);
                    }
                    break;
                case 3:
                    if (e.Key == Key.Left)
                    {
                        pointer_pos = 2;
                        Pointer.Margin = new Thickness(400, 380, 0, 0);
                    }
                    else if (e.Key == Key.Up)
                    {
                        pointer_pos = 1;
                        Pointer.Margin = new Thickness(510, 350, 0, 0);
                    }
                    break;
                case 4: break;
            }
            if (pointer_pos >= 5 && pointer_pos <= 8)
            {
                if (e.Key == Key.Down)
                {
                    if ((pointer_pos + 1) <= (x.POKENOW.SKILLS.Count + 3))
                    {
                        pointer_pos++;
                        Pointer.Margin = new Thickness(Pointer.Margin.Left, Pointer.Margin.Top + 34, 0, 0);
                    }
                }
                if (e.Key == Key.Up)
                {
                    if ((pointer_pos - 1) >= 5)
                    {
                        pointer_pos--;
                        Pointer.Margin = new Thickness(Pointer.Margin.Left, Pointer.Margin.Top - 34, 0, 0);
                    }
                }
            }
            if (pointer_pos >= 9)
            {
                if (e.Key == Key.Down)
                {
                    if ((pointer_pos + 1) <= (poke.Count + 8))
                    {
                        pointer_pos++;
                        Pointer.Margin = new Thickness(Pointer.Margin.Left, Pointer.Margin.Top + 34, 0, 0);
                    }
                }
                if (e.Key == Key.Up)
                {
                    if ((pointer_pos - 1) >= 9)
                    {
                        pointer_pos--;
                        Pointer.Margin = new Thickness(Pointer.Margin.Left, Pointer.Margin.Top - 34, 0, 0);
                    }
                }
            }

            #endregion
            if (e.Key == Key.Space)
            {
                switch (pointer_pos)
                {
                    case 0: ptmove0(); break;
                    case 1: ptmove1(); break;
                    case 2: ptmove2(); break;
                    case 3: ptmove3(); break;
                    case 4: ptmove4(); break;
                    case 5:
                    case 6:
                    case 7:
                    case 8: ptmove5to8(); break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16: ptmove9to16(); break;
                }
            }
            if (e.Key == Key.X && pointer_pos > 3)
            {
                Action_Reset();
            }
            /*test
            if (e.Key == Key.Q)
            {
                HitSpEffect();
            }
            */
            if (e.Key == Key.B)
            {
                PokeMap.Instance.ResetGym(play.NATURE);
                this.Close();
            }
        }

        private void ptmove0()
        {
            List<string> skills = x.POKENOW.SKILLS;
            choice_bg.Visibility = Visibility.Visible;
            choice_ava.Visibility = Visibility.Visible;

            string s;
            char[] txtchar;
            int[] pixel;

            for (int i = 0; i < skills.Count; i++)
            {
                s = skills[i];        
                txtchar = s.ToCharArray();

                graphic = new Rectangle();
                graphic.Height = 16;
                graphic.Width = 8;
                graphic.Margin = new Thickness(32, 0, 0, 0);
                choice_ava.Children.Add(graphic);

                for (int j = 0; j < txtchar.Length; j++)
                {                   
                    graphic = new Rectangle();
                    graphic.Height = 16;
                    graphic.Width = 8;
                    graphic.Margin = new Thickness(0);
                    pixel = Pixel_Position(txtchar[j]);
                    graphic.Fill = ImagePainting("33730.png", pixel[0], pixel[1], 6, 11);
                    choice_ava.Children.Add(graphic);
                }
                graphic = new Rectangle();
                graphic.Height = 16;
                graphic.Width = 320 - 32 - 8 * txtchar.Length;
                graphic.Margin = new Thickness(0, 0, 0, 0);
                choice_ava.Children.Add(graphic);

                graphic = new Rectangle();
                graphic.Height = 2;
                graphic.Width = 320;
                graphic.Margin = new Thickness(0, 0, 0, 0);
                choice_ava.Children.Add(graphic);
            }
            pointer_pos = 5;
            Pointer.Margin = new Thickness(180, 80, 0, 0);
        }
        private void ptmove1()
        {
            Dictionary<string, int> amount = x.ITEMS;
            choice_bg.Visibility = Visibility.Visible;
            choice_ava.Visibility = Visibility.Visible;

            string s = "HPHealer   X" + Convert.ToString(amount["HPHealer"]);
            char[] txtchar;
            int[] pixel;
            txtchar = s.ToCharArray();

            graphic = new Rectangle();
            graphic.Height = 16;
            graphic.Width = 8;
            graphic.Margin = new Thickness(32, 0, 0, 0);
            choice_ava.Children.Add(graphic);

            for (int i = 0; i < txtchar.Length; i++)
            {                
                graphic = new Rectangle();
                graphic.Height = 16;
                graphic.Width = 8;
                graphic.Margin = new Thickness(0);
                pixel = Pixel_Position(txtchar[i]);
                graphic.Fill = ImagePainting("33730.png", pixel[0], pixel[1], 6, 11);
                choice_ava.Children.Add(graphic);
            }
            pointer_pos = 4;
            Pointer.Margin = new Thickness(180, 80, 0, 0);
        }
        private void ptmove2()
        {
            List<Pokemon> poke = x.PETS;
            choice_bg.Visibility = Visibility.Visible;
            choice_ava.Visibility = Visibility.Visible;

            string s;
            char[] txtchar;
            int[] pixel;

            for (int i = 0; i < poke.Count; i++)
            {
                s = poke[i].NAME + "   Lv" + poke[i].LV + "   HP:" + poke[i].HP + "/" + poke[i].MAXHP;
                txtchar = s.ToCharArray();

                graphic = new Rectangle();
                graphic.Height = 16;
                graphic.Width = 8;
                graphic.Margin = new Thickness(32, 0, 0, 0);
                choice_ava.Children.Add(graphic);

                for (int j = 0; j < txtchar.Length; j++)
                {
                    graphic = new Rectangle();
                    graphic.Height = 16;
                    graphic.Width = 8;
                    graphic.Margin = new Thickness(0);
                    pixel = Pixel_Position(txtchar[j]);
                    graphic.Fill = ImagePainting("33730.png", pixel[0], pixel[1], 6, 11);
                    choice_ava.Children.Add(graphic);
                }
                graphic = new Rectangle();
                graphic.Height = 16;
                graphic.Width = 320 - 32 - 8 * txtchar.Length;
                graphic.Margin = new Thickness(0, 0, 0, 0);
                choice_ava.Children.Add(graphic);

                graphic = new Rectangle();
                graphic.Height = 2;
                graphic.Width = 320;
                graphic.Margin = new Thickness(0, 0, 0, 0);
                choice_ava.Children.Add(graphic);
            }
            pointer_pos = 9;
            Pointer.Margin = new Thickness(180, 80, 0, 0);
        }
        private void ptmove3()
        {
            /*
            SoundPlayer player = new SoundPlayer("media/sound/forbidden.wav");
            player.Load();
            player.Play();
             */
        }
        private void ptmove4()
        {
            Display(play.dialogue(Gym.diag.heal,x.POKENOW.heal(x)));
            Action_Reset();
        }
        private async void ptmove5to8()
        {
            int result;
            text_clear();
            Action_Menu.Visibility = Visibility.Hidden;
            Pointer.Visibility = Visibility.Hidden;
            choice_ava.Visibility = Visibility.Hidden;
            choice_bg.Visibility = Visibility.Hidden;

            await HitSpEffect();
            await BattlePoke();
            EnPokeDisplay(play.POKENOW);
            MePokeDisplay(x.POKENOW);
            
            result = play.statusUpdate();
            if (result == 2)
            {
                PokeBallDisplay();
                EnPoke.Visibility = Visibility.Hidden;
                await Display(play.dialogue(Gym.diag.startend, false));
                x.GymVictory();
                PokeMap.Instance.ResetGym(play.NATURE);
                this.Close();
            }
            else
            {
                
                if (result == 1)
                {                    
                    PokeBallDisplay();
                    EnPoke.Visibility = Visibility.Hidden;
                    await Display(play.dialogue(Gym.diag.ko, result == 1));
                    EnPoke.Visibility = Visibility.Visible;
                    EnPokeDisplay(play.POKENOW);
                }
                else
                {
                    EnPoke.Fill = ImagePainting("dive.png");
                    await Task.Delay(2000);
                    EnPoke.Fill = ImagePainting("pokeBack/magikarp.png");
                    await Display(play.dialogue(Gym.diag.ko, result == 1));
                }
                
                
                Action_Menu.Visibility = Visibility.Visible;
                Pointer.Visibility = Visibility.Visible;
                Action_Reset();
            }
        }
        private void ptmove9to16()
        {
            bool tf=x.SwitchPoke(x.PETS[pointer_pos - 9]);
            MePokeDisplay(x.POKENOW);
            Action_Reset();
            Task t= Display(play.dialogue(Gym.diag.poke, tf));                        
        }
        private void Action_Reset()
        {
            graphic = new Rectangle();
            graphic.Height = 24;
            graphic.Width = 320;
            graphic.Margin = new Thickness(0, 0, 0, 0);

            choice_ava.Children.Clear();
            choice_ava.Children.Add(graphic);
            choice_ava.Visibility = Visibility.Hidden;
            choice_bg.Visibility = Visibility.Hidden;

            pointer_pos = 0;
            Pointer.Margin = new Thickness(400, 350, 0, 0);
        }

        public static void OpenBattleScreen()
        {
            GymBattleVS battleGame = new GymBattleVS();
            battleGame.Show();
        }

        private void on_Closed(object sender, EventArgs e)
        {
            MainWindow.gymTimer.Start();
            MainWindow.Instance.Show();
            MainWindow.Instance.Activate();
            Reset();
        }

        private void Reset()
        {
            instance = null;
        }

    }
}
