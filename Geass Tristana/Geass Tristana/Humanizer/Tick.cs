﻿using Geass_Tristana.Other;

namespace Geass_Tristana.Humanizer
{
    internal class Tick : Core
    {
        private float _maxDelay;
        private float _minDelay;
        private float _nextTick;

        public Tick(float min = 0, float max = 100)
        {
            _nextTick = AssemblyTime();
            _minDelay = min;
            _maxDelay = max;
        }

        public float GetMaxDelay()
        {
            return _maxDelay;
        }

        public float GetMinDelay()
        {
            return _minDelay;
        }

        public bool IsReady()
        {
            return AssemblyTime() > _nextTick;
        }

        public void SetMinAndMax(float min, float max)
        {
            _minDelay = min;
            _maxDelay = max;
        }

        public void UseTick(float next)
        {
            _nextTick = AssemblyTime() + next;
        }
    }
}