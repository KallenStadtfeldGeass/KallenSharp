using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Events 
{
    class HumanizeEvents : Core
    {
        public const string MenuNameBase = ".Humanizer Menu";
        private const string MenuItemBase = ".Humanizer.";

        public const string ItemBase = MenuItemBase + "Delays.";

        private static string[] _sDelays =
        {
                 "LevelDelay", "EventDelay", "ItemDelay", "TrinketDelay"
        };
        
        public static void LoadDelays()
        {

            try
            {
                foreach (var sDelay in _sDelays.Where(sDelay => !_TickManager.IsPresent(sDelay)))
                {
                    _TickManager.AddTick($"{ItemBase}Slider.{sDelay}",10f, SMenu.Item($"{ItemBase}Slider.{sDelay}").GetValue<Slider>().Value);
                }
            }

            catch
            {
              
            }

            finally
            {
                _TickManager.SetRandomizer(SMenu.Item($"{ItemBase}Slider.MinSeedDelay").GetValue<Slider>().Value,SMenu.Item($"{ItemBase}Slider.MaxSeedDelay").GetValue<Slider>().Value);
            }

        }
    }
}
