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
            MyTicker.AddTick($"{Names.ProjectName}.OnLevel", 125, 175);
            MyTicker.AddTick($"{Names.ProjectName}.UseItems", 75, 125);
            Loaded = true;
        }

        /// <summary>
        /// Checks the on level.
        /// </summary>
        /// <returns></returns>
        public static bool CheckOnLevel() => MyTicker.CheckTick("GeassLib.OnLevel");

        /// <summary>
        /// Uses the on level.
        /// </summary>
        public static void UseOnLevel() => MyTicker.UseTick("GeassLib.OnLevel");

        /// <summary>
        /// Checks the trinket.
        /// </summary>
        /// <returns></returns>
        public static bool CheckTrinket() => MyTicker.CheckTick("GeassLib.TrinketBuy");

        /// <summary>
        /// Uses the trinket.
        /// </summary>
        public static void UseTrinket() => MyTicker.UseTick("GeassLib.TrinketBuy");

        /// <summary>
        /// Checks the items.
        /// </summary>
        /// <returns></returns>
        public static bool CheckItems() => MyTicker.CheckTick("GeassLib.Items");

        /// <summary>
        /// Uses the items.
        /// </summary>
        public static void UseItems() => MyTicker.UseTick("GeassLib.Items");
    }
}