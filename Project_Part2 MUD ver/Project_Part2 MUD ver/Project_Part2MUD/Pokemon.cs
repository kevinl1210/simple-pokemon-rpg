using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;

namespace Project_Part2MUD
{
    public class Pokemon
    {
        private int lv, maxhp, hp, atk, def;
        private int species_rank;
        private string nature_type, name, species;
        private List<string> skills;

        public int MAXHP { get { return maxhp; } }
        public int LV { get { return lv; } }
        public string NA_TYPE { get { return nature_type; } }
        public List<string> SKILLS { get { return skills; } }
        public int HP { get { return hp; } }
        public int ATK { get { return atk; } }
        public int DEF { get { return def; } }
        public string NAME { get { return name; } }

        //The follow 3 parts are updated!!!
        public bool levelUp(Player x)
        {
            if (x.levelUpExp())
            {
                lv += 1;
                maxhp += 2;
                hp = maxhp;
                return true;
            }
            else return false;
        }
        public bool evolve(Player x)
        {
            if (x.ItemExist(nature_type))
            {
                if ((species_rank + 1) < PokeDex.Instance.evolve_dex[species].Length)
                {
                    species_rank += 1;
                    name = PokeDex.Instance.evolve_dex[species][species_rank];
                    PokeDex.PokeDetail temp = PokeDex.Instance.poke_detail[name];
                    maxhp = temp.HP + (lv - 1) * 2;
                    atk = temp.ATK;
                    def = temp.DEF;
                    hp = maxhp;
                    return true;
                }
                else
                {
                    x.ItemPicked(nature_type);
                    return true;
                }
            }
            else return false;
        }
        public bool heal(Player x)
        {
            if (x.ItemExist("HPHealer"))
            {
                hp = maxhp;
                return true;
            }
            else return false;
        }

        public void skill_gain()
        {
            Random rnd = new Random();
            int luck = rnd.Next(1, 6);
            if (skills.Count < 3)
            {
                using (StreamReader sr = new StreamReader("skill_list.txt"))
                {
                    String line = null;
                    for (int i = 1; i <= luck; i++)
                    {
                        line = sr.ReadLine();
                    }
                    while (!skills.Contains(line))
                    {
                        sr.DiscardBufferedData();
                        sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                        luck = rnd.Next(1, 6);
                        for (int i = 1; i <= luck; i++)
                        {
                            line = sr.ReadLine();
                        }

                    }
                    skills.Add(line);
                }
            }
        }

        private class Type_Effect
        {
            public static int Water(String type)
            {
                switch (type)
                {
                    case "Fire":
                    case "Ground":
                        return 20;
                    case "Water":
                    case "Grass":
                    case "Dragon":
                        return 5;
                    default: return 10;
                }
            }
            public static int Fire(String type)
            {
                switch (type)
                {
                    case "Grass":
                    case "Bug":
                    case "Steel":
                    case "Ice":
                        return 20;
                    case "Water":
                    case "Fire":
                    case "Dragon":
                    case "Rock":
                        return 5;
                    default: return 10;
                }
            }
            public static int Electric(String type)
            {
                switch (type)
                {
                    case "Water":
                    case "Flying":
                        return 20;
                    case "Electric":
                    case "Grass":
                    case "Dragon":
                        return 5;
                    case "Ground":
                        return 0;
                    default: return 10;
                }
            }
            public static int Grass(String type)
            {
                switch (type)
                {
                    case "Water":
                    case "Ground":
                    case "Rock":
                        return 20;
                    case "Fire":
                    case "Dragon":
                    case "Grass":
                    case "Poison":
                    case "Flying":
                    case "Bug":
                    case "Steel":
                        return 5;
                    default: return 10;
                }
            }
            public static int Fighting(String type)
            {
                switch (type)
                {
                    case "Normal":
                    case "Ice":
                    case "Rock":
                    case "Dark":
                    case "Steel":
                        return 20;
                    case "Poison":
                    case "Flying":
                    case "Psychic":
                    case "Bug":
                    case "Fairy":
                        return 5;
                    case "Ghost":
                        return 0;
                    default: return 10;
                }
            }
            public static int Normal(String type)
            {
                switch (type)
                {
                    case "Steel":
                    case "Rock":
                        return 5;
                    case "Ghost":
                        return 0;
                    default: return 10;
                }
            }
            public static int Flying(String type)
            {
                switch (type)
                {
                    case "Grass":
                    case "Fighting":
                    case "Bug":
                        return 20;
                    case "Electric":
                    case "Rock":
                    case "Steel":
                        return 5;
                    default: return 10;
                }
            }
            public static int Dark(String type)
            {
                switch (type)
                {
                    case "Psychic":
                    case "Ghost":
                        return 20;
                    case "Fighting":
                    case "Dark":
                    case "Fairy":
                        return 5;
                    default: return 10;
                }
            }
            public static int Psychic(String type)
            {
                switch (type)
                {
                    case "Fighting":
                    case "Poison":
                        return 20;
                    case "Psychic":
                    case "Steel":
                        return 5;
                    case "Dark":
                        return 0;
                    default: return 10;
                }
            }
            public static int Steel(String type)
            {
                switch (type)
                {
                    case "Rock":
                    case "Fairy":
                    case "Ice":
                        return 20;
                    case "Water":
                    case "Fire":
                    case "Electric":
                    case "Steel":
                        return 5;
                    default: return 10;
                }
            }
            public static int Dragon(String type)
            {
                switch (type)
                {
                    case "Dragon":
                        return 20;
                    case "Steel":
                        return 5;
                    case "Fairy":
                        return 0;
                    default: return 10;
                }
            }
            public static int Fairy(String type)
            {
                switch (type)
                {
                    case "Fighting":
                    case "Dragon":
                    case "Dark":
                        return 20;
                    case "Steel":
                    case "Fire":
                    case "Poison":
                        return 5;
                    default: return 10;
                }
            }
            public static int Bug(String type)
            {
                switch (type)
                {
                    case "Grass":
                    case "Psychic":
                    case "Dark":
                        return 20;
                    case "Fire":
                    case "Fighting":
                    case "Poison":
                    case "Flying":
                    case "Ghost":
                    case "Steel":
                    case "Fairy":
                        return 5;
                    default: return 10;
                }
            }
            public static int Poison(String type)
            {
                switch (type)
                {
                    case "Grass":
                    case "Fairy":
                        return 20;
                    case "Poison":
                    case "Ground":
                    case "Rock":
                    case "Ghost":
                        return 5;
                    case "Steel":
                        return 0;
                    default: return 10;
                }
            }
            public static int Ground(String type)
            {
                switch (type)
                {
                    case "Fire":
                    case "Electric":
                    case "Poison":
                    case "Rock":
                    case "Steel":
                        return 20;
                    case "Grass":
                    case "Bug":
                        return 5;
                    case "Flying":
                        return 0;
                    default: return 10;
                }
            }
            public static int Ice(String type)
            {
                switch (type)
                {
                    case "Grass":
                    case "Ground":
                    case "Flying":
                    case "Dragon":
                        return 20;
                    case "Fire":
                    case "Water":
                    case "Ice":
                    case "Steel":
                        return 5;
                    default: return 10;
                }
            }
            public static int Rock(String type)
            {
                switch (type)
                {
                    case "Fire":
                    case "Ice":
                    case "Flying":
                    case "Bug":
                        return 20;
                    case "Fighting":
                    case "Ground":
                    case "Steel":
                        return 5;
                    default: return 10;
                }
            }
            public static int Ghost(String type)
            {
                switch (type)
                {
                    case "Psychic":
                    case "Ghost":
                        return 20;
                    case "Dark":
                        return 5;
                    case "Normal":
                        return 0;
                    default: return 10;
                }
            }
        }
        private int Type_effective(Pokemon Me, Pokemon En)
        {
            Type type = typeof(Type_Effect);
            object obj = Activator.CreateInstance(type);
            object[] mParam = new object[] { En.nature_type };
            int count = (int)type.InvokeMember(Me.nature_type, BindingFlags.InvokeMethod, null, obj, mParam);
            return count;
            /*
            int counter=1;
            switch (nature_type)
            {
                case "Water": break;
                case "Fire": break;
                case "Electric": break;
                case "Grass": break;
                case "Fighting": break;
                case "Normal": break;
                case "Flying": break;
                case "Dark": break;
                case "Psychic": break;
                case "Steel": break;
                case "Dragon": break;
                case "Fairy": break;
                case "Bug": break;
                case "Poison": break;
                case "Ground": break;

            }
            return counter;
            */
        }

        public int Normal_Attack(Pokemon Me, Pokemon En)
        {
            Random rnd = new Random();
            int varies = rnd.Next(1, 9);
            int dmg;
            if (Me.atk - En.def > 1)
            {
                dmg = (Me.atk - En.def) * Type_effective(Me, En) / 10 + varies - 5;
            }
            else { dmg = 1; }
            En.hp -= dmg;
            if (En.hp < 0) En.hp = 0;
            return dmg;
        }
        public int Double_Edge(Pokemon Me, Pokemon En)
        {
            Random rnd = new Random();
            int varies = rnd.Next(1, 9);
            int dmg;

            if (Me.atk - En.def > 1)
            {
                dmg = (Me.atk - En.def) + varies - 5;
                dmg += ((Me.atk - En.def) + varies - 5) / 3 * 1000;
            }
            else
            {
                dmg = 1001;
            }
            En.hp -= dmg % 1000;
            Me.hp -= dmg / 1000;
            if (En.hp < 0) En.hp = 0;
            return dmg;
        }              //Special Treatment
        public int Fury_Attack(Pokemon Me, Pokemon En)
        {
            Random rnd = new Random();
            int varies = rnd.Next(1, 7);
            int count, dmg;

            switch (varies)
            {
                case 1:
                case 2:
                    count = 2; break;
                case 3:
                case 4:
                    count = 3; break;
                case 5:
                    count = 4; break;
                case 6:
                    count = 5; break;
                default: count = 2; break;
            }
            if (Me.atk - En.def > 1) { dmg = (Me.atk - En.def) / 3 * count * Type_effective(Me, En) / 10; }
            else { dmg = count; }

            En.hp -= dmg;
            if (En.hp < 0) En.hp = 0;
            return dmg;
        }
        public int Leech_Life(Pokemon Me, Pokemon En)
        {
            Random rnd = new Random();
            int varies = rnd.Next(1, 9);
            int dmg;
            if (atk - En.def > 1)
            {
                dmg = (Me.atk - En.def) * Type_effective(Me, En) / 10 + varies - 5;
                dmg += ((Me.atk - En.def) * Type_effective(Me, En) / 10 + varies - 5) / 2 * 1000;
            }
            else
            {
                dmg = 1001;
            }
            En.hp -= dmg % 1000;
            Me.hp += dmg / 1000;
            if (En.hp < 0) En.hp = 0;
            dmg = 0 - dmg;
            return dmg;
        }               //Special Treatment
        public int Strength(Pokemon Me, Pokemon En)
        {
            int dmg;
            dmg = 10;
            En.hp -= dmg;
            if (En.hp < 0) En.hp = 0;
            return dmg;
        }
        public int Death_Strike(Pokemon Me, Pokemon En)
        {
            Random rnd = new Random();
            int varies = rnd.Next(1, 3);
            int dmg;

            if (Me.hp < Me.maxhp / 4)
            {
                switch (varies)
                {
                    case 1: dmg = 20; break;
                    default: dmg = 0; break;
                }
                En.hp -= dmg;
            }
            else
            {
                dmg = -1;
            }

            if (En.hp < 0) En.hp = 0;
            return dmg;
        }

        public int Attack(string atkway, Pokemon enemy)
        {
            Type type = typeof(Pokemon);
            object obj = Activator.CreateInstance(type);
            object[] mParam = new object[] { this, enemy };
            int dmg = (int)type.InvokeMember(atkway, BindingFlags.InvokeMethod, null, obj, mParam);
            return dmg;
        }

        // exception
        public Pokemon() { }

        public Pokemon(string nameX)
        {
            lv = 1;
            PokeDex.PokeDetail temp = PokeDex.Instance.poke_detail[nameX];
            nature_type = temp.NATURE_TYPE;
            name = nameX;
            maxhp = temp.HP;
            hp = maxhp;
            atk = temp.ATK;
            //Console.WriteLine(atk);
            def = temp.DEF;
            species = temp.SPECIES;
            species_rank = Array.IndexOf(PokeDex.Instance.evolve_dex[species], name);
            skills = new List<string>();
            skills.Add("Normal_Attack");
        }
        //God Creation
        public Pokemon(string name, string nature_type, string species, int lv, int maxhp, int atk, int def)
        {
            this.name = name;
            this.nature_type = nature_type;
            this.species = species;
            this.lv = lv;
            this.maxhp = maxhp;
            hp = maxhp;
            this.atk = atk;
            this.def = def;
            skills.Add("Normal_Attack");
        }
        public Pokemon(string name, string nature_type, string species, int lv, int maxhp, int atk, int def, List<string> skills)
        {
            this.name = name;
            this.nature_type = nature_type;
            this.species = species;
            this.lv = lv;
            this.maxhp = maxhp;
            hp = maxhp;
            this.atk = atk;
            this.def = def;
            this.skills = skills;
        }

    }

}
