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

namespace test
{
    /// <summary>
    /// Interaction logic for ItemScreen.xaml
    /// </summary>
    public partial class ItemScreen : Window
    {
        private ItemScreen()
        {
            InitializeComponent();
            Player x = Player.Instance;

            ExpBlock.Text = "EXP:" + x.EXP;
            MoneyBlock.Text = "Money:$" + x.MONEY;
            PokeOperator.Visibility = Visibility.Hidden;
            ShowPoke();
            this.Show();
        }

        private void ShowPoke()                   //This part is updated
        {
            //ImageBrush ib = new ImageBrush();
            Player x = Player.Instance;  //testing use
            List<Pokemon> pokedis = x.PETS;
            WrapPanel pokeinfo;
            Rectangle pokepic;
            TextBlock pokedata;
            for (int i = 0; i < pokedis.Count; i++)
            {
                pokeinfo = display.Children[i] as WrapPanel;
                pokepic = pokeinfo.Children[0] as Rectangle;
                pokedata = pokeinfo.Children[1] as TextBlock;
                pokeinfo.Visibility = Visibility.Visible;

                string temp = pokedis[i].NAME;
                temp = char.ToLower(temp[0]).ToString() + temp.Substring(1);
                pokepic.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/media/pokemon/" + temp + ".png")));
                pokedata.Text = pokedis[i].NAME + "\nLv:" + pokedis[i].LV + "\nAtk:" + pokedis[i].ATK + " Def:" + pokedis[i].DEF + "\nHP:" + pokedis[i].HP + "/" + pokedis[i].MAXHP;
                pokedata.Foreground = Brushes.Yellow;
                AssistTool.RemoveRoutedEventHandlers(pokeinfo, MouseUpEvent);
                int index = i;
                pokeinfo.MouseUp += (sender, EventArgs) => { PokeSelect_Click(sender, EventArgs, pokedis[index]); };
            }
            for (int i = pokedis.Count; i < 8; i++)
            {
                pokeinfo = display.Children[i] as WrapPanel;
                pokeinfo.Visibility = Visibility.Hidden;
            }
            MsgBlock.Text = "Click on the Pokemon that you want to do something about";
            PokeOperator.Visibility = Visibility.Hidden;
        }

        private void ShowItem()                        //This part is updated
        {
            Player x = Player.Instance;  //testing use
            Dictionary<string, int> itemdis = x.ITEMS;
            WrapPanel iteminfo;
            Rectangle itempic;
            TextBlock itemdata;

            int j = 0;
            foreach (KeyValuePair<string, int> pair in itemdis)
            {
                if (pair.Value > 0)
                {
                    iteminfo = display.Children[j] as WrapPanel;
                    itempic = iteminfo.Children[0] as Rectangle;
                    itemdata = iteminfo.Children[1] as TextBlock;
                    iteminfo.Visibility = Visibility.Visible;
                    j++;

                    itempic.Fill = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/media/item/" + (pair.Key) + ".png")));
                    itemdata.Text = pair.Key + "X" + pair.Value;
                    itemdata.Foreground = Brushes.Yellow;

                    AssistTool.RemoveRoutedEventHandlers(iteminfo, MouseUpEvent);
                    iteminfo.MouseUp += (sender, EventArgs) => { ItemSold_Click(sender, EventArgs, pair.Key); };   //elegant solution for click event with parameters
                }
            }

            for (int i = j; i < 8; i++)
            {
                iteminfo = display.Children[j] as WrapPanel;
                iteminfo.Visibility = Visibility.Hidden;
            }
            MsgBlock.Text = "Click Item, Sell Item, Each worths $100";
            PokeOperator.Visibility = Visibility.Hidden;
        }

        //Allow user to see what he wants to see
        private void ShowPoke_Click(object sender,EventArgs e)
        {
            ShowPoke();
        }
        private void ShowItem_Click(object sender,EventArgs e)
        {
            ShowItem();
        }
        //Sell items
        private void ItemSold_Click(object sender, EventArgs e, string item)               //This part is updated
        {
            Player x = Player.Instance;
            x.ItemSold(item);
            MoneyBlock.Text = "Money:$" + x.MONEY;
            ShowItem();
        }       
        //Attach events to the buttons
        private void PokeSelect_Click(object sender, EventArgs e, Pokemon x)
        {
            PokeDeselect(x);
            PokeOperator.Visibility = Visibility.Visible;
            HealButton.Click+= (senderX, EventArgs) => { Heal_Click(senderX, EventArgs, x); };
            UpButton.Click+= (senderX, EventArgs) => { LevelUp_Click(senderX, EventArgs, x); };
            EvolveButton.Click+=(senderX, EventArgs) => { Evolve_Click(senderX, EventArgs, x); };
            SellButton.Click+= (senderX, EventArgs) => { Sell_Click(senderX, EventArgs, x); };
        }
        //Remove events from the buttons
        private void PokeDeselect(Pokemon x)
        {
            AssistTool.RemoveRoutedEventHandlers(HealButton, Button.ClickEvent);
            AssistTool.RemoveRoutedEventHandlers(UpButton, Button.ClickEvent);
            AssistTool.RemoveRoutedEventHandlers(EvolveButton, Button.ClickEvent);
            AssistTool.RemoveRoutedEventHandlers(SellButton, Button.ClickEvent);
        }
        //THE buttons that help manage pokemon
        private void Heal_Click(object sender, EventArgs e, Pokemon x)         //This part is updated
        {
            Player y = Player.Instance;
            if(!x.heal(y))MessageBox.Show("You don't have HP Healer!! What a careless trainer!!");
            ShowPoke();
        }
        private void LevelUp_Click(object sender, EventArgs e, Pokemon x)   //This part is updated
        {
            Player y = Player.Instance;
            if(!x.levelUp(y))MessageBox.Show("You don't have enough exp!! Try to pick up some more fights with other trainers to earn exp!!");
            ExpBlock.Text = "EXP:" + y.EXP;
            ShowPoke();
        }
        private void Evolve_Click(object sender, EventArgs e, Pokemon x)        //This part is updated
        {
            Player y = Player.Instance;
            if(!x.evolve(y))MessageBox.Show("You don't have the stone!! Go find it!!");
            ShowPoke();
        }
        private void Sell_Click(object sender, EventArgs e, Pokemon x)
        {
            Player y = Player.Instance;
            y.EvilOwnerSellPokemon(x);
            MoneyBlock.Text = "Money:$" + y.MONEY;
            ShowPoke();           
        }

        public static void OpenItemScreen()
        {
            ItemScreen i = new ItemScreen();
        }

        private void on_Closed(object sender, EventArgs e)
        {
            MainWindow.Instance.Show();
            MainWindow.Instance.Activate();
        }

        private void on_Activated(object sender, EventArgs e)
        {
            MainWindow.Instance.Hide();
        }

        private void on_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.B)
            {
                this.Close();
            }
        }
    }
}
