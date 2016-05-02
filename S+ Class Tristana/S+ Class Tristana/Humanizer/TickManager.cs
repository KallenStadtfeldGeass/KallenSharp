using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Humanizer
{
    class TickManager
    {
        public readonly Dictionary<string, Tick> Ticks = new Dictionary<string, Tick>();
        private Random rnd;
        private float RandomMin;
        private float RandomMax;

        public TickManager()
        {
            rnd = new Random();
        }
        public void SetRandomizer(float min,float max)
        {
            RandomMin = min;
            RandomMax = max;
        }

        public bool IsPresent(String key)
        {
            return Ticks.ContainsKey(key);
        }

        public void AddTick(String KeyName,float min, float max)
        {
            if (IsPresent(KeyName))
            {
                Console.WriteLine("Key {0} already created", KeyName);
                return;
            }

           Ticks.Add(KeyName, new Tick(min, max));
        }

        public bool CheckTick(string key)
        {
            if (Ticks.ContainsKey(key))
                return Ticks[key].IsReady();

            Console.WriteLine("Key {0} not found",key);
            return false;

        }

        public void UseTick(string key)
        {
            if (Ticks.ContainsKey(key))
                Ticks[key].UseTick(rnd.NextFloat(Ticks[key].GetMinDelay(), Ticks[key].GetMaxDelay()) + rnd.NextFloat(RandomMin, RandomMax));

            
            Console.WriteLine("Key {0} not found", key);
        }
    }
}
