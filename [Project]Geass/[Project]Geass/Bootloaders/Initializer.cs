using _Project_Geass.Bootloaders.Base.Menus;
using _Project_Geass.Bootloaders.Champions;
using _Project_Geass.Bootloaders.Core.Menus;
using _Project_Geass.Data;
using _Project_Geass.Globals;

namespace _Project_Geass.Bootloaders
{
    internal class Initializer
    {
        public Initializer()
        {
            //Load Base Menu(What champs to use)
            // ReSharper disable once UnusedVariable
            var aioChamp = new Data.Champions.AioChampion();
            //Humanizer.DelayHandler.Load(Core.Functions.MenuOptions.HumanizerEnabled());
            Humanizer.DelayHandler.Load(true);
            var initializerMenu = new SettingsMenu();
            Static.Objects.SettingsMenu.AddToMainMenu();

            if (!Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.Enable").GetValue<bool>()) return;

            //Initilize Menus

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.DrawingMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new DrawingMenu();
                Static.Objects.ProjectLogger.WriteLog("DrawingMenu");
            }

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ManaMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new ManaMenu(aioChamp.GetManaSettings);
                Static.Objects.ProjectLogger.WriteLog("Mana Menu");
            }

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ItemMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new ItemsMenu();
                Static.Objects.ProjectLogger.WriteLog("Item");
            }

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.OnLevelMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new OnLevelMenu(aioChamp.GetAbilities);
                Static.Objects.ProjectLogger.WriteLog("Auto Level Menu");
            }

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.TrinketMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable

                var menu = new TrinketMenu();
                Static.Objects.ProjectLogger.WriteLog("Trinket Menu");
            }

            if (Static.Objects.Player.ChampionName == "Ashe")
            {
                // var mAshe = new Champions.Menus.Ashe();
                var eAshe = new Ashe();
            }
            Static.Objects.ProjectMenu.AddToMainMenu();
        }
    }
}