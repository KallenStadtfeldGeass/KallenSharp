using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Structure
{
    sealed class Level
    { 
        const short Q = 1;
        const short W = Q+1;
        const short E = W+1;
        const short R = E+1;


        public readonly int[] AbilitySequence ={
            E,W,Q,E,
            E,R,E,Q,
            E,Q,R,Q,
            Q,W,W,R,
            W,W
        };
 
   
    }

}
