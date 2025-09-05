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
        public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
        {
            if (caster.is_playing_hand)
                return true; //Override this, applies to any target, always checked
            else return false;
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            if (caster.is_playing_hand)
                return true; //Override this, applies to any target, always checked
            else return false;
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            if (caster.is_playing_hand)
                return true; //Override this, applies to any target, always checked
            else return false;
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            if (caster.is_playing_hand)
                return true; //Override this, applies to any target, always checked
            else return false;
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, CardData target)
        {
            if (caster.is_playing_hand)
                return true; //Override this, applies to any target, always checked
            else return false;
        }
        
    }
}

