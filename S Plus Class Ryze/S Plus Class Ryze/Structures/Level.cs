﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Plus_Class_Ryze.Structures
{
    internal class Level
    {
        public struct Abilitys // So you can refeer to spell to level by slot rather than 1,2,3,4
        {
            public const int Q = 1;
            public const int W = 2;
            public const int E = 3;
            public const int R = 4;
        }

        public static readonly int[] Sequence =
        {
            Abilitys.Q, Abilitys.W, Abilitys.E, Abilitys.Q, Abilitys.Q, Abilitys.R, Abilitys.Q, Abilitys.W, Abilitys.Q,
            Abilitys.W, Abilitys.R, Abilitys.E, Abilitys.W, Abilitys.W, Abilitys.E, Abilitys.R, Abilitys.E, Abilitys.E
        };

    }
}
