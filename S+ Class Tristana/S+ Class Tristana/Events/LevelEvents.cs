using LeagueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Events
{
    class LevelEvents : Core
    {
        public const string _MenuNameBase = ".Level Menu";
        public const string _MenuItemBase = ".Level.";
        private int lastLevel = 1;
        private Data.Level _Level = new Data.Level();
        public void OnUpdate(EventArgs args)
        {
            if (!_Limiter.CheckDelay($"{Humanizer.Delay.DelayItemBase}Slider.LevelDelay")) return;

            _Limiter.UseTick($"{Humanizer.Delay.DelayItemBase}Slider.LevelDelay");

            if (lastLevel != _Champion.Player.Level)
            {
                _Champion.UpdateChampionRange(_Champion.Player.Level);
                lastLevel = _Champion.Player.Level;
            }

            if (SMenu.Item(_MenuItemBase + "Boolean.AutoLevelUp").GetValue<bool>())
                LevelUpSpells();
        }


        private int QOff = 0;
        private int WOff = 0;
        private int EOff = 0;
        private int ROff = 0;

        private void LevelUpSpells()
        {
            var qL = _Champion.Player.Spellbook.GetSpell(_Champion.GetSpellQ().Slot).Level + QOff;
            var wL = _Champion.Player.Spellbook.GetSpell(_Champion.GetSpellW().Slot).Level + WOff;
            var eL = _Champion.Player.Spellbook.GetSpell(_Champion.GetSpellE().Slot).Level + EOff;
            var rL = _Champion.Player.Spellbook.GetSpell(_Champion.GetSpellR().Slot).Level + ROff;

            if (qL + wL + eL + rL >= _Champion.Player.Level) return;

            int[] level = { 0, 0, 0, 0 };

             for (var i = 0; i < _Champion.Player.Level; i++)
             {
                level[_Level.AbilitySequence[i] - 1] = level[_Level.AbilitySequence[i] - 1] + 1;
             }
            
            if (qL < level[0]) _Champion.Player.Spellbook.LevelSpell(SpellSlot.Q);
            if (wL < level[1]) _Champion.Player.Spellbook.LevelSpell(SpellSlot.W);
            if (eL < level[2]) _Champion.Player.Spellbook.LevelSpell(SpellSlot.E);
            if (rL < level[3]) _Champion.Player.Spellbook.LevelSpell(SpellSlot.R);


        }
    }
}
