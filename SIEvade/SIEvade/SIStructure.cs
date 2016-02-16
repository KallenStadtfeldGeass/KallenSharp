namespace SIEvade
{
    class Evade
    {
        public struct Base
        {
            public short EvadeMode;
            public bool UseEvade;
            public bool UseEvadeSkills;

            public SpellsSettings SpellSetting;
            public TimeSettings TimeSetting;
            public OtherSettings OtherSetting;

            public Base(SpellsSettings spellSetting, TimeSettings timeSetting, OtherSettings otherSetting,short evadeMode, bool useEvade,bool useEvadeSkills)
            {
                SpellSetting = spellSetting;
                TimeSetting = timeSetting;
                OtherSetting = otherSetting;
                EvadeMode = evadeMode;
                UseEvade = useEvade;
                UseEvadeSkills = useEvadeSkills;
            }
        }

        public struct SpellsSettings
        {
            public bool DodgeDangerous;
            public bool DodgeCircular;
            public bool DodgeFog;

            public SpellsSettings(bool dodgeDanerous, bool dodgeCircular, bool dodgeFog)
            {
                DodgeDangerous = dodgeDanerous;
                DodgeCircular = dodgeCircular;
                DodgeFog = dodgeFog;
            }
        }

        public struct TimeSettings
        {
            public float ReactionTime;
            public float TickTime;
            public float DetectionTime;

            public TimeSettings(float reactionTime, float tickTime, float detectionTime)
            {
                ReactionTime = reactionTime;
                TickTime = tickTime;
                DetectionTime = detectionTime;
            }
        }

        public struct OtherSettings
        {
            public bool ClickOnce;
            public bool FastMove;
            public bool ContinueMovement;
            public bool SpellColision;

            public OtherSettings(bool clickOnce, bool fastMove, bool continueMovement, bool spellColision)
            {
                ClickOnce = clickOnce;
                FastMove = fastMove;
                ContinueMovement = continueMovement;
                SpellColision = spellColision;
            }
        }     
    }
}
