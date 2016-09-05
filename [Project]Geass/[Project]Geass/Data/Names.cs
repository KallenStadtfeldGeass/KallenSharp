namespace _Project_Geass.Data
{
    internal static class Names
    {
        public static readonly string[] ChampionBundled = {
            "Ashe",
            "Ezreal",
            "Tristana",
            "Kalista"
        };

        public static readonly string ProjectName = "[Project]Geass";
        public static readonly string SettingsName = "[Project]Geass Modules";

        public static class Menu
        {
            public static string BaseItem = "MenuSettings.";
            public static string BaseName = "Menu Settings";

            public static string LevelItemBase = "Level.";
            public static string LevelNameBase = "On Level";

            public static string TrinketItemBase = "Trinket.";
            public static string TrinketNameBase = "Trinket";

            public static string DrawingItemBase = "Drawing.";
            public static string DrawingNameBase = $"Drawing";

            public static string LastHitHelperItemBase = "LastHitHelper.";
            public static string LastHitHelperNameBase = "LastHit Helper";

            public static string ManaItemBase = "Mana.";
            public static string ManaNameBase = "Mana";

            public static string MenuDefensiveItemBase = ItemNameBase + "Defensive.";
            public static string MenuDefensiveNameBase = "Defensive";
            public static string ItemNameBase = "Item.";
            public static string ItemMenuBase = "Items";
            public static string MenuOffensiveItemBase = ItemNameBase + "Offensive.";
            public static string MenuOffensiveNameBase = "Offensive";
        }
    }
}