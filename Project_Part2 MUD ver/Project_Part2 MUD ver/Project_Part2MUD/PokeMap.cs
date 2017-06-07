using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Part2MUD
{
    class PokeMap
    {
        private Gym[] gyms;
        private PokeMap(){
            gyms = new Gym[1];
            gyms[0] = Gym.Instance(1215, 178, Gym.na_type.water);
        }

        private static PokeMap instance;
        public static PokeMap Instance
        {
            get
            {
                if (instance == null) instance = new PokeMap();
                return instance;
            }
        }

        public void ResetGym(Gym.na_type type)
        {
            for (int i = 0; i < gyms.Length; i++)
            {
                if (gyms[i].NATURE == type)
                {
                    gyms[i] = gyms[i].PartialReset();
                }
            }
        }
        
        public Gym[] GetAllGym
        {
            get { return gyms; }
        }
        public Gym GetTypedGym(Gym.na_type type)
        {
            for(int i = 0; i < gyms.Length; i++)
            {
                if (gyms[i].NATURE == type)
                {
                    return gyms[i];
                }
            }
            return null;
        }
       //For text mmode use
        public int[] GymPosition(int aim)
        {
            int[] target=new int [2];
            target[0] = gyms[aim].Position[0];
            target[1]=  gyms[aim].Position[1];
            return target;
        }    
        //For wpf use
        public int[] GymPositionRaw(int aim)
        {
            int[] target = new int[2];
            target[0] = gyms[aim].PositionRaw[0];
            target[1] = gyms[aim].PositionRaw[1];
            return target;
        }
    }
}
