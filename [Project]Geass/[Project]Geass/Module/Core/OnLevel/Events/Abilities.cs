using _Project_Geass.Data;
using _Project_Geass.Globals;
using _Project_Geass.Humanizer;
using LeagueSharp;
using System;

namespace _Project_Geass.Module.Core.OnLevel.Events
{
    internal class Abilities
    {
        private readonly int[] _abilitySequences;

        public Abilities(int[] sequence)
        {
            _abilitySequences = sequence;
            Game.OnUpdate += OnUpdate;
        }

        private void OnUpdate(EventArgs args)
        {
            if (DelayHandler.CheckOnLevel())
            {
                if (!Static.Objects.ProjectMenu.Item(Names.Menu.LevelItemBase + "Boolean.AutoLevelUp").GetValue<bool>())
                    return;
                DelayHandler.UseOnLevel();

                LevelUpSpells();
            }
        }

        private void LevelUpSpells()
        {
            var qL = Static.Objects.Player.Spellbook.GetSpell(SpellSlot.Q).Level + _qOff;
            var wL = Static.Objects.Player.Spellbook.GetSpell(SpellSlot.W).Level + _wOff;
            var eL = Static.Objects.Player.Spellbook.GetSpell(SpellSlot.E).Level + _eOff;
            var rL = Static.Objects.Player.Spellbook.GetSpell(SpellSlot.R).Level + _rOff;

            if (qL + wL + eL + rL >= Static.Objects.Player.Level) return;

            int[] level = { 0, 0, 0, 0 };

            for (var i = 0; i < Static.Objects.Player.Level; i++)
                level[_abilitySequences[i] - 1] = level[_abilitySequences[i] - 1] + 1;

            if (qL < level[0]) Static.Objects.Player.Spellbook.LevelSpell(SpellSlot.Q);
            if (wL < level[1]) Static.Objects.Player.Spellbook.LevelSpell(SpellSlot.W);
            if (eL < level[2]) Static.Objects.Player.Spellbook.LevelSpell(SpellSlot.E);
            if (rL < level[3]) Static.Objects.Player.Spellbook.LevelSpell(SpellSlot.R);
        }

#pragma warning disable RECS0122 // Initializing field with default value is redundant
        private readonly int _qOff = 0;
        private readonly int _wOff = 0;
        private readonly int _eOff = 0;
        private readonly int _rOff = 0;
#pragma warning restore RECS0122 // Initializing field with default value is redundant
    }
}