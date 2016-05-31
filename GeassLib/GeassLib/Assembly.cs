using System;

namespace GeassLib
{
    public class Assembly
    {
        public Assembly()
        {
            _assemblyLoadTime = DateTime.Now;
        }

        private readonly DateTime _assemblyLoadTime;

        public float AssemblyTime() => (float)DateTime.Now.Subtract(_assemblyLoadTime).TotalMilliseconds;
    }
}