using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geass_Tristana.Data
{
    internal sealed class Level
    {
        private const short E = W + 1;
        private const short Q = 1;
        private const short R = E + 1;
        private const short W = Q + 1;

        public static readonly int[] AbilitySequence ={
            E,W,Q,E,
            E,R,E,Q,
            E,Q,R,Q,
            Q,W,W,R,
            W,W
        };
    }
}
