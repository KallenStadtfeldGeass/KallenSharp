using System;
using System.Collections.Generic;
using SharpDX;

namespace S__Class_Tristana.Humanizer
{
    internal class TickManager
    {
        private float _randomMax;
        private float _randomMin;
        private readonly Random _rnd;

        public TickManager()
        {
            _rnd = new Random();
        }

        public void AddTick(string keyName, float min, float max)
        {
            if (IsPresent(keyName))
            {
                Console.WriteLine($"Key {keyName} already created");
                return;
            }

            Ticks.Add(keyName, new Tick(min, max));
        }

        public bool CheckTick(string key)
        {
            if (Ticks.ContainsKey(key))
                return Ticks[key].IsReady();

            Console.WriteLine($"Key {key} not found");
            return false;
        }

        public bool IsPresent(string key)
        {
            return Ticks.ContainsKey(key);
        }

        public void SetRandomizer(float min, float max)
        {
            _randomMin = min;
            _randomMax = max;
        }

        public void UseTick(string key)
        {
            if (Ticks.ContainsKey(key))
                Ticks[key].UseTick(_rnd.NextFloat(Ticks[key].GetMinDelay(), Ticks[key].GetMaxDelay()) + _rnd.NextFloat(_randomMin, _randomMax));

            Console.WriteLine($"Key {key} not found");
        }
        public readonly Dictionary<string, Tick> Ticks = new Dictionary<string, Tick>();
    }
}