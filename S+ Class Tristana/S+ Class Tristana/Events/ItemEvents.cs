using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Events
{
    class ItemEvents : Core
    {
        public const string _MenuNameBase = ".Item Menu";
        public const string _MenuItemBase = ".Item.";

        public const string _MenuOffensiveNameBase = ".Offensive Menu";
        public const string _MenuOffensiveItemBase = _MenuItemBase + ".Offensive.";

        public const string _MenuDefensiveNameBase = ".Defensive Menu";
        public const string _MenuDefensiveItemBase = _MenuItemBase + ".Defensive.";

        public void OnUpdate(EventArgs args)
        {
            #region Offensive

            if (!Humanizer.Delay.Limiter.CheckDelay($"{Humanizer.Delay.DelayItemBase}Slider.ItemDelay")) return;

            Humanizer.Delay.Limiter.UseTick($"{Humanizer.Delay.DelayItemBase}Slider.ItemDelay");

            var target = TargetSelector.GetTarget(1500, TargetSelector.DamageType.Physical);
            if (target == null) return;

            var inCombo = CommonOrbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo;

            if (SMenu.Item(_MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(Items_Offensive.Botrk.Id))
            // If enabled and has item
            {
                if (Items_Offensive.Botrk.IsReady())
                {
                    if (
                        target.IsValidTarget(Player.AttackRange + Player.BoundingRadius) || Player.HealthPercent < SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                        if (SMenu.Item(_MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo &&
                            target.HealthPercent < SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MaxHp").GetValue<Slider>().Value
                            //in combo and target hp less then
                            ||
                            !SMenu.Item(_MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() &&
                            target.HealthPercent < SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MinHp").GetValue<Slider>().Value
                            //not in combo but target HP less then
                            ||
                            (Player.HealthPercent <
                             SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        //Player hp less then
                        {
                            Items.UseItem(Items_Offensive.Botrk.Id, target);
                            return;
                        }

                    }
                }
            }

            if (SMenu.Item(_MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(Items_Offensive.Cutless.Id))
            // If enabled and has item
            {
                if (Items_Offensive.Cutless.IsReady())
                {
                    if (
                        target.IsValidTarget(Player.AttackRange +
                                           Player.BoundingRadius) ||
                        Player.HealthPercent <
                       SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                        if (SMenu.Item(_MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo &&
                            target.HealthPercent <
                            SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MaxHp").GetValue<Slider>().Value
                            //in combo and target hp less then
                            ||
                            !SMenu.Item(_MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() &&
                            target.HealthPercent <
                            SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MinHp").GetValue<Slider>().Value
                            //not in combo but target HP less then
                            ||
                            (Player.HealthPercent <
                             SMenu.Item(_MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        //Player hp less then
                        {
                            Items.UseItem(Items_Offensive.Cutless.Id, target);
                            return;
                        }
                    }
                }
            }

            if (SMenu.Item(_MenuOffensiveItemBase + "Boolean.Youmuu").GetValue<bool>() && Items.HasItem(Items_Offensive.GhostBlade.Id))
            // If enabled and has item
            {
                if (Items_Offensive.GhostBlade.IsReady() &&
                    target.IsValidTarget(Player.AttackRange + Player.BoundingRadius))
                // Is ready and target is in auto range 
                {
                    if (inCombo)
                    {
                        Items.UseItem(Items_Offensive.GhostBlade.Id);
                        return;
                    }
                }
            }

            #endregion

            #region Defensive

            if (SMenu.Item(_MenuDefensiveItemBase + "Boolean.QSS").GetValue<bool>() && Items.HasItem(Items_Defensive.Qss.Id))
            {
                if (SMenu.Item(_MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo ||
                    !SMenu.Item(_MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (Items_Defensive.Qss.IsReady())
                    {

                        foreach (var buff in Bufftype.Where(buff => SMenu.Item(_MenuDefensiveItemBase + "Boolean.QSS." + buff).GetValue<bool>()))
                        {
                            if (Player.HasBuffOfType(buff))
                                Utility.DelayAction.Add(SMenu.Item(_MenuDefensiveItemBase + "Slider.QSS.Delay").GetValue<Slider>().Value, () => Items.UseItem(Items_Defensive.Qss.Id));

                        }
                    }
                }
            }


            if (SMenu.Item(_MenuDefensiveItemBase + "Boolean.Merc").GetValue<bool>() && Items.HasItem(Items_Defensive.Merc.Id))
            {
                if (SMenu.Item(_MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo ||
                    !SMenu.Item(_MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (Items_Defensive.Merc.IsReady())
                    {
                        foreach (var buff in Bufftype.Where(buff => SMenu.Item(_MenuDefensiveItemBase + "Boolean.Merc." + buff).GetValue<bool>()))
                        {
                            if (Player.HasBuffOfType(buff))
                                Utility.DelayAction.Add(SMenu.Item(_MenuDefensiveItemBase + "Slider.Merc.Delay").GetValue<Slider>().Value, () => Items.UseItem(Items_Defensive.Qss.Id));

                        }
                    }
                }
            }

            #endregion
        }

        public static void Before_Attack(Orbwalking.BeforeAttackEventArgs args)
        {

        }

        public static void After_Attack(AttackableUnit unit, AttackableUnit target)
        {

        }

    }
}
