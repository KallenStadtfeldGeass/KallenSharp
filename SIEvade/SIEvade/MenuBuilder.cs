using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace SIEvade
{
    internal class MenuBuilder : Core
    {

        private static string _menuNameBase;
        private static readonly string MenuItemBase = "SIEvade.";
        private static string _champName;
        private static string _currentMode = "T";
        public static void Load(string champName)
        {
            _champName = champName;
            _menuNameBase = $"{MenuItemBase}{_champName}";
            SMenu.AddSubMenu(_Menu());

            Game.OnUpdate += OnUpdate;
        }
        
        private static Menu _Menu()
        {

            var menu = new Menu(_menuNameBase, $".{_champName}");

            //var resetMenu = new Menu(".Reset Options", $".{_champName}.Reset");
            //resetMenu.AddItem(new MenuItem(MenuItemBase + "Reset.Tank", "Use Tank Settings").SetValue(false));
            //resetMenu.AddItem(new MenuItem(MenuItemBase + "Reset.Jungle", "Use Jungle Settings").SetValue(false));
            //resetMenu.AddItem(new MenuItem(MenuItemBase + "Reset.Mid", "Use Mid Settings").SetValue(false));
            //resetMenu.AddItem(new MenuItem(MenuItemBase + "Reset.Adc", "Use ADC Settings").SetValue(false));
            //resetMenu.AddItem(new MenuItem(MenuItemBase + "Reset.Support", "Use Support Settings").SetValue(false));

            var hpMenu = new Menu(".HP% Options", $".{_champName}.HP");
            hpMenu.AddItem(new MenuItem($"{_champName}.DangerLevel.High", "High HP% Activation").SetValue(new Slider(85)));
            hpMenu.AddItem(new MenuItem($"{_champName}.DangerLevel.Mid", "Mid HP% Activation").SetValue(new Slider(65)));
            hpMenu.AddItem(new MenuItem($"{_champName}.DangerLevel.Low", "Low HP% Activation").SetValue(new Slider(35)));

            var levelsMenu = new Menu(".Danger Level Settings", $".{_champName}.DangerLevelSettings");

            var lowMenu = GenerateEvadeMenu("LowHP", "Low",
                new Evade.Base(new Evade.SpellsSettings(false, true, true),
                    new Evade.TimeSettings(150, 50, 100), new Evade.OtherSettings(false, true, true, true),
                    3,true,true));


            var midMenu = GenerateEvadeMenu("MidHP", "Mid",
          new Evade.Base(new Evade.SpellsSettings(false, true, false),
              new Evade.TimeSettings(250, 75, 125), new Evade.OtherSettings(true, false, true, false),
              3,true,true));


            var highMenu = GenerateEvadeMenu("HighHP", "High",
  new Evade.Base(new Evade.SpellsSettings(true, false, false),
                    new Evade.TimeSettings(300, 100, 150), new Evade.OtherSettings(true, false, true, false),
                    3,true,false));

            levelsMenu.AddSubMenu(lowMenu);
            levelsMenu.AddSubMenu(midMenu);
            levelsMenu.AddSubMenu(highMenu);

          // menu.AddSubMenu(resetMenu);
            menu.AddSubMenu(hpMenu);
            menu.AddSubMenu(levelsMenu);
            return menu;
        }
  
        private static Menu GenerateEvadeMenu(string menuString,string dangerLevelString,Evade.Base evadeBase)
        {
            string itembase = $"{_champName}.DangerLevelSettings.{dangerLevelString}";
            var evadeMenu = new Menu($".{menuString}", $".{_champName}.DangerLevelSettings.{dangerLevelString}");
            evadeMenu.AddItem(new MenuItem(itembase + ".EvadeMode", "Evade Smoothness level").SetValue(new Slider(evadeBase.EvadeMode, 1, 3)));
            evadeMenu.AddItem(new MenuItem(itembase + ".UseEvade", "Activate Evade").SetValue(evadeBase.UseEvade));
            evadeMenu.AddItem(new MenuItem(itembase + ".UseEvadeSkills", "Use Evade Skills").SetValue(evadeBase.UseEvadeSkills));

            var timeMenu = new Menu(".Delay Settings", $".{_champName}.DangerLevelSettings.{dangerLevelString}.Time");
            //Time
            timeMenu.AddItem(new MenuItem(itembase + ".TimeSettings.Reaction", "Reaction Time").SetValue(new Slider((int)evadeBase.TimeSetting.ReactionTime, 0, 750)));
            timeMenu.AddItem(new MenuItem(itembase + ".TimeSettings.Tick", "Tick Delay").SetValue(new Slider((int)evadeBase.TimeSetting.TickTime, 0, 250)));
            timeMenu.AddItem(new MenuItem(itembase + ".TimeSettings.Detection", "Spell Detection Delay").SetValue(new Slider((int)evadeBase.TimeSetting.DetectionTime, 0, 500)));

            var spellMenu = new Menu(".Spell Dodge Settings", $".{_champName}.DangerLevelSettings.{dangerLevelString}.SpellSettings");
            //Spell Settings
            spellMenu.AddItem(new MenuItem(itembase + ".SpellSettings.DodgeDangerous", "Dodge Only Dangerous").SetValue(evadeBase.SpellSetting.DodgeDangerous));
            spellMenu.AddItem(new MenuItem(itembase + ".SpellSettings.DodgeCircular", "Dodge Circular").SetValue(evadeBase.SpellSetting.DodgeCircular));
            spellMenu.AddItem(new MenuItem(itembase + ".SpellSettings.DodgeFog", "Dodge Fog of war Spells").SetValue(evadeBase.SpellSetting.DodgeFog));


            var otherMenu = new Menu(".Other Settings", $".{_champName}.DangerLevelSettings.{dangerLevelString}.OtherSettings");
            //Other settings
            otherMenu.AddItem(new MenuItem(itembase + ".OtherSettings.ClickOnce", "Only Click Once").SetValue(evadeBase.OtherSetting.ClickOnce));
            otherMenu.AddItem(new MenuItem(itembase + ".OtherSettings.FastMove", "Allow Fast Move").SetValue(evadeBase.OtherSetting.FastMove));
            otherMenu.AddItem(new MenuItem(itembase + ".OtherSettings.ContinueMovement", "Continue Movement").SetValue(evadeBase.OtherSetting.ContinueMovement));
            otherMenu.AddItem(new MenuItem(itembase + ".OtherSettings.SpellColision", "Spell Colision Check").SetValue(evadeBase.OtherSetting.SpellColision));

            evadeMenu.AddSubMenu(timeMenu);
            evadeMenu.AddSubMenu(spellMenu);
            evadeMenu.AddSubMenu(otherMenu);
            return evadeMenu;
        }

        private static void OnUpdate(EventArgs args)
        {
            if (Time.TickCount - Time.LastTick < 100) return;

            Time.LastTick = Time.TickCount;

            var mode = "none";

            if (SMenu.Item($"{_champName}.DangerLevel.Low").GetValue<Slider>().Value >= (int)Player.HealthPercent)
                mode = "Low";
                
            else if (SMenu.Item($"{_champName}.DangerLevel.Mid").GetValue<Slider>().Value >= (int)Player.HealthPercent)
                mode = "Mid";

            else if (SMenu.Item($"{_champName}.DangerLevel.High").GetValue<Slider>().Value >= (int)Player.HealthPercent)
                mode = "High";

            if(!_currentMode.Equals(mode))
                SyncMenu(mode);
        }


        private static void SyncMenu(string mode)
        {
            _currentMode = mode;
            switch (mode) 
            {

                case "none"://Dont evade
                {
                    // ReSharper disable once RedundantArgumentDefaultValue
                        Menu.GetMenu("ezEvade", "ezEvade").Item("DodgeSkillShots").SetValue(new KeyBind(Menu.GetMenu("ezEvade", "ezEvade").Item("DodgeSkillShots").GetValue<KeyBind>().Key, KeyBindType.Toggle,false));
                    // ReSharper disable once RedundantArgumentDefaultValue
                        Menu.GetMenu("ezEvade", "ezEvade").Item("ActivateEvadeSpells").SetValue(new KeyBind(Menu.GetMenu("ezEvade", "ezEvade").Item("ActivateEvadeSpells").GetValue<KeyBind>().Key, KeyBindType.Toggle, false));

                        break;
                }
                default: // handle evade based on HP
                {
                        string itembase = $"{_champName}.DangerLevelSettings.{mode}";
                        Menu.GetMenu("ezEvade", "ezEvade").Item("DodgeSkillShots").SetValue(new KeyBind(Menu.GetMenu("ezEvade", "ezEvade").Item("DodgeSkillShots").GetValue<KeyBind>().Key, KeyBindType.Toggle, 
                            SMenu.Item(itembase + ".UseEvade").GetValue<bool>()));
                        Menu.GetMenu("ezEvade", "ezEvade").Item("ActivateEvadeSpells").SetValue(new KeyBind(Menu.GetMenu("ezEvade", "ezEvade").Item("ActivateEvadeSpells").GetValue<KeyBind>().Key, KeyBindType.Toggle,
                            SMenu.Item(itembase + ".UseEvadeSkills").GetValue<bool>()));

                      
                        Menu.GetMenu("ezEvade", "ezEvade").Item("EvadeMode").SetValue(new StringList(new[] { "Fast", "Smooth", "Very Smooth" }, SMenu.Item(itembase + ".EvadeMode").GetValue<Slider>().Value));

                        //Time
                        Menu.GetMenu("ezEvade", "ezEvade").Item("TickLimiter").SetValue(new Slider(SMenu.Item(itembase + ".TimeSettings.Tick").GetValue<Slider>().Value, 0, 250));
                        Menu.GetMenu("ezEvade", "ezEvade").Item("ReactionTime").SetValue(new Slider(SMenu.Item(itembase + ".TimeSettings.Reaction").GetValue<Slider>().Value, 0, 750));
                        Menu.GetMenu("ezEvade", "ezEvade").Item("SpellDetectionTime").SetValue(new Slider(SMenu.Item(itembase + ".TimeSettings.Detection").GetValue<Slider>().Value, 0, 500));

                        //Spell
                        Menu.GetMenu("ezEvade", "ezEvade").Item("DodgeDangerous").SetValue(SMenu.Item(itembase + ".SpellSettings.DodgeDangerous").GetValue<bool>());
                        Menu.GetMenu("ezEvade", "ezEvade").Item("DodgeCircularSpells").SetValue(SMenu.Item(itembase + ".SpellSettings.DodgeCircular").GetValue<bool>());
                        Menu.GetMenu("ezEvade", "ezEvade").Item("DodgeFOWSpells").SetValue(SMenu.Item(itembase + ".SpellSettings.DodgeFog").GetValue<bool>());

                        //Other
                        Menu.GetMenu("ezEvade", "ezEvade").Item("CheckSpellCollision").SetValue(SMenu.Item(itembase + ".OtherSettings.SpellColision").GetValue<bool>());
                        Menu.GetMenu("ezEvade", "ezEvade").Item("ContinueMovement").SetValue(SMenu.Item(itembase + ".OtherSettings.ContinueMovement").GetValue<bool>());
                        Menu.GetMenu("ezEvade", "ezEvade").Item("ClickOnlyOnce").SetValue(SMenu.Item(itembase + ".OtherSettings.ClickOnce").GetValue<bool>());
                        Menu.GetMenu("ezEvade", "ezEvade").Item("FastMovementBlock").SetValue(!SMenu.Item(itembase + ".OtherSettings.FastMove").GetValue<bool>());

                        break;
                }

            }

        }

    }
}
