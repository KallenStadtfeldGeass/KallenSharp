using LeagueSharp.Common;
using S__Class_Tristana.Other;
using System;
using System.Linq;

namespace S__Class_Tristana.Events
{
    internal class HumanizeEvents : Core
    {
        private const string MenuItemBase = ".Humanizer.";

        public const string ItemBase = MenuItemBase + "Delays.";
        public const string MenuNameBase = ".Humanizer Menu";

        public static void LoadDelays()
        {
            try
            {
                foreach (var sDelay in SDelays.Where(sDelay => !TickManager.IsPresent(sDelay)))
                {
                    TickManager.AddTick($"{ItemBase}Slider.{sDelay}", 10f, SMenu.Item($"{ItemBase}Slider.{sDelay}").GetValue<Slider>().Value);
                }
            }

            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            finally
            {
                TickManager.SetRandomizer(SMenu.Item($"{ItemBase}Slider.MinSeedDelay").GetValue<Slider>().Value, SMenu.Item($"{ItemBase}Slider.MaxSeedDelay").GetValue<Slider>().Value);
            }
        }

        private static readonly string[] SDelays =
        {
                 "LevelDelay", "EventDelay", "ItemDelay", "TrinketDelay"
        };
    }
}