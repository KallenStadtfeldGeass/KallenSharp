using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Core.Functions
{
    internal class Mana
    {
        #region Combo

        public static int GetManaPercent => (int)(Static.Objects.Player.Mana / Static.Objects.Player.MaxMana * 100);
        public static int HealthPercent => (int)(Static.Objects.Player.Health / Static.Objects.Player.MaxHealth * 100);

        public static bool CheckComboQ()
        {
            if (!Champions.Base.Champion.Q.IsReady()) return false;

            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckComboW()
        {
            if (!Champions.Base.Champion.W.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckComboE()
        {
            if (!Champions.Base.Champion.E.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckComboR()
        {
            if (!Champions.Base.Champion.R.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        #endregion Combo

        #region Mixed

        public static bool CheckMixedQ()
        {
            if (!Champions.Base.Champion.Q.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckMixedW()
        {
            if (!Champions.Base.Champion.W.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckMixedE()
        {
            if (!Champions.Base.Champion.E.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckMixedR()
        {
            if (!Champions.Base.Champion.R.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        #endregion Mixed

        #region Clear

        public static bool CheckClearQ()
        {
            if (!Champions.Base.Champion.Q.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckClearW()
        {
            if (!Champions.Base.Champion.W.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckClearE()
        {
            if (!Champions.Base.Champion.E.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        public static bool CheckClearR()
        {
            if (!Champions.Base.Champion.R.IsReady()) return false;
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= GetManaPercent;
        }

        #endregion Clear
    }
}