using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TcgEngine
{
    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/IsPlayingHand", order = 1)]
    public class ConditionIsPlayingHand : ConditionData
    {
        [Header("패에서 발동한 카드인지")]
        public ConditionOperatorBool oper;
        public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
        {
            return CompareBool(caster.is_playing_hand, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            return CompareBool(caster.is_playing_hand, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            return CompareBool(caster.is_playing_hand, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            return CompareBool(caster.is_playing_hand, oper);
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, CardData target)
        {
            return CompareBool(caster.is_playing_hand, oper);
        }
        
    }
}

