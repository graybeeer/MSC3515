using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TcgEngine
{
    
    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/DeadNaturally", order = 10)]
    public class ConditionDeadNaturally : ConditionData
    {
        [Header("caster가 턴 시작시에 파괴됐는지 확인")]
        public ConditionOperatorBool oper;

        public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
        {
            return CompareBool(caster.destroied_naturally, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            return CompareBool(caster.destroied_naturally, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            return CompareBool(caster.destroied_naturally, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, CardData target)
        {
            return CompareBool(caster.destroied_naturally, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            return CompareBool(caster.destroied_naturally, oper);
        }

    }
}