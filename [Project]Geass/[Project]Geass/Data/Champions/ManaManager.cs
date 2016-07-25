namespace _Project_Geass.Data.Champions
{
    class ManaManager
    {

        public static string[] ManaModes = { "Combo", "Mixed", "Clear" };
        public static string[] ManaAbilities = { "Q", "W", "E", "R" };
        //First = Combo
        //Second = Mixed
        //Third = LastHit
        // -1 = N/A
        // Other Numbers = Mana%
        public static int[,] Tristana = { { -1, -1, 25, 15}, { -1, -1, 30, 20}, { -1, -1, 40, -1}};
        public static int[,] Corki = { { 20, -1, 40, 15}, { 30, -1,50, 35}, { 35, -1, 40, 35} };

    }
}
