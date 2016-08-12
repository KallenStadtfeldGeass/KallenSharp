using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace _Project_Geass.Bootloaders.Core.Functions
{
    internal class MenuOptions
    {
        public static bool HumanizerEnabled()
            => Static.Objects.ProjectMenu.Item($"{Names.Menu.BaseItem}.Humanizer").GetValue<bool>();

        public static bool DrawingOnSelfEnabled()
            =>
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                ".Boolean.DrawOnSelf").GetValue<bool>();

        public static bool DrawQRangeEnabled()
            =>
                DrawingOnSelfEnabled() &&
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.QColor")
                    .GetValue<Circle>()
                    .Active;

        public static Color DrawQRangeColor()
            =>

                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.QColor")
                    .GetValue<Circle>()
                    .Color;

        public static bool DrawWRangeEnabled()
            =>
                DrawingOnSelfEnabled() &&
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.WColor")
                    .GetValue<Circle>()
                    .Active;

        public static Color DrawWRangeColor()
            =>

                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.WColor")
                    .GetValue<Circle>()
                    .Color;

        public static bool DrawERangeEnabled()
            =>
                DrawingOnSelfEnabled() &&
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.EColor")
                    .GetValue<Circle>()
                    .Active;

        public static Color DrawERangeColor()
    =>
         Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.EColor")
            .GetValue<Circle>()
            .Color;

        public static bool DrawRRangeEnabled()
            =>
                DrawingOnSelfEnabled() &&
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.RColor")
                    .GetValue<Circle>()
                    .Active;

        public static Color DrawRRangeColor()
    =>
        Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.RColor")
            .GetValue<Circle>()
            .Color;

        public static bool DrawingOnEnemyEnabled()
            =>
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                ".Boolean.DrawOnEnemy").GetValue<bool>();

        public static bool DrawingDamageEnabled()
            => DrawingOnEnemyEnabled() &&
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                ".Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Active;
    }
}