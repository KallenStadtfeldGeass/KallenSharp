using LeagueSharp;
using LeagueSharp.Common;


namespace S__Class_Tristana
{
    class Core
    {
        //Holds all the const data
       // public static Data.Monster _Monsters = new Data.Monster();
        public static Libaries.Assembly _Assembly = new Libaries.Assembly();
       

        //Hold Global Data and Fuctions
        public static Libaries.Time _Time = new Libaries.Time();
        public static Libaries.Champion _Champion = new Libaries.Champion(550f, 900f, 625f, 700f);
        public static Humanizer.Delay.Limiter _Limiter = new Humanizer.Delay.Limiter();

        //Menu 
        public static readonly string MenuName = _Assembly.GetName();
        public static Menu SMenu { get; set; } = new Menu(MenuName, MenuName, true);

        //Global 
        public static Obj_AI_Hero Player;
        public static Orbwalking.Orbwalker CommonOrbwalker { get; set; }


        public static readonly BuffType[] Bufftype =
         {
            BuffType.Snare,
            BuffType.Blind,
            BuffType.Charm,
            BuffType.Stun,
            BuffType.Fear,
            BuffType.Slow,
            BuffType.Taunt,
            BuffType.Suppression
        };

    }
}
