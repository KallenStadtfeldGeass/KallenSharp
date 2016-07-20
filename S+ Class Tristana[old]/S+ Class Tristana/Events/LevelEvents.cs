using LeagueSharp;
using S__Class_Tristana.Other;
using System;

namespace S__Class_Tristana.Events
{
    internal class LevelEvents : Core
    {
        public const string MenuItemBase = ".Level.";
        public const string MenuNameBase = ".Level Menu";

        private void LevelUpSpells()
        {
            var qL = Champion.Player.Spellbook.GetSpell(Champion.GetSpellQ.Slot).Level + _qOff;
            var wL = Champion.Player.Spellbook.GetSpell(Champion.GetSpellW.Slot).Level + _wOff;
            var eL = Champion.Player.Spellbook.GetSpell(Champion.GetSpellE.Slot).Level + _eOff;
            var rL = Champion.Player.Spellbook.GetSpell(Champion.GetSpellR.Slot).Level + _rOff;

            if (qL + wL + eL + rL >= Champion.Player.Level) return;

            int[] level = { 0, 0, 0, 0 };

            for (var i = 0; i < Champion.Player.Level; i++)
                level[_level.AbilitySequence[i] - 1] = level[_level.AbilitySequence[i] - 1] + 1;

            if (qL < level[0]) Champion.Player.Spellbook.LevelSpell(SpellSlot.Q);
            if (wL < level[1]) Champion.Player.Spellbook.LevelSpell(SpellSlot.W);
            if (eL < level[2]) Champion.Player.Spellbook.LevelSpell(SpellSlot.E);
            if (rL < level[3]) Champion.Player.Spellbook.LevelSpell(SpellSlot.R);
        }

        public void OnUpdate(EventArgs args)
        {
            if (!TickManager.CheckTick($"{HumanizeEvents.ItemBase}Slider.LevelDelay")) return;

            TickManager.UseTick($"{HumanizeEvents.ItemBase}Slider.LevelDelay");

            if (_lastLevel != Champion.Player.Level)
            {
                Champion.UpdateChampionRange(Champion.Player.Level);
                _lastLevel = Champion.Player.Level;
            }

            if (SMenu.Item(MenuItemBase + "Boolean.AutoLevelUp").GetValue<bool>())
                LevelUpSpells();
        }

        private int _lastLevel = 1;
        private readonly Data.Level _level = new Data.Level();

#pragma warning disable RECS0122 // Initializing field with default value is redundant
        private readonly int _qOff = 0;
        private readonly int _wOff = 0;
        private readonly int _eOff = 0;
        private readonly int _rOff = 0;
#pragma warning restore RECS0122 // Initializing field with default value is redundant
    }
}