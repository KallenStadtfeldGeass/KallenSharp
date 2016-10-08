using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using _Project_Geass.Data.Constant.Items;
using _Project_Geass.Data.Static;
using _Project_Geass.Tick;

namespace _Project_Geass.Module.Core.Items.Events
{

    internal class Item
    {
        #region Public Fields

        public Orbwalking.Orbwalker Orbwalker;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        /// <param name="orbwalker">
        ///     The orbwalker.
        /// </param>
        public Item(Orbwalking.Orbwalker orbwalker)
        {
            Orbwalker=orbwalker;
            _offensive=new Offensive();
            _defensive=new Defensive();
            Orbwalking.AfterAttack+=After_Attack;
            Orbwalking.BeforeAttack+=Before_Attack;
            Game.OnUpdate+=OnUpdate;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Raises the <see cref="E:Update" /> event.
        /// </summary>
        /// <param name="args">
        ///     The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        public void OnUpdate(EventArgs args)
        {
            #region Offensive

            if (!Handler.CheckItems())
                return;

            Handler.UseItems();
            var target=TargetSelector.GetTarget(1500, TargetSelector.DamageType.Physical);
            if (target==null)
                return;

            if (Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Boolean.Bork").GetValue<bool>()&&LeagueSharp.Common.Items.HasItem(_offensive.Botrk.Id))
                // If enabled and has item
                if (_offensive.Botrk.IsReady())
                    if (target.IsValidTarget(Objects.Player.AttackRange+Objects.Player.BoundingRadius)||(Objects.Player.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        if ((Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Boolean.ComboOnly").GetValue<bool>()&&(Orbwalker.ActiveMode==Orbwalking.OrbwalkingMode.Combo)
                             &&(target.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Slider.Bork.MaxHp").GetValue<Slider>().Value))
                            //in combo and target hp less then
                            ||(!Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Boolean.ComboOnly").GetValue<bool>()
                               &&(target.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Slider.Bork.MinHp").GetValue<Slider>().Value))
                            //not in combo but target HP less then
                            ||(Objects.Player.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveNameBase+"Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                            //Player hp less then
                        {
                            Objects.ProjectLogger.WriteLog($"Use Bork on {target}");
                            LeagueSharp.Common.Items.UseItem(_offensive.Botrk.Id, target);
                            return;
                        }

            if (Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Boolean.Bork").GetValue<bool>()&&LeagueSharp.Common.Items.HasItem(_offensive.Cutless.Id))
                // If enabled and has item
                if (_offensive.Cutless.IsReady())
                    if (target.IsValidTarget(Objects.Player.AttackRange+Objects.Player.BoundingRadius)||(Objects.Player.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                        if ((Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Boolean.ComboOnly").GetValue<bool>()&&(Orbwalker.ActiveMode==Orbwalking.OrbwalkingMode.Combo)
                             &&(target.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Slider.Bork.MaxHp").GetValue<Slider>().Value))
                            //in combo and target hp less then
                            ||(!Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Boolean.ComboOnly").GetValue<bool>()
                               &&(target.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Slider.Bork.MinHp").GetValue<Slider>().Value))
                            //not in combo but target HP less then
                            ||(Objects.Player.HealthPercent<Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Slider.Bork.MinHp.Player").GetValue<Slider>().Value))
                            //Player hp less then
                        {
                            Objects.ProjectLogger.WriteLog($"Use Cutless on {target}");
                            LeagueSharp.Common.Items.UseItem(_offensive.Cutless.Id, target);
                            return;
                        }

            if (Objects.ProjectMenu.Item(Names.Menu.MenuOffensiveItemBase+"Boolean.Youmuu").GetValue<bool>()&&LeagueSharp.Common.Items.HasItem(_offensive.GhostBlade.Id))
                // If enabled and has item
                if (_offensive.GhostBlade.IsReady()&&target.IsValidTarget(Objects.Player.AttackRange+Objects.Player.BoundingRadius))
                    // Is ready and target is in auto range
                    if (Orbwalker.ActiveMode==Orbwalking.OrbwalkingMode.Combo)
                    {
                        Objects.ProjectLogger.WriteLog($"Use Ghostblade on {target}");
                        LeagueSharp.Common.Items.UseItem(_offensive.GhostBlade.Id);
                        return;
                    }

            #endregion Offensive

            #region Defensive

            if (Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.QSS").GetValue<bool>()&&LeagueSharp.Common.Items.HasItem(_defensive.Qss.Id))
                if ((Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.ComboOnly").GetValue<bool>()&&(Orbwalker.ActiveMode==Orbwalking.OrbwalkingMode.Combo))
                    ||!Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.ComboOnly").GetValue<bool>())
                    if (_defensive.Qss.IsReady())
                        foreach (var buff in
                            Buffs.GetTypes.Where(buff => Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.QSS."+buff).GetValue<bool>()))
                            if (Objects.Player.HasBuffOfType(buff))
                            {
                                Objects.ProjectLogger.WriteLog($"Use QSS Reason {buff}");
                                Utility.DelayAction.Add(Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Slider.QSS.Delay").GetValue<Slider>().Value, () => LeagueSharp.Common.Items.UseItem(_defensive.Qss.Id));
                            }

            // ReSharper disable once RedundantNameQualifier
            if (Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.Merc").GetValue<bool>()&&LeagueSharp.Common.Items.HasItem(_defensive.Merc.Id))
                if ((Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.ComboOnly").GetValue<bool>()&&(Orbwalker.ActiveMode==Orbwalking.OrbwalkingMode.Combo))
                    ||!Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.ComboOnly").GetValue<bool>())
                    if (_defensive.Merc.IsReady())
                        foreach (var buff in
                            Buffs.GetTypes.Where(buff => Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Boolean.Merc."+buff).GetValue<bool>()))
                            if (Objects.Player.HasBuffOfType(buff))
                            {
                                Objects.ProjectLogger.WriteLog($"Use Merc Reason {buff}");
                                Utility.DelayAction.Add(Objects.ProjectMenu.Item(Names.Menu.MenuDefensiveItemBase+"Slider.Merc.Delay").GetValue<Slider>().Value, () => LeagueSharp.Common.Items.UseItem(_defensive.Qss.Id));
                            }

            #endregion Defensive
        }

        #endregion Public Methods

        #region Private Fields

        private readonly Defensive _defensive;
        private readonly Offensive _offensive;

        #endregion Private Fields

        #region Private Methods

        private void After_Attack(AttackableUnit unit, AttackableUnit target) {}
        private void Before_Attack(Orbwalking.BeforeAttackEventArgs args) {}

        #endregion Private Methods
    }

}