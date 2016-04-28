using LeagueSharp.Common;
using SharpDX;
using System.Collections.Generic;

namespace S__Class_Tristana.Data
{
    sealed class Monster
    {
        public struct MonsterBar
        {
            readonly int BarWidth;
            readonly int XOffset;
            readonly int YOffsetBegin;
            readonly int YOffsetEnd;

            public MonsterBar(int barWidth, int xoffset, int yOffsetBegin, int yOffsetEnd)
            {
                BarWidth = barWidth;
                XOffset = xoffset;
                YOffsetBegin = yOffsetBegin;
                YOffsetEnd = yOffsetEnd;
            }
        }

        public Dictionary<string, Vector2> MonsterLocations = new Dictionary<string, Vector2>()
        {
            {"Neutral.Dragon",SummonersRift.River.Dragon},
            {"Neutral.Baron",SummonersRift.River.Baron},

            {"Chaos.Red",new Vector2(7016.869f, 10775.55f)},
            {"Chaos.Blue",new Vector2(10931.73f, 6990.844f)},

            {"Order.Red",new Vector2(7862.244f, 4111.187f)},
            {"Order.Blue",new Vector2(3871.489f, 7901.054f)}
        };

        public Dictionary<string, MonsterBar> MonsterBarDictionary = new Dictionary<string, MonsterBar>()
        {
            {"SRU_Red",new MonsterBar(145,3,18,10)},
            {"SRU_Blue",new MonsterBar(145,3,18,10)},
            {"SRU_Dragon",new MonsterBar(145,3,18,10)},
            {"SRU_Baron",new MonsterBar(194,-22,13,16)},
            {"SRU_Crab",new MonsterBar(61,45,34,3)},
            {"SRU_Krug",new MonsterBar(81,58,18,4)},
            {"SRU_Gromp",new MonsterBar(87,62,18,4)},
            {"SRU_Murkwolf",new MonsterBar(75,54,19,4)},
            {"SRU_Razorbeak",new MonsterBar(75,54,18,4)}
        };

    }
}
