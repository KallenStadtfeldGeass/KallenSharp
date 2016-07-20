using LeagueSharp;
using LeagueSharp.Common;

namespace GeassBlitz.Misc
{
    class Core
    {

        private const string AssemblyName = "Geass Blitzcrank [B]";

        //Global External Classes and Variables
        public static Orbwalking.Orbwalker CommonOrbwalker { get; set; }
        public static Damage DamageLib = new Damage();
        public static Menu SMenu { get; set; } = new Menu(AssemblyName, AssemblyName, true);
        public static GeassLib.Functions.Logging.Logger Logger = new GeassLib.Functions.Logging.Logger("Geass Blitz");
       
        //Hold Global Data and Functions
        public static Libaries.Champion Champion = new Libaries.Champion();

        public static GeassLib.Humanizer.TickManager TickManager = new GeassLib.Humanizer.TickManager();

        public static readonly BuffType[] Bufftype = GeassLib.Data.Buffs.GetTypes;
    }
}
