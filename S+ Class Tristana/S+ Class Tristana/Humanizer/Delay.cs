using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;


namespace S__Class_Tristana.Humanizer
{
    class Delay : Core
    {
        // ReSharper disable once InconsistentNaming
        private readonly Random _random = new Random();
        private float _lastAttackTick, _lastMoveTick;
        private float _currentAttackDelay = 500f;
        private float _currentMoveDelay = 500f;
        public long BlockedCommands;

        public const string MenuNameBase = ".Humanizer Menu";
        private const string MenuItemBase = ".Humanizer.";

        public const string DelayMenuNameBase = ".Delays.";
        public const string DelayItemBase = MenuItemBase + "Delays.";

        public class Limiter
        {
            private readonly Random Rand = new Random();
            public readonly Dictionary<string, NewLevelShit> Delays = new Dictionary<string, NewLevelShit>();

            private float _fMin = 0f, _fMax = 250f;

            public struct NewLevelShit
            {
                public readonly float Delay;
                public readonly float LastTick;

                public NewLevelShit(float delay, float lastTick)
                {
                    Delay = delay;
                    LastTick = lastTick;
                }
            }


            private string[] _sDelays =
            {
                 "LevelDelay", "EventDelay", "ItemDelay", "TrinketDelay"
            };
       
            private void LoadDelays()
            {

                try
                {
                    foreach (var sDelay in _sDelays.Where(sDelay => !Delays.ContainsKey(sDelay)))
                    {
                        Delays.Add($"{Delay.DelayItemBase}Slider.{sDelay}",
                            new NewLevelShit(
                                SMenu.Item($"{Delay.DelayItemBase}Slider.{sDelay}").GetValue<Slider>().Value, 0f));
                    }
                }
                catch
                {
                    //Fuck her right in the pussy
                }
                finally
                {
                    _fMin = SMenu.Item($"{Delay.DelayItemBase}Slider.MinSeedDelay").GetValue<Slider>().Value;
                    _fMax = SMenu.Item($"{Delay.DelayItemBase}Slider.MaxSeedDelay").GetValue<Slider>().Value;
                }

            }

            public bool CheckDelay(string key)
            {
                if (Delays.ContainsKey(key))
                    return Delays[key].LastTick - _Time.TickCount() < Delays[key].Delay;

                LoadDelays();

                return false;
            }

            public void UseTick(string key)
            {
                Delays[key] = new NewLevelShit(Delays[key].Delay,
                  _Time.TickCount() + Rand.NextFloat(_fMin, _fMax)); //Randomize delay
            }
        }
    }
}
