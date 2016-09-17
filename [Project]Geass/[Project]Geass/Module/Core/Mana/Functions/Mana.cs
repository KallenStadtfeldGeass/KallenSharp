using _Project_Geass.Data;
using LeagueSharp.Common;
using _Project_Geass.Functions;
using _Project_Geass.Global;
using _Project_Geass.Global.Data;

namespace _Project_Geass.Module.Core.Mana.Functions
{
    internal class Mana
    {
        #region Combo

        public bool Enabled;
        //
        public int ManaPercent => (int)(StaticObjects.Player.Mana / StaticObjects.Player.MaxMana * 100);
        public int HealthPercent => (int)(StaticObjects.Player.Health / StaticObjects.Player.MaxHealth * 100);

        public Spell Q { get; set; }
        public Spell W { get; set; }
        public Spell E { get; set; }
        public Spell R { get; set; }

        public Mana(Spell q, Spell w, Spell e, Spell r,bool enabled)
        {
            Q = q;
            W = w;
            E = e;
            R = r;
            Enabled = enabled;
        }

        public bool CheckComboQ()
        {
            if (!Q.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[0]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckComboW()
        {
            if (!W.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[1]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckComboE()
        {
            if (!E.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[2]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckComboR()
        {
            if (!R.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[3]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[0]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        #endregion Combo

        #region Mixed

        public bool CheckMixedQ()
        {
            if (!Q.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[0]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckMixedW()
        {
            if (!W.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[1]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckMixedE()
        {
            if (!E.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[2]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckMixedR()
        {
            if (!R.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[3]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[1]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        #endregion Mixed

        #region Clear

        public bool CheckClearQ()
        {
            if (!Q.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[0]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[0]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckClearW()
        {
            if (!W.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[1]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[1]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckClearE()
        {
            if (!E.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[2]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[2]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        public bool CheckClearR()
        {
            if (!R.IsReady()) return false;
            if (!Enabled) return true;
            if (StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[3]}") ==
                null) return true;

            return StaticObjects.ProjectMenu.Item(
                $"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[2]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[3]}")
                .GetValue<Slider>()
                .Value <= ManaPercent;
        }

        #endregion Clear
    }
}