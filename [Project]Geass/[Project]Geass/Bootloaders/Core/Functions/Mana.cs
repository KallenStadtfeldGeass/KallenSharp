using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Core.Functions
{
    internal class Mana
    {
        #region Combo

        public static bool CheckComboQ()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckComboW()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckComboE()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckComboR()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[0]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        #endregion Combo

        #region Mixed

        public static bool CheckMixedQ()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckMixedW()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckMixedE()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckMixedR()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[1]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        #endregion Mixed

        #region Clear

        public static bool CheckClearQ()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckClearW()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckClearE()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        public static bool CheckClearR()
        {
            if (Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}") ==
                null) return true;

            return Static.Objects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[2]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= Bootloaders.Champions.Base.Champion.GetManaPercent;
        }

        #endregion Clear
    }
}