namespace _Project_Geass.Data.Champions
{
    class AbilitySequences
    {
        private const short E = W + 1;
        private const short Q = 1;
        private const short R = E + 1;
        private const short W = Q + 1;

        public static readonly int[] Corki ={
            Q,E,W,Q,
            Q,R,Q,E,
            Q,E,R,E,
            E,W,W,R,
            W,W
        };

        public static readonly int[] Tristana ={
            E,W,Q,E,
            E,R,E,Q,
            E,Q,R,Q,
            Q,W,W,R,
            W,W
        };
    }
}
