using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Part2MUD
{
    public class Presenter
    {
        Player x = Player.Instance;
        PokeMap world = PokeMap.Instance;
        PokeDex knowledge = PokeDex.Instance;
        // new
        PokeGenerator pkGenerator = PokeGenerator.Instance;
        private bool _catch;
        public bool Catch{
            get { return _catch; }
            set { _catch = value; }
        }
        // new
        private const int worldsize= 25;
        public enum direction{ W,A,S,D };
        int player_xpos, player_ypos;
        
        private Presenter(){
            player_xpos = x.x_pos / 60;     //12
            player_ypos = x.y_pos / 60;     //9
        }
        private static Presenter instance;
        public static Presenter Instance
        {
            get
            {
                if (instance == null) instance = new Presenter();
                return instance;
            }
        }
        
        //The gym battle function has not been implemented
        public void GymDiagDisplay(string[] diag)
        {
            for (int i = 0; i < diag.Length; i++)
            {
                Console.WriteLine(diag[i]);
            }
        }
        public void GymStatus(Gym fight)
        {
            Console.WriteLine("Total number of opponents:"+fight.TOTAL);
            Console.WriteLine("Number of defeated pokemon:" + fight.KO);
            Console.WriteLine("Current opponent:{0} Lv:{1} HP:{2}/{3}", fight.POKENOW.NAME, fight.POKENOW.LV, fight.POKENOW.HP, fight.POKENOW.MAXHP);
            Console.WriteLine("Your pokemon:{0} Lv:{1} HP:{2}/{3}", x.POKENOW.NAME, x.POKENOW.LV, x.POKENOW.HP, x.POKENOW.MAXHP);
            Console.WriteLine("Choose your action:");
            Console.WriteLine("A.Fight");
            Console.WriteLine("B.Change Pokemon");
            Console.WriteLine("C.Use Item");

            bool quit = false;
            char input;
            while (!quit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                input = Console.ReadKey().KeyChar;
                Console.ResetColor();
                Console.WriteLine();
                switch (input)
                {
                    case 'a':
                    case 'A':
                        GymBattle(fight);
                        quit = true;
                        break;
                    case 'b':
                    case'B':
                        GymPoke(fight);
                        quit = true;
                        break;
                    case 'c':
                    case 'C':
                        GymItem(fight);
                        quit = true;
                        break;
                    default:
                        break;                       
                }
            }
        }
        public void GymBattle(Gym fight)
        {
            Console.WriteLine("Choose a skill to attack!!");
            for (int i = 0; i < x.POKENOW.SKILLS.Count; i++)
            {
                Console.WriteLine("{0}.{1}", (char)(i + 'A'), x.POKENOW.SKILLS[i]);
            }
            Console.WriteLine("Q.Quit skill menu");

            int target;
            int result;
            bool quit = false;
            char input;
            while (!quit)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                input = Console.ReadKey().KeyChar;
                Console.ResetColor();
                Console.WriteLine();
                switch (input)
                {
                    case 'Q':
                    case 'q':
                        quit = true; break;
                    default:
                        if (input >= 'a' && input <= (char)('a' + x.POKENOW.SKILLS.Count - 1) || (input >= 'A' && input <= (char)('A' + x.POKENOW.SKILLS.Count - 1)))
                        {
                            if (input <= 'Z')
                            {
                                target = input - 'A';
                            }
                            else
                            {
                                target = input - 'a';
                            }                            
                            string atk = x.POKENOW.SKILLS[target];
                            int dmg = x.POKENOW.Attack(atk, fight.POKENOW);
                            GymDiagDisplay(fight.dialogue(dmg, atk));
                            result = fight.statusUpdate();
                            if (result == 2)
                            {
                                GymDiagDisplay(fight.dialogue(Gym.diag.startend, false));
                                world.ResetGym(fight.NATURE);
                                x.GymVictory();
                            }
                            else
                            {
                                if (result == 1)
                                {
                                    GymDiagDisplay(fight.dialogue(Gym.diag.ko, result == 1));
                                }
                                else
                                {
                                    GymDiagDisplay(fight.dialogue(Gym.diag.ko, result == 1));
                                }
                                GymStatus(fight);
                            }

                            quit = true;
                            
                        }
                        
                        break;
                }

            }
        }
        public void GymPoke(Gym fight) {            
            Console.WriteLine("Choose the pokemon that you would like to switch to:");
            for (int i = 0; i < x.PETS.Count; i++)
            {
                Pokemon tmp = x.PETS[i];
                Console.WriteLine("{6}.{0} Lv:{1} HP:{2}/{3} ATK:{4} DEF:{5}", tmp.NAME, tmp.LV, tmp.HP, tmp.MAXHP, tmp.ATK, tmp.DEF, (char)(i+'A'));
            }
            Console.WriteLine("Q.Quit pokeon Menu");

            int target;
            bool quit = false;
            char input;
            while (!quit)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                input = Console.ReadKey().KeyChar;
                Console.ResetColor();
                Console.WriteLine();
                switch (input)
                {
                    case 'Q':
                    case 'q':
                        quit = true; break;
                    default:
                        if (input >= 'a' && input <= (char)('a' + x.PETS.Count - 1) || input >= 'A' && input <= (char)('A' + x.PETS.Count - 1))
                        {
                            if (input <= 'Z')
                            {
                                target = input - 'A';
                            }
                            else
                            {
                                target = input - 'a';
                            }
                                
                            bool tf = x.SwitchPoke(x.PETS[target]);
                            GymDiagDisplay(fight.dialogue(Gym.diag.poke, tf));                            
                            quit = true;
                            GymStatus(fight);
                        }
                        
                        break;
                }

            }
        }
        public void GymItem(Gym fight) {
            if(x.ITEMS["HPHealer"]>0){
                Console.WriteLine("You can use the following item:");
                Console.WriteLine("H.HPHealer   X{0}", x.ITEMS["HPHealer"]);
            }
            Console.WriteLine("Q.Quit item menu");
            
            bool quit = false;
            char input;
            while (!quit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                input = Console.ReadKey().KeyChar;
                Console.ResetColor();
                Console.WriteLine();
                switch (input)
                {
                    case 'h':
                    case 'H':
                        GymDiagDisplay(fight.dialogue(Gym.diag.heal, x.POKENOW.heal(x)));                     
                        GymStatus(fight);
                        quit = true;
                        break;
                    case 'q':
                    case 'Q':
                        GymStatus(fight);
                        quit = true;
                        break;
                    default: break;
                }
            }
        }

        public void CatchPokemon()
        {
            Catch = false;
            Console.WriteLine("Throwing pokeball...");
            Random r = new Random();
            if (r.NextDouble() > 0.5)
            {
                Console.WriteLine("You caught {0}", pkGenerator.wildPokemon);
                x.PETS.Add(new Pokemon(pkGenerator.wildPokemon));
            }
            else
            {
                Console.WriteLine("{0} has escaped!", pkGenerator.wildPokemon);
            }
            pkGenerator.encountered = false;
            Console.ReadKey();
            Console.WriteLine();
        }

        
        //Although the interface is prepared, but the choosing of pokemon function has not been implemented
        public void PokeFullDisplay()
        {
            bool quit = false;
            Console.WriteLine("Which pokemon would you like to handle?");
            for (int i = 0; i < x.PETS.Count; i++)
            {
                Pokemon tmp = x.PETS[i];
                Console.WriteLine("{7}. {0} Type:{1} Lv:{2} HP:{3}/{4} ATK:{5} DEF:{6}", tmp.NAME, tmp.NA_TYPE, tmp.LV, tmp.HP, tmp.MAXHP, tmp.ATK, tmp.DEF, (char)(i+'A'));
                Console.Write("Skills:");
                for (int j = 0; j < tmp.SKILLS.Count; j++)
                {
                    Console.Write(tmp.SKILLS[j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Q.Quit Menu");
            char input;
            while (!quit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                input = Console.ReadKey().KeyChar;
                Console.ResetColor();
                int index;
                if (input >= 'a' && input <= 'z')
                {
                    index = (int)input - 'a';
                }else if(input>='A' && input<='Z'){
                    index = (int)input - 'A';
                }
                else
                {
                    index = -1;
                }
                
                if (index >= 0 && index < x.PETS.Count)
                {
                    Console.WriteLine();
                    Console.WriteLine("What do you want to do with {0}?", x.PETS[index].NAME, index);
                    Console.WriteLine("H.Heal");
                    Console.WriteLine("L.Level Up");
                    Console.WriteLine("E.Evolve");
                    Console.WriteLine("S.Sell");
                    Console.WriteLine("B.Back");
                    bool back = false;
                    while (!back)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        char action = Console.ReadKey().KeyChar;
                        Console.ResetColor();
                        Console.WriteLine();
                        back = true;
                        switch (action)
                        {
                            case 'H':
                            case 'h': Heal(x.PETS[index]); break;
                            case 'l':
                            case 'L': LevelUp(x.PETS[index]); break;
                            case 'e':
                            case 'E': Evolve(x.PETS[index]); break;
                            case 's':
                            case 'S': Sell(x.PETS[index]); break;
                            case 'b':
                            case 'B': break;
                            default: back = false; break;
                        }
                        quit = true;
                    }
                }

                switch (input)
                {
                    case 'Q':
                    case 'q':
                        quit = true; break;

                }
            }
            Console.WriteLine();
        }                 
        public void Heal(Pokemon target)
        {
            if (target.heal(x)) { 
                Console.WriteLine("{0} has healed to full hp!", target.NAME); 
            }else{ 
                Console.WriteLine("Sorry, you dont have hp healer."); 
            }
        }
        public void LevelUp(Pokemon target)
        {
            if (target.levelUp(x)) { 
                Console.WriteLine("{0} has leveled up", target.NAME); 
            }else { 
                Console.WriteLine("Sorry, you dont have enough exp."); 
            }
        }
        public void Evolve(Pokemon target)
        {
            string pkmon = target.NAME;
            if (target.evolve(x)) { 
                Console.WriteLine("{0} has evolved to {1}", pkmon, target.NAME); 
            }else{ 
                Console.WriteLine("Sorry, you dont have evolve stone, go find one."); 
            }
        }
        public void Sell(Pokemon target)
        {
            string pkmon = target.NAME;
            x.EvilOwnerSellPokemon(target);
            Console.WriteLine("Your {0} is sold.", pkmon);
        }
        
        public void ItemDisplay()
        {
            Console.WriteLine("You have the following items:");
            List<string> items=new List<string>();
            int count =0;
            foreach(KeyValuePair<string, int> tmp in x.ITEMS){
                if(tmp.Value>0){
                    if (tmp.Key == "HPHealer")
                    {
                        Console.WriteLine("{2}:{1} X{0}", tmp.Value, tmp.Key, (char)('A' + count));
                    }
                    else
                    {
                        Console.WriteLine("{2}:{1} stone X{0}", tmp.Value, tmp.Key, (char)('A' + count));
                    }                   
                    items.Add(tmp.Key);
                    count++;
                }
            }
            Console.WriteLine("If you want to sell anything, just press the corresponding letter");
            Console.WriteLine("Q.Quit Menu");
            ItemChoice(items, count);
        }
        public void ItemChoice(List<string> items, int count)
        {
            bool quit = false;
            char input;
            int tmp;
            while (!quit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                input = Console.ReadKey().KeyChar;
                Console.ResetColor();
                Console.WriteLine();
                switch (input)
                {
                    case 'Q':
                    case 'q':
                        quit = true; break;
                    default:
                        if (input >= 'a' && input <= (char)('a' + count-1))
                        {
                            tmp = input - 'a';
                            x.ItemSold(items[tmp]);
                            Console.WriteLine("You have sold a {0}", items[tmp]);
                            ItemDisplay();
                            quit = true;
                        }
                        else if (input >= 'A' && input <= (char)('A' + count-1))
                        {
                            tmp = input - 'A';
                            x.ItemSold(items[tmp]);
                            Console.WriteLine("You have sold a {0}", items[tmp]);
                            ItemDisplay();
                            quit = true;                            
                        }                        
                        break;

                }
            }
        }

        public void Movement(char wasd)
        {
            pkGenerator.stepsToEncounter++;
            switch (wasd)
            {
                case 'W':
                case 'w': if (player_ypos > 0) player_ypos--; break;
                case 'A':
                case 'a': if (player_xpos > 0) player_xpos--; break;
                case 'S':
                case 's': if (player_ypos < worldsize - 1) player_ypos++; break;
                case 'D':
                case 'd': if (player_xpos < worldsize - 1) player_xpos++; break;
            }
        }
        public int MapPrint()
        {
            int position = -1;
            int xstart, xend, ystart, yend;
            int gym_xpos = -1, gym_ypos = -1;

            if (player_xpos < 4)
            {
                xstart = 0;
                xend = 8;
            }
            else if (player_xpos > worldsize - 5)
            {
                xstart = worldsize - 9;
                xend = worldsize - 1;
            }
            else
            {
                xstart = player_xpos - 4;
                xend = player_xpos + 4;
            }
            if (player_ypos < 2)
            {
                ystart = 0;
                yend = 4;
            }
            else if (player_ypos > worldsize - 3)
            {
                ystart = worldsize - 5;
                yend = worldsize - 1;
            }
            else
            {
                ystart = player_ypos - 2;
                yend = player_ypos + 2;
            }
            //Console.WriteLine("Player X pos:{0} Y pos:{1} XS:{2} XE:{3} YS:{4} YE:{5}", player_xpos, player_ypos, xstart, xend, ystart, yend);

            Gym tmp = null;
            //Console.WriteLine(world.GetAllGym.Length);
            for (int i = 0; i < world.GetAllGym.Length; i++)
            {
                gym_xpos = world.GetAllGym[i].Position[0];              //20
                gym_ypos = world.GetAllGym[i].Position[1];              //2
                //Console.WriteLine("Gym X pos:{0} Y pos:{1}", gym_xpos, gym_ypos);
                if (gym_xpos >= xstart && gym_xpos <= xend && gym_ypos >= ystart && gym_ypos <= yend)
                {
                    position = i;
                    tmp = world.GetAllGym[i];
                    break;
                }
            }
            //tmp = world.GetTypedGym(Gym.na_type.water);
            //Console.WriteLine(tmp.Position[0]);
            for (int i = ystart; i <= yend; i++)         // 7 to 11
            {
                for (int j = xstart; j <= xend; j++)     // 8 to 16
                {
                    if (position == -1)
                    {
                        if (j == player_xpos && i == player_ypos) Console.Write("X");
                        else Console.Write(".");
                    }
                    else
                    {
                        if (gym_ypos == player_ypos && gym_xpos == player_xpos)
                        {
                            Console.Write("Z");
                        }
                        else
                        {
                            if (j == gym_xpos && i == gym_ypos) Console.Write("G");
                            else if (j == player_xpos && i == player_ypos) Console.Write("X");
                            else Console.Write(".");
                        }
                    }
                }
                Console.WriteLine();
            }
            return position;
        }
        public int MapDisplay()
        {
            Catch = false;
            int gym_found = MapPrint();            
            if (gym_found!=-1) Console.WriteLine("You find a gym nearby...Wanna try it?");
            // new
            else if (pkGenerator.encountered) { Console.WriteLine("Wild {0} appears!!!!! Wanna catch it?", pkGenerator.wildPokemon); Catch = true; }
            else if (pkGenerator.encounteredAll) Console.WriteLine("You have encountered all 8 pokemons!!!!");
            // new
            else Console.WriteLine("A world of nothing...");
             
            Console.WriteLine("W.Move UP");
            Console.WriteLine("A.Move LEFT");
            Console.WriteLine("S.Move DOWN");
            Console.WriteLine("D.Move RIGHT");
            Console.WriteLine("V.View your pokemon");
            Console.WriteLine("R.View your items");
            if (gym_found!=-1) Console.WriteLine("B.Battle in gym");
            if (pkGenerator.encountered) Console.WriteLine("C.Catch pokemon");
            Console.WriteLine("X.Exit game");
            return gym_found;
        }
    }
    
    
    
    class Program
    {
        public enum direction { W, A, S, D };
        static void Main(string[] args)
        {
            Presenter god = Presenter.Instance;
            PokeMap world = PokeMap.Instance;
            Console.WriteLine("Welcome to the Pokemon World!! Press the letter you see to perform the corresponding action");
            char input;
            bool endGame=false;
            int battle;
            while (!endGame)
            {
                battle=god.MapDisplay();
                Console.ForegroundColor = ConsoleColor.Red;
                input=Console.ReadKey().KeyChar;
                Console.ResetColor();
                Console.WriteLine();
                switch (input)
                {
                    case 'V':
                    case 'v':
                        god.PokeFullDisplay();break;
                    case 'R':
                    case 'r':
                        god.ItemDisplay();break;
                    case 'W':
                    case 'w':
                    case 'A':
                    case 'a':
                    case 'S':
                    case 's':
                    case 'D':
                    case 'd':
                        god.Movement(input);break;
                    case 'B':
                    case 'b':
                        if (battle!=-1) god.GymStatus(world.GetAllGym[battle]);break;
                    case 'C':
                    case 'c':
                        if (god.Catch) {god.CatchPokemon();} break;
                    case 'X':
                    case 'x':
                        endGame = true;break;
                    default:break;
                }
                //Console.Clear();
                //Console.WriteLine("===================================");
            }
            //Console.ReadKey(); //Pause
        }
    }
}
