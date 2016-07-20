﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeassBlitz.Data
{
    internal class Level
    {
        private const short E = W + 1;
        private const short Q = 1;
        private const short R = E + 1;
        private const short W = Q + 1;

        public static readonly int[] AbilitySequence ={
            Q,E,W,Q,
            Q,R,Q,E,
            Q,E,R,E,
            E,W,W,R,
             W,W

        };
    }
}