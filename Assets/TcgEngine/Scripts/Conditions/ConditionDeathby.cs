using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TcgEngine
{
    /// <summary>
    /// Condition that check if the CardData is a valid deckbuilding card (not a summon token)
    /// </summary>
    
    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/Deathby", order = 10)]
    public class ConditionDeathby : ConditionData
    {
        [Header("caster가 해당 카드에게 사망했는지 체크. 죽메-자길 죽인 유닛에게 발동 용")]
        public ConditionOperatorBool oper;

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            return CompareBool(caster.dead_by_uid == target.uid, oper);
        }

    }
}