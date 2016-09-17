namespace _Project_Geass.Data.Champions
{
    internal class SettingsBase
    {
        public static string[] ManaModes = { "Combo", "Mixed", "Clear" };
        public static string[] ManaAbilities = { $"Q", $"W", $"E", $"R" };

        public const short E = W + 1;
        public const short Q = 1;
        public const short R = E + 1;
        public const short W = Q + 1;
    }
}