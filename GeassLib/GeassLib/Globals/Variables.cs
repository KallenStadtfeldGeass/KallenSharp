using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeassLib.Globals
{
    public class Variables
    {
        public static float AssemblyTime() => (float)DateTime.Now.Subtract(Globals.Objects.AssemblyLoadTime).TotalMilliseconds;
        public static string AssemblyName;
    
        public static bool InCombo = false;
    }
}
