using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeassLib.Functions.Logging;
using LeagueSharp;
using LeagueSharp.Common;

namespace GeassLib.Globals
{
    class Objects
    {
        public static Menu GeassLibMenu { get; set; }
        public static Obj_AI_Hero Player { get; set; }
        public static DateTime AssemblyLoadTime;
        public static Logger Logger;
    }
}
