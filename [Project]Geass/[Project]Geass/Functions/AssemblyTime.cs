using _Project_Geass.Globals;
using System;

namespace _Project_Geass.Functions
{
    internal static class AssemblyTime
    {
        /// <summary>
        /// Return current time
        /// </summary>
        /// <returns></returns>
        public static float CurrentTime() => (float)DateTime.Now.Subtract(Static.Objects.AssemblyLoadTime).TotalMilliseconds;
    }
}