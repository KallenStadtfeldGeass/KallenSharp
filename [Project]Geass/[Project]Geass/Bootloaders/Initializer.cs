using _Project_Geass.Bootloaders.Base.Menus;
using _Project_Geass.Bootloaders.Core.Menus;
using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp;
using System.Linq;

namespace _Project_Geass.Bootloaders
{
    internal class Initializer
    {
        public Initializer()
        {
            if (Names.ChampionBundled.All(p => ObjectManager.Player.ChampionName != p)) return;

            Humanizer.DelayHandler.Load(true);
            // ReSharper disable once UnusedVariable
            var initializerMenu = new SettingsMenu();
            Static.Objects.SettingsMenu.AddToMainMenu();

            if (!Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.Enable").GetValue<bool>()) return;

            var champ = new Data.Champions.Load();

            //Initilize Menus

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.DrawingMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new DrawingMenu(champ.GetDrawing);
                //Static.Objects.ProjectLogger.WriteLog("Drawing Menu");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ManaMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new ManaMenu(champ.GetManaSettings);
                //  Static.Objects.ProjectLogger.WriteLog("Mana Menu");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ItemMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new ItemsMenu();
                //  Static.Objects.ProjectLogger.WriteLog("Item");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.OnLevelMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new OnLevelMenu(champ.GetAbilities);
                //  Static.Objects.ProjectLogger.WriteLog("Auto Level Menu");
            }

            if (
                Static.Objects.SettingsMenu.Item(
                    $"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.TrinketMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable

                var menu = new TrinketMenu();
                //  Static.Objects.ProjectLogger.WriteLog("Trinket Menu");
            }

            Static.Objects.ProjectMenu.AddToMainMenu();
        }
    }
}