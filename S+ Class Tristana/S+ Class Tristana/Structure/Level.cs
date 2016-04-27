using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Structure
{
    class Level
    {
        public struct Abilitys
        {
            public const int Q = 1;
            public const int W = 2;
            public const int E = 3;
            public const int R = 4;
        }

        public readonly int[] AbilitySequence ={
            Abilitys.E,Abilitys.W,Abilitys.Q,Abilitys.E,
            Abilitys.E,Abilitys.R,Abilitys.E,Abilitys.Q,
            Abilitys.E,Abilitys.Q,Abilitys.R,Abilitys.Q,
            Abilitys.Q,Abilitys.W,Abilitys.W,Abilitys.R,
            Abilitys.W,Abilitys.W
        };
 
   
    }

}
