using System;

namespace GeassLib
{
    internal class Assembly
    {
        public Assembly()
        {
            _assemblyLoadTime = DateTime.Now;
        }

        private readonly DateTime _assemblyLoadTime;

        public float AssemblyTime() => (float)DateTime.Now.Subtract(_assemblyLoadTime).TotalMilliseconds;
    }
}