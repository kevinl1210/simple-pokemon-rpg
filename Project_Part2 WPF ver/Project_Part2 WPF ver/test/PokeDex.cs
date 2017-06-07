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
using System.Reflection;

namespace test
{
    public class PokeDex
    {
        public  Dictionary<string, string[]> evolve_dex = new Dictionary<string, string[]>();
        public  Dictionary<string, PokeDetail> poke_detail = new Dictionary<string, PokeDetail>();

        public class PokeDetail
        {
            private string nature_type;
            private string species;
            private int hp, atk, def;

            public string NATURE_TYPE
            {
                get { return nature_type; }
            }
            public int HP
            {
                get { return hp; }
            }
            public int ATK
            {
                get { return atk; }
            }
            public int DEF
            {
                get { return def; }
            }
            public string SPECIES
            {
                get { return species; }
                set { species = value; }
            }

            public PokeDetail(string x, int hp, int atk, int def)
            {
                nature_type = x;
                this.hp = hp;
                this.atk = atk;
                this.def = def;
            }
        }

        private PokeDex()
        {
            char[] whitespace = new char[] { ' ', '\t' };
            String line;
            try
            {
                using (StreamReader sr = new StreamReader("evolve_list.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] series = line.Split(whitespace);
                        evolve_dex.Add(series[0], series);
                    }

                }
                using (StreamReader sr = new StreamReader("poke_detail.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] series = line.Split(whitespace);
                        PokeDetail x = new PokeDetail(series[2], Int32.Parse(series[3]), Int32.Parse(series[4]), Int32.Parse(series[5]));
                        //x.SPECIES = evolve_dex.Keys
                        //                      .Where(v => v.Contains(series[1]))
                        //                      .FirstOrDefault();
                        x.SPECIES = evolve_dex.FirstOrDefault(v => v.Value.Any(y => y.Contains(series[1]))).Key;
                        poke_detail.Add(series[1], x);
                    }
                }
            }
            catch (Exception) { Environment.Exit(0); }
        }
        static private PokeDex instance;
        static public PokeDex Instance
        {
            get
            {
                if (instance == null) instance = new PokeDex();
                return instance;
            }
        }
    }
}
