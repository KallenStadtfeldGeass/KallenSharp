using _Project_Geass.Data;
using _Project_Geass.Globals;
using _Project_Geass.Module.Core.Items.Menus;
using _Project_Geass.Module.Core.Mana.Menus;
using _Project_Geass.Module.Core.OnLevel.Menus;
using _Project_Geass.Module.PreLoad.Menus;
using LeagueSharp;
using System.Linq;

namespace _Project_Geass.Module
{
    internal class Initializer
    {
        public Initializer()
        {
            if (Names.ChampionBundled.All(p => ObjectManager.Player.ChampionName != p)) return;

            Humanizer.DelayHandler.Load(true);
            // ReSharper disable once UnusedVariable
            var initializerMenu = new PreLoadMenu();
            Static.Objects.SettingsMenu.AddToMainMenu();

            if (!Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.Enable").GetValue<bool>()) return;

            var champ = new Data.Champions.Load();
            var coreMenu = new LeagueSharp.Common.Menu("Core Modules","CoreModulesMenu");

            //Initilize Menus

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.DrawingMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new Core.Drawing.Menus.Drawing(coreMenu,champ.GetDrawing);
                //Static.Objects.ProjectLogger.WriteLog("Drawing Menu");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ManaMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new ManaMenu(coreMenu,champ.GetManaSettings);
                //  Static.Objects.ProjectLogger.WriteLog("Mana Menu");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ItemMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new Item(coreMenu);
                //  Static.Objects.ProjectLogger.WriteLog("Item");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.OnLevelMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new Abilities(coreMenu,champ.GetAbilities);
                //  Static.Objects.ProjectLogger.WriteLog("Auto Level Menu");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.TrinketMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable

                var menu = new Trinket(coreMenu);
                //  Static.Objects.ProjectLogger.WriteLog("Trinket Menu");
            }
            Static.Objects.ProjectMenu.AddSubMenu(coreMenu);
            Static.Objects.ProjectMenu.AddToMainMenu();
        }
    }
}