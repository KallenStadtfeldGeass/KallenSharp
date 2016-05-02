using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Humanizer
{
    class Tick : Core
    {
        private float MinDelay;
        private float MaxDelay;
        private float NextTick;

        public void SetMinAndMax(float min, float max)
        {
            MinDelay = min;
            MaxDelay = max;
        }

        public void UseTick(float next)
        {
            NextTick = _Time.TickCount() + next;
        }

        public bool IsReady()
        {
            return _Time.TickCount() > NextTick;
        }

        public float GetMinDelay()
        {
            return MinDelay;
        }

        public float GetMaxDelay()
        {
            return MaxDelay;
        }

        public Tick(float min = 0,  float max = 100)
        {
            NextTick = _Time.TickCount();
            MinDelay = min;
            MaxDelay = max;
        }

    }
}
