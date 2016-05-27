using LeagueSharp.Common;

namespace Geass_Tristana.Events
{
    internal partial class OrbwalkerEvents
    {
        private bool ComboUseQ()
        {
            if (!SMenu.Item(ManaMenuItemBase + "Use.ManaManager").GetValue<bool>()) return true;
            return SMenu.Item(ManaMenuItemBase + "Combo.Slider.MinMana.Q").GetValue<Slider>().Value <=
                   Champion.GetManaPercent;
        }

        private bool ComboUseE()
        {
            if (!SMenu.Item(ManaMenuItemBase + "Use.ManaManager").GetValue<bool>()) return true;

            return SMenu.Item(ManaMenuItemBase + "Combo.Slider.MinMana.E").GetValue<Slider>().Value <=
                   Champion.GetManaPercent;
        }

        private bool ComboUseR()
        {
            if (!SMenu.Item(ManaMenuItemBase + "Use.ManaManager").GetValue<bool>()) return true;

            return SMenu.Item(ManaMenuItemBase + "Combo.Slider.MinMana.R").GetValue<Slider>().Value <=
                   Champion.GetManaPercent;
        }

        private bool MixedUseQ()
        {
            if (!SMenu.Item(ManaMenuItemBase + "Use.ManaManager").GetValue<bool>()) return true;

            return SMenu.Item(ManaMenuItemBase + "Mixed.Slider.MinMana.Q").GetValue<Slider>().Value <=
                   Champion.GetManaPercent;
        }

        private bool MixedUseE()
        {
            if (!SMenu.Item(ManaMenuItemBase + "Use.ManaManager").GetValue<bool>()) return true;

            return SMenu.Item(ManaMenuItemBase + "Mixed.Slider.MinMana.E").GetValue<Slider>().Value <=
                   Champion.GetManaPercent;
        }

        //private bool MixedUseR()
        //{
        //    if (!SMenu.Item(ManaMenuNameBase + "Use.ManaManager").GetValue<bool>()) return true;

        //    return SMenu.Item(MenuNameBase + "Mixed.Slider.MinMana.R").GetValue<Slider>().Value >
        //           Champion.GetManaPercent;
        //}

        private bool ClearUseQ()
        {
            if (!SMenu.Item(ManaMenuItemBase + "Use.ManaManager").GetValue<bool>()) return true;

            return SMenu.Item(ManaMenuItemBase + "Clear.Slider.MinMana.Q").GetValue<Slider>().Value <=
                   Champion.GetManaPercent;
        }

        private bool ClearUseE()
        {
            if (!SMenu.Item(ManaMenuItemBase + "Use.ManaManager").GetValue<bool>()) return true;

            return SMenu.Item(ManaMenuItemBase + "Clear.Slider.MinMana.E").GetValue<Slider>().Value <=
                   Champion.GetManaPercent;
        }
    }
}
