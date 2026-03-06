using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TcgEngine
{

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/MemoriedTurn_legacy", order = 99)]
    public class ConditionMemoriedTurn : ConditionData
    {
        //[Header("현재 어빌리티에 기억된 턴인지")]
        /*
        public ConditionOperatorBool oper;
        public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
        {
            //return CompareBool(data.turn_count == ability.memory_turn[0], oper);
        }
        */
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            return false;
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            return false;
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            return false;
        }
    }
}