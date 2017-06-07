using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_Part2MUD
{
    public class PokeGenerator
    {
        private Random rnd;
        public string wildPokemon;
        private static List<string> normal;
        private static List<string> evolve;
        private static List<string> evolve2;
        private static List<string> RandomPokemonList;
        private static int _stepsToEncounter;
        public int currentStep;
        // new
        private bool _encountered;
        private bool _encounteredAll;
        public bool encountered
        {
            get { return _encountered; }
            set { _encountered = value; }
        }
        public bool encounteredAll
        {
            get { return _encounteredAll; }
            set { _encounteredAll = value; }
        }
        // new
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
                // new
                encountered = false;
                encounteredAll = false;
                // new
                if (_stepsToEncounter == currentStep)
                {
                    if (RandomPokemonList.Count > 0)
                    {
                        wildPokemon = RandomPokemonList[0];
                        RandomPokemonList.RemoveAt(0);
                        //MessageBox.Show(string.Format("Wild {0} appears!!!!!", wildPokemon));
                       // new
                        encountered = true;
                        // new
                        _stepsToEncounter = 0;
                    }
                    else //MessageBox.Show("You have encountered all 8 pokemons!!!!");
                    {
                        // new
                        encounteredAll = true;
                        // new
                    }
                }
            }
        }
        private static PokeGenerator instance;
        public static PokeGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PokeGenerator();
                }
                return instance;
            }
        }

        private PokeGenerator()
        {
            // new
            encountered = false;
            encounteredAll = false;
            // new
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
            //currentStep = rnd.Next(80, 200);
            currentStep = rnd.Next(4, 11);              // 4 to 10 steps to encounter a wild pokemon
        }
    }
}
