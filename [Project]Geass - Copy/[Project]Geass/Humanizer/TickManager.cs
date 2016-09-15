﻿using SharpDX;
using System;
using System.Collections.Generic;

namespace _Project_Geass.Humanizer
{
    internal class TickManager
    {
        /// <summary>
        /// The random maximum
        /// </summary>
        private float _randomMax;

        /// <summary>
        /// The random minimum
        /// </summary>
        private float _randomMin;

        /// <summary>
        /// The RNG
        /// </summary>
        private readonly Random _rng;

        /// <summary>
        /// The ticks
        /// </summary>
        public readonly Dictionary<string, Tick> Ticks = new Dictionary<string, Tick>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TickManager"/> class.
        /// </summary>
        public TickManager()
        {
            _rng = new Random();
        }

        /// <summary>
        /// Adds the tick.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public void AddTick(string keyName, float min, float max)
        {
            if (keyName.Length <= 0)
                Console.WriteLine("Add Key can not be null");

            if (IsPresent(keyName))
            {
                Console.WriteLine($"Key {keyName} already created");
                return;
            }

            Ticks.Add(keyName, new Tick(min, max));
        }

        /// <summary>
        /// Checks the tick.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool CheckTick(string key)
        {
            if (key.Length <= 0)
                Console.WriteLine("Check Key an not be null");
            if (Ticks.ContainsKey(key))
                return Ticks[key].IsReady(Functions.AssemblyTime.CurrentTime());

            Console.WriteLine($"Key {key} not found");
            return false;
        }

        /// <summary>
        /// Determines whether the specified key is present.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key is present; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPresent(string key)
        {
            return Ticks.ContainsKey(key);
        }

        /// <summary>
        /// Sets the randomizer.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public void SetRandomizer(float min, float max)
        {
            _randomMin = min;
            _randomMax = max;
        }

        /// <summary>
        /// Uses the tick.
        /// </summary>
        /// <param name="key">The key.</param>
        public void UseTick(string key)
        {
            if (key.Length <= 0) return;

            if (Ticks.ContainsKey(key))
                Ticks[key].UseTick(Functions.AssemblyTime.CurrentTime(),
                    _rng.NextFloat(Ticks[key].GetMinDelay(), Ticks[key].GetMaxDelay()) +
                    _rng.NextFloat(_randomMin, _randomMax));
            else
                Console.WriteLine($"Key {key} not found");
        }
    }
}