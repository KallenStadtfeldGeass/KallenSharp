using Geass_Tristana.Other;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

namespace Geass_Tristana.Events
{
    internal class ItemEvents : Core
    {
        public const string MenuDefensiveItemBase = MenuItemBase + ".Defensive.";

        public const string MenuDefensiveNameBase = ".Defensive Menu";
        public const string MenuItemBase = ".Item.";
        public const string MenuNameBase = ".Item Menu";
        public const string MenuOffensiveItemBase = MenuItemBase + ".Offensive.";

        public const string MenuOffensiveNameBase = ".Offensive Menu";

        public void After_Attack(AttackableUnit unit, AttackableUnit target)
        {
        }

        public void Before_Attack(Orbwalking.BeforeAttackEventArgs args)
        {
        }

        public void OnUpdate(EventArgs args)
        {
            #region Offensive

            if (!TickManager.CheckTick($"{HumanizeEvents.ItemBase}Slider.ItemDelay")) return;

            TickManager.UseTick($"{HumanizeEvents.ItemBase}Slider.ItemDelay");

            var target = TargetSelector.GetTarget(1500, TargetSelector.DamageType.Physical);
            if (target == null) return;

            var inCombo = CommonOrbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo;

            if (SMenu.Item(MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(GeassLib.Data.Items.Offensive.Botrk.Id))
            // If enabled and has item
            {
                if (GeassLib.Data.Items.Offensive.Botrk.IsReady())
                {
                    if (
                        target.IsValidTarget(Champion.Player.AttackRange + Champion.Player.BoundingRadius) || Champion.Player.HealthPercent < SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                        if (SMenu.Item(MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo &&
                            target.HealthPercent < SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MaxHp").GetValue<Slider>().Value
                            //in combo and target hp less then
                            ||
                            !SMenu.Item(MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() &&
                            target.HealthPercent < SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MinHp").GetValue<Slider>().Value
                            //not in combo but target HP less then
                            ||
                            (Champion.Player.HealthPercent <
                             SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        //Player hp less then
                        {
                            Libaries.Logger.Write($"Use Bork on {target}");
                            Items.UseItem(GeassLib.Data.Items.Offensive.Botrk.Id, target);
                            return;
                        }
                    }
                }
            }

            if (SMenu.Item(MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(GeassLib.Data.Items.Offensive.Cutless.Id))
            // If enabled and has item
            {
                if (GeassLib.Data.Items.Offensive.Cutless.IsReady())
                {
                    if (
                        target.IsValidTarget(Champion.Player.AttackRange +
                                           Champion.Player.BoundingRadius) ||
                        Champion.Player.HealthPercent <
                       SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                        if (SMenu.Item(MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo &&
                            target.HealthPercent <
                            SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MaxHp").GetValue<Slider>().Value
                            //in combo and target hp less then
                            ||
                            !SMenu.Item(MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() &&
                            target.HealthPercent <
                            SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MinHp").GetValue<Slider>().Value
                            //not in combo but target HP less then
                            ||
                            (Champion.Player.HealthPercent <
                             SMenu.Item(MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        //Player hp less then
                        {

                            Libaries.Logger.Write($"Use Cutless on {target}");
                            Items.UseItem(GeassLib.Data.Items.Offensive.Cutless.Id, target);
                            return;
                        }
                    }
                }
            }

            if (SMenu.Item(MenuOffensiveItemBase + "Boolean.Youmuu").GetValue<bool>() && Items.HasItem(GeassLib.Data.Items.Offensive.GhostBlade.Id))
            // If enabled and has item
            {
                if (GeassLib.Data.Items.Offensive.GhostBlade.IsReady() &&
                    target.IsValidTarget(Champion.Player.AttackRange + Champion.Player.BoundingRadius))
                // Is ready and target is in auto range
                {
                    if (inCombo)
                    {

                        Libaries.Logger.Write($"Use Ghostblade on {target}");
                        Items.UseItem(GeassLib.Data.Items.Offensive.GhostBlade.Id);
                        return;
                    }
                }
            }

            #endregion Offensive

            #region Defensive

            if (SMenu.Item(MenuDefensiveItemBase + "Boolean.QSS").GetValue<bool>() && Items.HasItem(GeassLib.Data.Items.Defensive.Qss.Id))
            {
                if (SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo ||
                    !SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (GeassLib.Data.Items.Defensive.Qss.IsReady())
                    {
                        foreach (var buff in Bufftype.Where(buff => SMenu.Item(MenuDefensiveItemBase + "Boolean.QSS." + buff).GetValue<bool>()))
                        {
                            if (Champion.Player.HasBuffOfType(buff))
                            {

                                Libaries.Logger.Write($"Use QSS Reason {buff}");
                                Utility.DelayAction.Add(
                                    SMenu.Item(MenuDefensiveItemBase + "Slider.QSS.Delay").GetValue<Slider>().Value,
                                    () => Items.UseItem(GeassLib.Data.Items.Defensive.Qss.Id));

                            }
                        }
                    }
                }
            }

            if (SMenu.Item(MenuDefensiveItemBase + "Boolean.Merc").GetValue<bool>() && Items.HasItem(GeassLib.Data.Items.Defensive.Merc.Id))
            {
                if (SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo ||
                    !SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (GeassLib.Data.Items.Defensive.Merc.IsReady())
                    {
                        foreach (var buff in Bufftype.Where(buff => SMenu.Item(MenuDefensiveItemBase + "Boolean.Merc." + buff).GetValue<bool>()))
                        {
                            if (Champion.Player.HasBuffOfType(buff))
                            {
                                Libaries.Logger.Write($"Use Merc Reason {buff}");
                                Utility.DelayAction.Add(
                                    SMenu.Item(MenuDefensiveItemBase + "Slider.Merc.Delay").GetValue<Slider>().Value,
                                    () => Items.UseItem(GeassLib.Data.Items.Defensive.Qss.Id));

                            }
                        }
                    }
                }
            }

            #endregion Defensive
        }

   
    }
}