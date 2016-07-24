using _Project_Geass.Constants;

namespace _Project_Geass.Humanizer
{
    internal class DelayHandler
    {
        private static readonly TickManager MyTicker = new TickManager();
        public static bool Loaded;

        public static void Load()
        {
            MyTicker.AddTick($"{Names.ProjectName}.OnLevel", 125, 175);
            MyTicker.AddTick($"{Names.ProjectName}.UseItems", 75, 125);
            Loaded = true;
        }

        public static bool CheckOnLevel() => MyTicker.CheckTick("GeassLib.OnLevel");

        public static void UseOnLevel() => MyTicker.UseTick("GeassLib.OnLevel");

        public static bool CheckTrinket() => MyTicker.CheckTick("GeassLib.TrinketBuy");

        public static void UseTrinket() => MyTicker.UseTick("GeassLib.TrinketBuy");

        public static bool CheckItems() => MyTicker.CheckTick("GeassLib.Items");

        public static void UseItems() => MyTicker.UseTick("GeassLib.Items");
    }
}