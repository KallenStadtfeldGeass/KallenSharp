using _Project_Geass.Data;

namespace _Project_Geass.Humanizer
{
    internal class DelayHandler
    {
        private static readonly TickManager MyTicker = new TickManager();
        public static bool Loaded;

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load()
        {
            if (Loaded) return;
            MyTicker.AddTick($"{Names.ProjectName}.OnLevel", 150, 200);
            MyTicker.AddTick($"{Names.ProjectName}.UseItems", 75, 125);
            MyTicker.AddTick($"{Names.ProjectName}.TrinketBuy", 200, 275);
            Loaded = true;
        }

        /// <summary>
        /// Checks the on level.
        /// </summary>
        /// <returns></returns>
        public static bool CheckOnLevel() => MyTicker.CheckTick($"{Names.ProjectName}.OnLevel");

        /// <summary>
        /// Uses the on level.
        /// </summary>
        public static void UseOnLevel() => MyTicker.UseTick($"{Names.ProjectName}.OnLevel");

        /// <summary>
        /// Checks the trinket.
        /// </summary>
        /// <returns></returns>
        public static bool CheckTrinket() => MyTicker.CheckTick($"{Names.ProjectName}.TrinketBuy");

        /// <summary>
        /// Uses the trinket.
        /// </summary>
        public static void UseTrinket() => MyTicker.UseTick($"{Names.ProjectName}.TrinketBuy");

        /// <summary>
        /// Checks the items.
        /// </summary>
        /// <returns></returns>
        public static bool CheckItems() => MyTicker.CheckTick($"{Names.ProjectName}.UseItems");

        /// <summary>
        /// Uses the items.
        /// </summary>
        public static void UseItems() => MyTicker.UseTick($"{Names.ProjectName}.UseItems");
    }
}