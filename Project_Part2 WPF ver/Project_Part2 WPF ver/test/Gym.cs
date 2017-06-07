using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Gym
    {
        List<Pokemon> Enemy = new List<Pokemon>();
        Player x = Player.Instance;
        private int pokeTotal;
        private int pokeDefeated;
        private Pokemon currentPoke;
        private int x_position, y_position;
        private na_type nature;
        public enum diag { heal, poke, ko, startend };
        public enum na_type { water, fire, rock, electric, grass };

        private Gym(int x, int y, na_type type)
        {
            switch (type)
            {
                case na_type.water:
                    Enemy.Add(new Pokemon("Magikarp", "Water", "Magikarp", 1, 8, 10, 55, null));
                    Enemy.Add(new Pokemon("Magikarp", "Water", "Magikarp", 1, 12, 10, 55, null));
                    Enemy.Add(new Pokemon("Magikarp", "Water", "Magikarp", 1, 16, 10, 55, null));
                    Enemy.Add(new Pokemon("Magikarp", "Water", "Magikarp", 1, 20, 10, 55, null));
                    break;
            }
            nature = type;
            x_position = x;
            y_position = y;
            pokeDefeated = 0;
            pokeTotal = Enemy.Count;
            currentPoke = Enemy[pokeDefeated];
        }
        static private Dictionary<na_type, Gym> instance;
        static public Gym Instance(int x, int y, na_type type)
        {
            if (instance == null) instance = new Dictionary<na_type, Gym>();
            if (!instance.ContainsKey(type))
            {
                instance.Add(type, new Gym(x, y, type));
            }
            return instance[type];
        }

        public int[] Position
        {
            get
            {
                int[] tmp = new int[2];
                tmp[0] = x_position / 60;
                tmp[1] = y_position / 60;
                return tmp;
            }
        }
        public int[] PositionRaw
        {
            get
            {
                int[] tmp = new int[2];
                tmp[0] = x_position;
                tmp[1] = y_position;
                return tmp;
            }
        }
        public int TOTAL { get { return pokeTotal; } }
        public int KO { get { return pokeDefeated; } }
        public List<Pokemon> ENEMY { get { return Enemy; } }
        public Pokemon POKENOW { get { return currentPoke; } }
        public na_type NATURE { get { return nature; } }
        public int statusUpdate()
        {
            if (currentPoke.HP <= 0)
            {
                pokeDefeated++;
                if (pokeDefeated == pokeTotal)
                {
                    return 2;
                }
                else
                {
                    currentPoke = Enemy[pokeDefeated];
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }
        public Gym PartialReset()
        {
            int tmpx, tmpy;
            tmpx = x_position;
            tmpy = y_position;
            instance.Remove(nature);
            instance.Add(nature, new Gym(tmpx, tmpy, nature));
            return instance[nature];
        }

        public string[] dialogue(diag action, bool possible)
        {
            List<string> temp = new List<string>();

            switch (action)
            {
                case diag.heal:
                    if (possible)
                    {
                        temp.Add(x.POKENOW.NAME + " fully healed!!");
                    }
                    else
                    {
                        temp.Add("Sorry, no HP Healer...");
                    }
                    break;
                case diag.poke:
                    if (possible)
                    {
                        temp.Add("Pokemon Switched!!");
                    }
                    else
                    {
                        temp.Add("You evil trainer!! Can't switch!!");
                    }
                    break;
                case diag.ko:
                    if (possible)
                    {
                        temp.Add(Enemy[pokeDefeated - 1].NAME + " has been defeated!!");
                        temp.Add("Here comes " + Enemy[pokeDefeated].NAME + "!!");
                    }
                    else
                    {
                        //Your enemy's turn, currently we just have the reaction of magikart
                        if (nature == na_type.water)
                        {
                            temp.Add(currentPoke.NAME + " dives!! Cure abandoned!!");
                            temp.Add("No damages have caused...");
                        }
                    }
                    break;
                case diag.startend:
                    if (possible)
                    {
                        temp.Add("Choose your action!");
                    }
                    else
                    {
                        if (nature == na_type.water)        //msg for winning the gym of water, available for adding more different dialogue for other gyms
                        {
                            temp.Add("Your have defeated this ... Magikart gym!!");
                            temp.Add("Is this really pride to do?");
                        }
                        temp.Add("Anyway, you got your 10000 dollars!");
                        temp.Add("Good Bye! You can come back later!");
                    }
                    break;
                default: break;
            }
            /*
            switch (action)
            {
                case diag.heal:
                case diag.poke:
                    temp.Add("Choose your action!");
                    break;
                default: break;
            }
            */
            string[] strs = temp.ToArray();
            return strs;
        }
        public string[] dialogue(int dmg, string fight)
        {
            List<string> temp = new List<string>();

            if (dmg != -1)
            {
                temp.Add(x.POKENOW.NAME + " uses " + fight + "!!!");
                if (dmg != 0)
                {
                    temp.Add(currentPoke.NAME + " received " + (dmg % 1000) + " points of damage!!!");
                    if (dmg > 1000)
                    {
                        temp.Add(x.POKENOW.NAME + "\'s HP is decreased by " + (dmg / 1000) + " points!!!");
                    }
                    if (dmg < -1000)
                    {
                        temp.Add(x.POKENOW.NAME + "\'s HP is recovered by " + (dmg / 1000) + " points!!!");
                    }
                }
                else
                {
                    temp.Add("It was not effective...");
                }
            }
            else
            {
                temp.Add("This is a near-death skill, use it later");
            }
            string[] strs = temp.ToArray();
            return strs;
        }
    }
}
