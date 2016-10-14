using System.Drawing;
using LeagueSharp.Common;
using _Project_Geass.Data.Constant.Champions;
using _Project_Geass.Data.Static;
using _Project_Geass.Functions;
using _Project_Geass.Module.Champions.Core;
using _Project_Geass.Module.Core.Items.Menus;
using _Project_Geass.Module.Core.Mana.Menus;
using _Project_Geass.Module.Core.OnLevel.Menus;
using _Project_Geass.Tick;
using Color = SharpDX.Color;

namespace _Project_Geass
{

    internal class Initializer
    {
        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Initializer" /> class.
        /// </summary>
        public Initializer()
        {
            Objects.ProjectLogger.WriteLog("Loading...");
            if (!Bootloader.ChampionBundled.ContainsKey(Objects.Player.ChampionName))
                return;
            if (!Bootloader.ChampionBundled[Objects.Player.ChampionName])
                return;

            Objects.ProjectLogger.WriteLog("Load Delays...");
            Handler.Load(true);
            Data.Cache.Objects.Load();

            // ReSharper disable once UnusedVariable
            var initializerMenu=new SettingsMenuGenerater();
            Objects.SettingsMenu.AddToMainMenu();

            var championSettings=new Settings();

            if (!Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Objects.Player.ChampionName}.Enable").GetValue<bool>())
                return;

            var coreMenu=new Menu("Core Modules", "CoreModulesMenu");

            var drawingEnabled=Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Objects.Player.ChampionName}.DrawingMenu").GetValue<bool>();
            var manaEnabled=Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Objects.Player.ChampionName}.ManaMenu").GetValue<bool>();
            var itemEnabled=Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Objects.Player.ChampionName}.ItemMenu").GetValue<bool>();
            var autoLevelEnabled=Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Objects.Player.ChampionName}.OnLevelMenu").GetValue<bool>();
            var trinketEnabled=Objects.SettingsMenu.Item($"{Names.Menu.BaseItem}{Objects.Player.ChampionName}.TrinketMenu").GetValue<bool>();

            Objects.ProjectLogger.WriteLog("Load Base Menu's...");
            var orbWalker=new Orbwalking.Orbwalker(Objects.ProjectMenu.SubMenu(nameof(Orbwalking.Orbwalker)).SetFontStyle(FontStyle.Bold, Color.GreenYellow));
            // ReSharper disable once UnusedVariable
            var manaMenu=new ManaMenu(coreMenu, championSettings.ManaSettings, manaEnabled);
            // ReSharper disable once UnusedVariable
            var drawingMeun=new Module.Core.Drawing.Menus.Drawing(coreMenu, championSettings.DrawingSettings, drawingEnabled);
            // ReSharper disable once UnusedVariable
            var itemMenu=new Item(coreMenu, itemEnabled, orbWalker);
            // ReSharper disable once UnusedVariable
            var autoLevelMenu=new Abilities(coreMenu, championSettings.AbilitieSettings, autoLevelEnabled);
            // ReSharper disable once UnusedVariable
            var trinketMenu=new Trinket(coreMenu, trinketEnabled);

            Objects.ProjectMenu.AddSubMenu(coreMenu.SetFontStyle(FontStyle.Bold, Color.GreenYellow));
            Objects.ProjectMenu.AddToMainMenu();
            Bootloader.Load(manaEnabled, orbWalker);
        }

        #endregion Public Constructors
    }

}