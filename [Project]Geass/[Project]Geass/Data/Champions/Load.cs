using _Project_Geass.Bootloaders.Champions;
using _Project_Geass.Globals;

namespace _Project_Geass.Data.Champions
{
    internal class Load : Base
    {
        public int[] GetAbilities { get; set; }
        public bool[] GetDrawing { get; set; }
        public int[,] GetManaSettings { get; set; }

        public Load()
        {
            switch (Static.Objects.Player.ChampionName)
            {
                case nameof(Tristana):
                    GetAbilities = new int[]
                    {
                       E,W,Q,E,
                       E,R,E,Q,
                       E,Q,R,Q,
                       Q,W,W,R,
                       W,W
                    };

                    GetManaSettings = new[,] { { -1, -1, 35, 25 }, { -1, -1, 35, -1 }, { -1, -1, 50, -1 } };
                    GetDrawing = new[] { false, false, true, true };
                    // ReSharper disable once UnusedVariable
                    var a = new Tristana();
                    break;

                case nameof(Ezreal):
                    GetAbilities = new int[]
                    {
                        Q,E,W,Q,
                        Q,R,Q,E,
                        Q,E,R,E,
                        E,W,W,R,
                        W,W
                    };

                    GetManaSettings = new[,] { { 20, 30, -1, 35 }, { 30, 40, -1, 15 }, { 50, -1, -1, -1 } };
                    GetDrawing = new[] { true, true, false, false };
                    // ReSharper disable once UnusedVariable
                    var b = new Ezreal();
                    break;

                case nameof(Ashe):
                    GetAbilities = new int[]
                    {
                        W,Q,W,E,
                        W,R,W,Q,
                        W,Q,R,Q,
                        Q,E,E,R,
                        E,E
                    };

                    GetManaSettings = new[,] { { 25, 30, -1, 30 }, { 25, 35, -1, 35 }, { 40, 65, -1, -1 } };
                    GetDrawing = new[] { false, true, false, true };
                    // ReSharper disable once UnusedVariable
                    var c = new Ashe();
                    break;


                case nameof(Kalista):
                    GetAbilities = new int[]
                    {
                        E,W,Q,E,
                        E,R,E,Q,
                        E,Q,R,Q,
                        Q,W,W,R,
                        W,W
                    };

                    GetManaSettings = new[,] { { 40, -1, 25, 15 }, { -1, -1, 45, -1 }, { -1, -1, 60, -1 } };
                    GetDrawing = new[] { true, false, true, true };
                    // ReSharper disable once UnusedVariable
                    var d = new Kalista();
                    break;

            }
        }
    }
}