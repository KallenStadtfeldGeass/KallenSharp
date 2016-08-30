using _Project_Geass.Globals;

namespace _Project_Geass.Data.Champions
{
    internal class AioChampion : Base
    {
        public int[] GetAbilities { get; set; }

        public int[,] GetManaSettings { get; set; }

        public AioChampion()
        {
            switch (Static.Objects.Player.ChampionName)
            {
                case "Tristana":
                    GetAbilities = new int[]
                    {
                        E, W, Q, E,
                        E, R, E, Q,
                        E, Q, R, Q,
                        Q, W, W, R,
                        W, W
                    };

                    GetManaSettings = new[,] { { -1, -1, 35, 25 }, { -1, -1, 35, -1 }, { -1, -1, 50, -1 } };
                    break;
                case "Ezreal":
                    GetAbilities = new int[]
                    {
                        Q, E, W, Q,
                        Q, R, Q, E,
                        Q, E, R, E,
                        E, W, W, R,
                        W, W
                    };

                    GetManaSettings = new[,] { { 20, 30, -1, 35 }, { 30, 40, -1, 15 }, { 50, -1, -1, -1 } };
                    break;
                case "Ashe":
                    GetAbilities = new int[]
                    {
                        W,Q,W,E,
                        W,R,W,Q,
                        W,Q,R,Q,
                        Q,E,E,R,
                        E,E
                    };

                    GetManaSettings = new[,] { { 25, 30, -1, 30 }, { 25, 35, -1, 35 }, { 40, 65, -1, -1 } };
                    break;
            }
        }
    }
}