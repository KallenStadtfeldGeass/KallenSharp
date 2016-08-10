using _Project_Geass.Data;

namespace _Project_Geass.Humanizer
{
    internal class DelayHandler
    {
        private static readonly TickManager MyTicker = new TickManager();

        /// <summary>
        /// Loads the specified humanize.
        /// </summary>
        /// <param name="humanize">if set to <c>true</c> [humanize].</param>
        public static void Load(bool humanize)
        {
            var offset = 0;
            if (humanize)
                offset = 100;

            MyTicker.AddTick($"{Names.ProjectName}.OnLevel", 50 + offset, 75 + offset);
            MyTicker.AddTick($"{Names.ProjectName}.UseItems", offset, 25 + offset);
            MyTicker.AddTick($"{Names.ProjectName}.TrinketBuy", 50 + offset, 100 + offset);
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