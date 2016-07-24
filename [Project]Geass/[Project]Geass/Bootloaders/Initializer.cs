using _Project_Geass.Bootloaders.Base.Menus;
using _Project_Geass.Bootloaders.Core.Menus;
using _Project_Geass.Constants;
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
                    return Champions.Corki.SkillSequence.AbilitySequence;
                case "Tristana":
                    return Champions.Corki.SkillSequence.AbilitySequence;
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

            Static.Objects.ProjectMenu.AddToMainMenu();
        }
    }
}