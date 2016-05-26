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

            if (SMenu.Item(MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(_itemsOffensive.Botrk.Id))
            // If enabled and has item
            {
                if (_itemsOffensive.Botrk.IsReady())
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
                            Items.UseItem(_itemsOffensive.Botrk.Id, target);
                            return;
                        }
                    }
                }
            }

            if (SMenu.Item(MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(_itemsOffensive.Cutless.Id))
            // If enabled and has item
            {
                if (_itemsOffensive.Cutless.IsReady())
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
                            Items.UseItem(_itemsOffensive.Cutless.Id, target);
                            return;
                        }
                    }
                }
            }

            if (SMenu.Item(MenuOffensiveItemBase + "Boolean.Youmuu").GetValue<bool>() && Items.HasItem(_itemsOffensive.GhostBlade.Id))
            // If enabled and has item
            {
                if (_itemsOffensive.GhostBlade.IsReady() &&
                    target.IsValidTarget(Champion.Player.AttackRange + Champion.Player.BoundingRadius))
                // Is ready and target is in auto range
                {
                    if (inCombo)
                    {
                        Items.UseItem(_itemsOffensive.GhostBlade.Id);
                        return;
                    }
                }
            }

            #endregion Offensive

            #region Defensive

            if (SMenu.Item(MenuDefensiveItemBase + "Boolean.QSS").GetValue<bool>() && Items.HasItem(_itemsDefensive.Qss.Id))
            {
                if (SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo ||
                    !SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (_itemsDefensive.Qss.IsReady())
                    {
                        foreach (var buff in Bufftype.Where(buff => SMenu.Item(MenuDefensiveItemBase + "Boolean.QSS." + buff).GetValue<bool>()))
                        {
                            if (Champion.Player.HasBuffOfType(buff))
                                Utility.DelayAction.Add(SMenu.Item(MenuDefensiveItemBase + "Slider.QSS.Delay").GetValue<Slider>().Value, () => Items.UseItem(_itemsDefensive.Qss.Id));
                        }
                    }
                }
            }

            if (SMenu.Item(MenuDefensiveItemBase + "Boolean.Merc").GetValue<bool>() && Items.HasItem(_itemsDefensive.Merc.Id))
            {
                if (SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && inCombo ||
                    !SMenu.Item(MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (_itemsDefensive.Merc.IsReady())
                    {
                        foreach (var buff in Bufftype.Where(buff => SMenu.Item(MenuDefensiveItemBase + "Boolean.Merc." + buff).GetValue<bool>()))
                        {
                            if (Champion.Player.HasBuffOfType(buff))
                                Utility.DelayAction.Add(SMenu.Item(MenuDefensiveItemBase + "Slider.Merc.Delay").GetValue<Slider>().Value, () => Items.UseItem(_itemsDefensive.Qss.Id));
                        }
                    }
                }
            }

            #endregion Defensive
        }

        private readonly Data.Items.Defensive _itemsDefensive = new Data.Items.Defensive();
        private readonly Data.Items.Offensive _itemsOffensive = new Data.Items.Offensive();
    }
}