using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Part2MUD
{
    public class Player
    {
        //private Point playerCoordinate;
        public int x_pos,y_pos;                             //different from  the original one, double--->int
        private int money, exp;
        private List<Pokemon> pets = new List<Pokemon>();
        private Dictionary<string, int> Items = new Dictionary<string, int>();
        //private bool[] medals = new bool[8];
        private Pokemon currentPoke;            //new changes

        public int MONEY
        {
            get { return money; }
        }
        public int EXP
        {
            get { return exp; }
        }
        public List<Pokemon> PETS
        {
            get { return pets; }
        }
        public Dictionary<string, int> ITEMS
        {
            get { return Items; }
        }

        public Pokemon POKENOW             //new changes
        {
            get { return currentPoke; }
        }

        public void ItemPicked(String x)
        {
            Items[x]++;
        }
        public void PokeCaught(Pokemon x)
        {
            pets.Add(x);
        }

        public void GymVictory()
        {
            money += 10000;
            exp += 100;
        }

        public bool ItemExist(String itemX)
        {
            if (Items[itemX] > 0)
            {
                Items[itemX]--;
                return true;
            }
            return false;
        }
        public void ItemSold(String itemX)
        {
            if (ItemExist(itemX))
            {
                money += 100;
            }
        }
        public bool levelUpExp()
        {
            if (exp > 50)
            {
                exp -= 50;
                return true;
            }
            else return false;
        }
        public void expUP()
        {
            exp += 10;
        }
        public void EvilOwnerSellPokemon(Pokemon abandoned)
        {
            pets.Remove(abandoned);
            money += 1000;
        }
        public bool SwitchPoke(Pokemon change)
        {
            if (change.HP > 0)
            {
                currentPoke = change;
                return true;
            }
            else
            {
                return false;
            }

            
        }
        /*
        public void PositionRecording(double X, double Y)
        {
            x_pos = Convert.ToInt32(X);
            y_pos = Convert.ToInt32(Y);
        }
        */
        private Player()
        {
            Items.Add("HPHealer", 10);
            Items.Add("Water", 10);
            Items.Add("Fire", 0);
            Items.Add("Electric", 0);
            Items.Add("Grass", 0);
            Items.Add("Fighting", 0);
            Items.Add("Normal", 0);
            Items.Add("Flying", 0);
            Items.Add("Dark", 0);
            Items.Add("Psychic", 0);
            Items.Add("Steel", 0);
            Items.Add("Dragon", 0);
            Items.Add("Fairy", 0);
            Items.Add("Bug", 0);
            Items.Add("Poison", 0);
            Items.Add("Ground", 0);

            PETS.Add(new Pokemon("Pikachu"));
            SwitchPoke(PETS[0]);
            money = 0;
            exp = 0;
            //for (int i = 0; i < 8; i++) { medals[i] = false; }
            x_pos = 766;
            y_pos = 582;
        }
        static private Player instance;
        static public Player Instance
        {
            get
            {
                if (instance == null) instance = new Player();
                return instance;
            }
        }
    }
}
