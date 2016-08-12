using _Project_Geass.Data;
using _Project_Geass.Globals;
using _Project_Geass.Humanizer;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

namespace _Project_Geass.Bootloaders.Core.Events.OnUpdate
{
    internal class ItemEvents
    {
        public ItemEvents()
        {
            Orbwalking.AfterAttack += After_Attack;
            Orbwalking.BeforeAttack += Before_Attack;
            Game.OnUpdate += OnUpdate;
        }

        private void After_Attack(AttackableUnit unit, AttackableUnit target)
        {
        }

        private void Before_Attack(Orbwalking.BeforeAttackEventArgs args)
        {
        }

        public void OnUpdate(EventArgs args)
        {
            #region Offensive

            if (!DelayHandler.CheckItems()) return;

            DelayHandler.UseItems();
            var target = TargetSelector.GetTarget(1500, TargetSelector.DamageType.Physical);
            if (target == null) return;

            if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(Data.Items.Offensive.Botrk.Id))
            // If enabled and has item
            {
                if (Data.Items.Offensive.Botrk.IsReady())
                {
                    if (
                        target.IsValidTarget(Static.Objects.Player.AttackRange + Static.Objects.Player.BoundingRadius) || Static.Objects.Player.HealthPercent < Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                        if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && Static.Variables.InCombo &&
                            target.HealthPercent < Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MaxHp").GetValue<Slider>().Value
                            //in combo and target hp less then
                            ||
                            !Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() &&
                            target.HealthPercent < Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MinHp").GetValue<Slider>().Value
                            //not in combo but target HP less then
                            ||
                            (Static.Objects.Player.HealthPercent <
                             Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveNameBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        //Player hp less then
                        {
                            Static.Objects.ProjectLogger.WriteLog($"Use Bork on {target}");
                            Items.UseItem(Data.Items.Offensive.Botrk.Id, target);
                            return;
                        }
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Boolean.Bork").GetValue<bool>() && Items.HasItem(Data.Items.Offensive.Cutless.Id))
            // If enabled and has item
            {
                if (Data.Items.Offensive.Cutless.IsReady())
                {
                    if (
                        target.IsValidTarget(Static.Objects.Player.AttackRange +
                                           Static.Objects.Player.BoundingRadius) ||
                        Static.Objects.Player.HealthPercent <
                       Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                        if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && Static.Variables.InCombo &&
                            target.HealthPercent <
                            Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MaxHp").GetValue<Slider>().Value
                            //in combo and target hp less then
                            ||
                            !Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() &&
                            target.HealthPercent <
                            Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MinHp").GetValue<Slider>().Value
                            //not in combo but target HP less then
                            ||
                            (Static.Objects.Player.HealthPercent <
                             Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        //Player hp less then
                        {
                            Static.Objects.ProjectLogger.WriteLog($"Use Cutless on {target}");
                            Items.UseItem(Data.Items.Offensive.Cutless.Id, target);
                            return;
                        }
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase + "Boolean.Youmuu").GetValue<bool>() && Items.HasItem(Data.Items.Offensive.GhostBlade.Id))
            // If enabled and has item
            {
                if (Data.Items.Offensive.GhostBlade.IsReady() &&
                    target.IsValidTarget(Static.Objects.Player.AttackRange + Static.Objects.Player.BoundingRadius))
                // Is ready and target is in auto range
                {
                    if (Static.Variables.InCombo)
                    {
                        Static.Objects.ProjectLogger.WriteLog($"Use Ghostblade on {target}");
                        Items.UseItem(Data.Items.Offensive.GhostBlade.Id);
                        return;
                    }
                }
            }

            #endregion Offensive

            #region Defensive

            if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.QSS").GetValue<bool>() && Items.HasItem(Data.Items.Defensive.Qss.Id))
            {
                if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && Static.Variables.InCombo ||
                    !Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (Data.Items.Defensive.Qss.IsReady())
                    {
                        foreach (var buff in Data.Buffs.GetTypes.Where(buff => Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.QSS." + buff).GetValue<bool>()))
                        {
                            if (Static.Objects.Player.HasBuffOfType(buff))
                            {
                                Static.Objects.ProjectLogger.WriteLog($"Use QSS Reason {buff}");
                                Utility.DelayAction.Add(
                                    Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Slider.QSS.Delay").GetValue<Slider>().Value,
                                    () => Items.UseItem(Data.Items.Defensive.Qss.Id));
                            }
                        }
                    }
                }
            }

            // ReSharper disable once RedundantNameQualifier
            if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.Merc").GetValue<bool>() && LeagueSharp.Common.Items.HasItem(Data.Items.Defensive.Merc.Id))
            {
                if (Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>() && Static.Variables.InCombo ||
                    !Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.ComboOnly").GetValue<bool>())
                {
                    if (Data.Items.Defensive.Merc.IsReady())
                    {
                        foreach (var buff in Data.Buffs.GetTypes.Where(buff => Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Boolean.Merc." + buff).GetValue<bool>()))
                        {
                            if (Static.Objects.Player.HasBuffOfType(buff))
                            {
                                Static.Objects.ProjectLogger.WriteLog($"Use Merc Reason {buff}");
                                Utility.DelayAction.Add(
                                    Static.Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase + "Slider.Merc.Delay").GetValue<Slider>().Value,
                                    () => Items.UseItem(Data.Items.Defensive.Qss.Id));
                            }
                        }
                    }
                }
            }

            #endregion Defensive
        }
    }
}