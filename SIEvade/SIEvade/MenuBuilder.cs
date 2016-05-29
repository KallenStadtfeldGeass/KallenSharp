using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace SIEvade
{
    internal class MenuBuilder : Core
    {

        private static string _menuNameBase;
        private const string MenuItemBase = "SIEvade.";
        private static string _champName;
        private static string _currentMode = "T";
        public static void Load(string champName)
        {
            _champName = champName;
            _menuNameBase = $"{MenuItemBase}{_champName}";
            SMenu.AddSubMenu(D_Menu());
            SMenu.AddSubMenu(C_Menu());
            Game.OnUpdate += OnUpdate;
        }

        private static Menu C_Menu()
        {
            //Champ Based Menu
            var menu = new Menu(_menuNameBase, $".{_champName}");
            var champMenu = new Menu(".HP% Options", $".{_champName}.HP");
            champMenu.AddItem(new MenuItem($"{_champName}.DangerLevel.High", "High HP% Activation").SetValue(new Slider(SMenu.Item(".DangerLevel.High").GetValue<Slider>().Value)));
            champMenu.AddItem(new MenuItem($"{_champName}.DangerLevel.Mid", "Mid HP% Activation").SetValue(new Slider(SMenu.Item(".DangerLevel.Mid").GetValue<Slider>().Value)));
            champMenu.AddItem(new MenuItem($"{_champName}.DangerLevel.Low", "Low HP% Activation").SetValue(new Slider(SMenu.Item(".DangerLevel.Low").GetValue<Slider>().Value)));

            var clevelsMenu = new Menu(".Danger Level Settings", $".{_champName}.DangerLevelSettings");

            var clowMenu = GenerateEvadeMenu("LowHP", "Low");
            var cmidMenu = GenerateEvadeMenu("MidHP", "Mid");
            var chighMenu = GenerateEvadeMenu("HighHP", "High");

            clevelsMenu.AddSubMenu(clowMenu);
            clevelsMenu.AddSubMenu(cmidMenu);
            clevelsMenu.AddSubMenu(chighMenu);

            champMenu.AddSubMenu(clevelsMenu);

            // menu.AddSubMenu(resetMenu);
     
            menu.AddSubMenu(champMenu);
            return menu;
        }
        private static Menu D_Menu()
        {

            var menu = new Menu("SIEvade Default", $".SIEvade");

            var defaultMenu = new Menu(".Default Options", $".default");
            defaultMenu.AddItem(new MenuItem($".DangerLevel.High", "High HP% Activation").SetValue(new Slider(85)));
            defaultMenu.AddItem(new MenuItem($".DangerLevel.Mid", "Mid HP% Activation").SetValue(new Slider(65)));
            defaultMenu.AddItem(new MenuItem($".DangerLevel.Low", "Low HP% Activation").SetValue(new Slider(35)));

            var levelsMenu = new Menu(".Danger Level Settings", $".DangerLevelSettings");

            var lowMenu = GenerateEvadeMenu("LowHP", "Low",
                new Evade.Base(new Evade.SpellsSettings(false, true, true),
                    new Evade.TimeSettings(150, 50, 100), new Evade.OtherSettings(false, true, true, true),
                    3, true, true));


            var midMenu = GenerateEvadeMenu("MidHP", "Mid",
          new Evade.Base(new Evade.SpellsSettings(false, true, false),
              new Evade.TimeSettings(250, 75, 125), new Evade.OtherSettings(true, false, true, false),
              3, true, true));


            var highMenu = GenerateEvadeMenu("HighHP", "High",
  new Evade.Base(new Evade.SpellsSettings(true, false, false),
                    new Evade.TimeSettings(300, 100, 150), new Evade.OtherSettings(true, false, true, false),
                    3, true, false));

            levelsMenu.AddSubMenu(lowMenu);
            levelsMenu.AddSubMenu(midMenu);
            levelsMenu.AddSubMenu(highMenu);

            defaultMenu.AddSubMenu(levelsMenu);
            menu.AddSubMenu(defaultMenu);
  
            return menu;
        }

        private static Menu GenerateEvadeMenu(string menuString, string dangerLevelString, Evade.Base evadeBase)
        {
            string itembase = $".DangerLevelSettings.{dangerLevelString}";
            var evadeMenu = new Menu($".{menuString}", $".DangerLevelSettings.{dangerLevelString}");
            evadeMenu.AddItem(new MenuItem(itembase + ".EvadeMode", "Evade Smoothness level").SetValue(new Slider(evadeBase.EvadeMode, 1, 3)));
            evadeMenu.AddItem(new MenuItem(itembase + ".UseEvade", "Activate Evade").SetValue(evadeBase.UseEvade));
            evadeMenu.AddItem(new MenuItem(itembase + ".UseEvadeSkills", "Use Evade Skills").SetValue(evadeBase.UseEvadeSkills));
            evadeMenu.AddItem(new MenuItem(itembase + ".UseDangerousKeys", "Enable Dodge Only Dangerous Keys").SetValue(false));

            var timeMenu = new Menu(".Delay Settings", $".DangerLevelSettings.{dangerLevelString}.Time");
            //Time
            timeMenu.AddItem(new MenuItem(itembase + ".TimeSettings.Reaction", "Reaction Time").SetValue(new Slider((int)evadeBase.TimeSetting.ReactionTime, 0, 750)));
            timeMenu.AddItem(new MenuItem(itembase + ".TimeSettings.Tick", "Tick Delay").SetValue(new Slider((int)evadeBase.TimeSetting.TickTime, 0, 250)));
            timeMenu.AddItem(new MenuItem(itembase + ".TimeSettings.Detection", "Spell Detection Delay").SetValue(new Slider((int)evadeBase.TimeSetting.DetectionTime, 0, 500)));

            var spellMenu = new Menu(".Spell Dodge Settings", $".DangerLevelSettings.{dangerLevelString}.SpellSettings");
            //Spell Settings
            spellMenu.AddItem(new MenuItem(itembase + ".SpellSettings.DodgeDangerous", "Dodge Only Dangerous(Always)").SetValue(evadeBase.SpellSetting.DodgeDangerous));
            spellMenu.AddItem(new MenuItem(itembase + ".SpellSettings.DodgeCircular", "Dodge Circular").SetValue(evadeBase.SpellSetting.DodgeCircular));
            spellMenu.AddItem(new MenuItem(itembase + ".SpellSettings.DodgeFog", "Dodge Fog of war Spells").SetValue(evadeBase.SpellSetting.DodgeFog));


            var otherMenu = new Menu(".Other Settings", $".DangerLevelSettings.{dangerLevelString}.OtherSettings");
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

        private static Menu GenerateEvadeMenu(string menuString, string dangerLevelString)
        {

            string champitembase = $"{_champName}.DangerLevelSettings.{dangerLevelString}";
            string defaultitembase = $".DangerLevelSettings.{dangerLevelString}";
            var evadeMenu = new Menu($".{menuString}", $".{_champName}.DangerLevelSettings.{dangerLevelString}");

           
            evadeMenu.AddItem(new MenuItem(champitembase + ".EvadeMode", "Evade Smoothness level")
                .SetValue(new Slider(SMenu.Item(defaultitembase + ".EvadeMode").GetValue<Slider>().Value, 1, 3)));


            evadeMenu.AddItem(new MenuItem(champitembase + ".UseEvade", "Activate Evade").SetValue(SMenu.Item(defaultitembase + ".UseEvade").GetValue<bool>()));
            evadeMenu.AddItem(new MenuItem(champitembase + ".UseEvadeSkills", "Use Evade Skills").SetValue(SMenu.Item(defaultitembase + ".UseEvadeSkills").GetValue<bool>()));
            evadeMenu.AddItem(new MenuItem(champitembase + ".UseDangerousKeys", "Enable Dodge Only Dangerous Keys").SetValue(SMenu.Item(defaultitembase + ".UseDangerousKeys").GetValue<bool>()));

            //SMenu.Item(Defaultitembase + ".UseEvadeSkills").GetValue<bool>()
            var timeMenu = new Menu(".Delay Settings", $".{_champName}.DangerLevelSettings.{dangerLevelString}.Time");
            //Time
            timeMenu.AddItem(new MenuItem(champitembase + ".TimeSettings.Reaction", "Reaction Time").SetValue(new Slider(SMenu.Item(defaultitembase + ".TimeSettings.Reaction").GetValue<Slider>().Value, 0, 750)));
            timeMenu.AddItem(new MenuItem(champitembase + ".TimeSettings.Tick", "Tick Delay").SetValue(new Slider(SMenu.Item(defaultitembase + ".TimeSettings.Tick").GetValue<Slider>().Value, 0, 250)));
            timeMenu.AddItem(new MenuItem(champitembase + ".TimeSettings.Detection", "Spell Detection Delay").SetValue(new Slider(SMenu.Item(defaultitembase + ".TimeSettings.Detection").GetValue<Slider>().Value, 0, 500)));

            var spellMenu = new Menu(".Spell Dodge Settings", $".{_champName}.DangerLevelSettings.{dangerLevelString}.SpellSettings");
            //Spell Settings
            spellMenu.AddItem(new MenuItem(champitembase + ".SpellSettings.DodgeDangerous", "Dodge Only Dangerous(Always)").SetValue(SMenu.Item(defaultitembase + ".SpellSettings.DodgeDangerous").GetValue<bool>()));
            spellMenu.AddItem(new MenuItem(champitembase + ".SpellSettings.DodgeCircular", "Dodge Circular").SetValue(SMenu.Item(defaultitembase + ".SpellSettings.DodgeCircular").GetValue<bool>()));
            spellMenu.AddItem(new MenuItem(champitembase + ".SpellSettings.DodgeFog", "Dodge Fog of war Spells").SetValue(SMenu.Item(defaultitembase + ".SpellSettings.DodgeFog").GetValue<bool>()));


            var otherMenu = new Menu(".Other Settings", $".{_champName}.DangerLevelSettings.{dangerLevelString}.OtherSettings");
            //Other settings
            otherMenu.AddItem(new MenuItem(champitembase + ".OtherSettings.ClickOnce", "Only Click Once").SetValue(SMenu.Item(defaultitembase + ".OtherSettings.ClickOnce").GetValue<bool>()));
            otherMenu.AddItem(new MenuItem(champitembase + ".OtherSettings.FastMove", "Allow Fast Move").SetValue(SMenu.Item(defaultitembase + ".OtherSettings.FastMove").GetValue<bool>()));
            otherMenu.AddItem(new MenuItem(champitembase + ".OtherSettings.ContinueMovement", "Continue Movement").SetValue(SMenu.Item(defaultitembase + ".OtherSettings.ContinueMovement").GetValue<bool>()));
            otherMenu.AddItem(new MenuItem(champitembase + ".OtherSettings.SpellColision", "Spell Colision Check").SetValue(SMenu.Item(defaultitembase + ".OtherSettings.SpellColision").GetValue<bool>()));

            evadeMenu.AddSubMenu(timeMenu);
            evadeMenu.AddSubMenu(spellMenu);
            evadeMenu.AddSubMenu(otherMenu);
            return evadeMenu;
        }

        private static void OnUpdate(EventArgs args)
        {
            if (Time.TickCount - Time.LastTick < 100) return;

            Time.LastTick = Time.TickCount;

            if (EzEvadeMenu == null)
            {
                Console.WriteLine("EzEvade null");
                EzEvadeMenu = Menu.GetMenu("ezEvade", "ezEvade");
                return;

            }


            var mode = "none";

            if (SMenu.Item($"{_champName}.DangerLevel.Low").GetValue<Slider>().Value >= (int)Player.HealthPercent)
                mode = "Low";

            else if (SMenu.Item($"{_champName}.DangerLevel.Mid").GetValue<Slider>().Value >= (int)Player.HealthPercent)
                mode = "Mid";

            else if (SMenu.Item($"{_champName}.DangerLevel.High").GetValue<Slider>().Value >= (int)Player.HealthPercent)
                mode = "High";

            if (!_currentMode.Equals(mode))
                SyncMenu(mode);
        }


        private static void SyncMenu(string mode)
        {
            _currentMode = mode;
                switch (mode)
                {

                    case "none": //Dont evade
                    {
                        // ReSharper disable once RedundantArgumentDefaultValue
                        EzEvadeMenu.Item("DodgeSkillShots")
                            .SetValue(new KeyBind(EzEvadeMenu.Item("DodgeSkillShots").GetValue<KeyBind>().Key,
                                KeyBindType.Toggle, false));
                        // ReSharper disable once RedundantArgumentDefaultValue
                        EzEvadeMenu.Item("ActivateEvadeSpells")
                            .SetValue(new KeyBind(EzEvadeMenu.Item("ActivateEvadeSpells").GetValue<KeyBind>().Key,
                                KeyBindType.Toggle, false));

                        break;
                    }

                    default: // handle evade based on HP
                    {

                        string itembase = $"{_champName}.DangerLevelSettings.{mode}";
                        EzEvadeMenu.Item("DodgeSkillShots")
                            .SetValue(new KeyBind(EzEvadeMenu.Item("DodgeSkillShots").GetValue<KeyBind>().Key,
                                KeyBindType.Toggle,
                                SMenu.Item(itembase + ".UseEvade").GetValue<bool>()));
                        EzEvadeMenu.Item("ActivateEvadeSpells")
                            .SetValue(new KeyBind(EzEvadeMenu.Item("ActivateEvadeSpells").GetValue<KeyBind>().Key,
                                KeyBindType.Toggle,
                                SMenu.Item(itembase + ".UseEvadeSkills").GetValue<bool>()));

                        EzEvadeMenu.Item("DodgeDangerousKeyEnabled")
                            .SetValue(SMenu.Item(itembase + ".UseDangerousKeys").GetValue<bool>());

                        EzEvadeMenu.Item("EvadeMode")
                            .SetValue(new StringList(new[] {"Fast", "Smooth", "Very Smooth"},
                                SMenu.Item(itembase + ".EvadeMode").GetValue<Slider>().Value));

                        //Time
                        EzEvadeMenu.Item("TickLimiter")
                            .SetValue(new Slider(SMenu.Item(itembase + ".TimeSettings.Tick").GetValue<Slider>().Value, 0,
                                250));
                        EzEvadeMenu.Item("ReactionTime")
                            .SetValue(
                                new Slider(SMenu.Item(itembase + ".TimeSettings.Reaction").GetValue<Slider>().Value, 0,
                                    750));
                        EzEvadeMenu.Item("SpellDetectionTime")
                            .SetValue(
                                new Slider(SMenu.Item(itembase + ".TimeSettings.Detection").GetValue<Slider>().Value,
                                    0, 500));

                        //Spell
                        EzEvadeMenu.Item("DodgeDangerous")
                            .SetValue(SMenu.Item(itembase + ".SpellSettings.DodgeDangerous").GetValue<bool>());
                        EzEvadeMenu.Item("DodgeCircularSpells")
                            .SetValue(SMenu.Item(itembase + ".SpellSettings.DodgeCircular").GetValue<bool>());
                        EzEvadeMenu.Item("DodgeFOWSpells")
                            .SetValue(SMenu.Item(itembase + ".SpellSettings.DodgeFog").GetValue<bool>());

                        //Other
                        EzEvadeMenu.Item("CheckSpellCollision")
                            .SetValue(SMenu.Item(itembase + ".OtherSettings.SpellColision").GetValue<bool>());
                        EzEvadeMenu.Item("ContinueMovement")
                            .SetValue(SMenu.Item(itembase + ".OtherSettings.ContinueMovement").GetValue<bool>());
                        EzEvadeMenu.Item("ClickOnlyOnce")
                            .SetValue(SMenu.Item(itembase + ".OtherSettings.ClickOnce").GetValue<bool>());
                        EzEvadeMenu.Item("FastMovementBlock")
                            .SetValue(!SMenu.Item(itembase + ".OtherSettings.FastMove").GetValue<bool>());
                        break;
                    }
                }

            
        }

    }
}