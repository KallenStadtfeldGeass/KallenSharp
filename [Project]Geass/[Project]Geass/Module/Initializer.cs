using _Project_Geass.Module.Core.Items.Menus;
using _Project_Geass.Module.Core.Mana.Menus;
using _Project_Geass.Module.Core.OnLevel.Menus;
using _Project_Geass.Module.PreLoad.Menus;
using LeagueSharp;
using System.Linq;
using LeagueSharp.Common;
using _Project_Geass.Data.Champions;
using _Project_Geass.Functions;
using _Project_Geass.Module.Champions.Heroes.Events;

namespace _Project_Geass.Module
{
    internal class Initializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Initializer"/> class.
        /// </summary>
        public Initializer()
        {
            StaticObjects.ProjectLogger.WriteLog("Loading...");
            if (Names.ChampionBundled.All(p => ObjectManager.Player.ChampionName != p)) return;
            StaticObjects.ProjectLogger.WriteLog("Load Delays...");
            Humanizer.DelayHandler.Load(true);
            // ReSharper disable once UnusedVariable
            var initializerMenu = new PreLoadMenu();
            StaticObjects.SettingsMenu.AddToMainMenu();

            if (!StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}{StaticObjects.Player.ChampionName}.Enable").GetValue<bool>()) return;

            var coreMenu = new Menu("Core Modules", "CoreModulesMenu");


            var drawingEnabled = StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}{StaticObjects.Player.ChampionName}.DrawingMenu").GetValue<bool>();
            var manaEnabled = StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}{StaticObjects.Player.ChampionName}.ManaMenu").GetValue<bool>();
            var itemEnabled = StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}{StaticObjects.Player.ChampionName}.ItemMenu").GetValue<bool>();
            var autoLevelEnabled = StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}{StaticObjects.Player.ChampionName}.OnLevelMenu").GetValue<bool>();
            var trinketEnabled = StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}{StaticObjects.Player.ChampionName}.TrinketMenu").GetValue<bool>();

            StaticObjects.ProjectLogger.WriteLog("Load Base Menu's...");
            var orbWalker = new Orbwalking.Orbwalker(StaticObjects.ProjectMenu.SubMenu(nameof(Orbwalking.Orbwalker)));
            var championSettings = new Settings();
            // ReSharper disable once UnusedVariable
            var manaMenu = new ManaMenu(coreMenu, championSettings.ManaSettings, manaEnabled);
            // ReSharper disable once UnusedVariable
            var drawingMeun = new Core.Drawing.Menus.Drawing(coreMenu, championSettings.DrawingSettings,drawingEnabled);
            // ReSharper disable once UnusedVariable
            var itemMenu = new Item(coreMenu,itemEnabled, orbWalker);
            // ReSharper disable once UnusedVariable
            var autoLevelMenu = new Abilities(coreMenu, championSettings.AbilitieSettings,autoLevelEnabled);
            // ReSharper disable once UnusedVariable
            var trinketMenu = new Trinket(coreMenu,trinketEnabled);

            StaticObjects.ProjectMenu.AddSubMenu(coreMenu);
            StaticObjects.ProjectMenu.AddToMainMenu();
            LoadChampion(manaEnabled, orbWalker);

        }

#pragma warning disable CC0091 // Use static method
        void LoadChampion(bool manaEnabled, Orbwalking.Orbwalker orbWalker)
#pragma warning restore CC0091 // Use static method
        {
            switch (StaticObjects.Player.ChampionName)
            {
                case nameof(Tristana):
                    // ReSharper disable once UnusedVariable
                   var tristana = new Tristana(manaEnabled, orbWalker);
                    break;

                case nameof(Ezreal):
                    // ReSharper disable once UnusedVariable
                    var ezreal = new Ezreal(manaEnabled, orbWalker);
                    break;

                case nameof(Ashe):
                    // ReSharper disable once UnusedVariable
                    var ashe = new Ashe(manaEnabled, orbWalker);
                    break;
            }
        }
    }
}