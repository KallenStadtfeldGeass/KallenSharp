using _Project_Geass.Data;
using _Project_Geass.Globals;

namespace _Project_Geass.Bootloaders.Core.Functions
{
    internal class MenuOptions
    {
        public static bool HumanizerEnabled()
        {
            return Static.Objects.ProjectMenu.Item($"{Names.Menu.BaseItem }.Humanizer").GetValue<bool>();
        }
    }
}