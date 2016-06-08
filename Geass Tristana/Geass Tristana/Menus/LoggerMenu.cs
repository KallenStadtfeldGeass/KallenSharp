using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
    class LoggerMenu : Libaries.Logger
    {
            public Menu GetMenu()
            {
                var menu = new Menu("Geass.Logger", "geassLogger");
                menu.AddItem(new MenuItem("Geass.Logger.Enable", "Enable Console Logger(debugger)").SetValue(false));
                return menu;

            }

            public void Load()
            {
                SMenu.AddSubMenu(GetMenu());
            }
        }
   }
