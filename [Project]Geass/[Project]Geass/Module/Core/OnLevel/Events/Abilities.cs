using _Project_Geass.Data;
using _Project_Geass.Globals;
using _Project_Geass.Humanizer;
using LeagueSharp;
using System;
using LeagueSharp.Common;
using _Project_Geass.Data.Champions;

namespace _Project_Geass.Module.Core.OnLevel.Events
{
    internal class Abilities : SettingsBase
    {
        private readonly int[] _abilitySequences;
        private readonly Random _rng;
        private int _lastLevel;

        public Abilities(int[] sequence)
        {
            _abilitySequences = sequence;
            _rng = new Random();
            Obj_AI_Base.OnLevelUp += Obj_AI_Base_OnLevelUp;
            Game.OnUpdate += OnUpdate;

        }

        public void OnUpdate(EventArgs args)
        {
            if (DelayHandler.CheckOnLevel())
                if (Static.Objects.Player.Level == 1)
                {
                    if (Static.Objects.ProjectMenu.Item(Names.Menu.LevelItemBase + "Boolean.AutoLevelUp").GetValue<bool>())
                        LevelUpSpells();

                    Game.OnUpdate -= OnUpdate;
                }
            DelayHandler.UseOnLevel();
        }

        private void Obj_AI_Base_OnLevelUp(Obj_AI_Base sender, EventArgs args)
        {
            if (!sender.IsMe || !Static.Objects.ProjectMenu.Item(Names.Menu.LevelItemBase + "Boolean.AutoLevelUp").GetValue<bool>())
                return;

            Utility.DelayAction.Add(_rng.Next(50,200) - Game.Ping, LevelUpSpells);
        }

        private void LevelUpSpells()
        {

            while (_lastLevel != Static.Objects.Player.Level)
            {
                var ability = _abilitySequences[_lastLevel];
                if (ability.Equals(Q))
                    Static.Objects.Player.Spellbook.LevelUpSpell(SpellSlot.Q);
                else if (ability.Equals(W))
                    Static.Objects.Player.Spellbook.LevelUpSpell(SpellSlot.W);
                else if (ability.Equals(E))
                    Static.Objects.Player.Spellbook.LevelUpSpell(SpellSlot.E);
                else if (ability.Equals(R))
                    Static.Objects.Player.Spellbook.LevelUpSpell(SpellSlot.R);

                _lastLevel++;
            }

        }

    }
}