using _Project_Geass.Global;
using System;

namespace _Project_Geass.Functions
{
    internal static class AssemblyTime
    {
        /// <summary>
        ///     Return current time
        /// </summary>
        /// <returns></returns>
        public static float CurrentTime()
            => (float) DateTime.Now.Subtract(StaticObjects.AssemblyLoadTime).TotalMilliseconds;
    }
}