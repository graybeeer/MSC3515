using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{
    /// <summary>
    /// 적이 있으면 돌진, 없으면 이동
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Rush", order = 3)]
    public class EffectRush : EffectData
    {
        public bool exhasuted;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            logic.FMoveOrAttackSlot(caster, target.slot, true);
            if (exhasuted)
                caster.exhausted = true;
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Slot target)
        {
            logic.FMoveOrAttackSlot(caster, target, true);
            if (exhasuted)
                caster.exhausted = true;
        }
    }
}