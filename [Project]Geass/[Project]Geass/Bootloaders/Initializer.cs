using _Project_Geass.Bootloaders.Base.Menus;
using _Project_Geass.Constants;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders
{
    internal class Initializer
    {
        public Initializer()
        {
            //Load Base Menu(What champs to use)
            var initializerMenu = new InitializerMenu();
            Globals.Static.Objects.SettingsMenu.AddToMainMenu();

            if (
                !Globals.Static.Objects.SettingsMenu.Item(Names.Menu.BaseName +
                                                          Globals.Static.Objects.Player.ChampionName).GetValue<bool>())
            {
                Globals.Static.Objects.ProjectMenu = new Menu(Constants.Names.ProjectName + $".{Globals.Static.Objects.Player.ChampionName}.Disabled", Constants.Names.ProjectName + $".{Globals.Static.Objects.Player.ChampionName}.Disabled", true);
                Globals.Static.Objects.ProjectMenu.AddToMainMenu();
                return;
            }

            Globals.Static.Objects.ProjectMenu = new Menu(Constants.Names.ProjectName + $".{Globals.Static.Objects.Player.ChampionName}", Constants.Names.ProjectName + $".{Globals.Static.Objects.Player.ChampionName}", true);

            //Initilize Menus

            Globals.Static.Objects.ProjectMenu.AddToMainMenu();
        }
    }
}