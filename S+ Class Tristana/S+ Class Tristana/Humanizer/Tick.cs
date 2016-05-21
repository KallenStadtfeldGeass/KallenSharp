using S__Class_Tristana.Other;
using System;

namespace S__Class_Tristana.Humanizer
{
    internal class Tick : Core
    {
        private float _maxDelay;
        private float _minDelay;
        private float _nextTick;

        public Tick(float min = 0, float max = 100)
        {
            _nextTick = TickCount();
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

        public float TickCount()
        {
            return (float)DateTime.Now.Subtract(_assemblyLoadTime).TotalMilliseconds;
        }

        public void UseTick(float next)
        {
            _nextTick = AssemblyTime() + next;
        }

        private readonly DateTime _assemblyLoadTime = DateTime.Now;
    }
}