using _Project_Geass.Bootloaders.Base.Menus;
using _Project_Geass.Bootloaders.Core.Menus;
using _Project_Geass.Data;
using _Project_Geass.Globals;

namespace _Project_Geass.Bootloaders
{
    internal class Initializer
    {
        private int[] getAbilitySequence(string name)
        {
            switch (name)
            {
                case "Corki":
                    return Data.Champions.AbilitySequences.Corki;
                case "Tristana":
                    return Data.Champions.AbilitySequences.Tristana;
            }
            return null;
        }

        private int[,] GetManaOptions(string name)
        {
            switch (name)
            {
                case "Corki":
                    return Data.Champions.ManaManager.Corki;
                case "Tristana":
                    return Data.Champions.ManaManager.Tristana;
            }
            return null;
        }

        public Initializer()
        {
            //Load Base Menu(What champs to use)
            // ReSharper disable once UnusedVariable
            var initializerMenu = new SettingsMenu();
            Static.Objects.SettingsMenu.AddToMainMenu();

            Humanizer.DelayHandler.Load();
            if (!Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.Enable").GetValue<bool>()) return;

            //Initilize Menus

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.LastHitHelperMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new LastHitMenu();
            }

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ManaMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new ManaMenu(GetManaOptions(Static.Objects.Player.ChampionName));
            }


            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.ItemMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new ItemsMenu();
            }

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.OnLevelMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new OnLevelMenu(getAbilitySequence(Static.Objects.Player.ChampionName));
            }

        

            if (Static.Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Static.Objects.Player.ChampionName}.TrinketMenu").GetValue<bool>())
            {
                // ReSharper disable once UnusedVariable
                var menu = new TrinketMenu();
            }

            Static.Objects.ProjectMenu.AddToMainMenu();
        }
    }
}