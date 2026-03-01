using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TcgEngine
{

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/Memoried", order = 1)]
    public class ConditionMemoried : ConditionData
    {
        [Header("Card is memoried")]
        public ConditionOperatorBool oper;

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            return CompareBool(ability.memory_card_uid.Contains(target.uid), oper);
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            return CompareBool(ability.memory_player.Contains(target), oper);
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            return CompareBool(ability.memory_slot.Contains(target), oper);
        }
    }
}