using System.Collections;
using System.Collections.Generic;
using TcgEngine.Client;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TcgEngine
{
    /// <summary>
    /// Condition that checks the card data matches
    /// </summary>

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/SlotOwnerPlayer", order = 10)]
    public class ConditionBoardSlotPlayer : ConditionData
    {
        [Header("Slot is")]
        public SlotType BoardSlotType;

        public ConditionOperatorBool oper;

        public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
        {
            int caster_id = caster.player_id;
            int target_id = data.slotInform.GetSlotData(caster.slot).owner_p_id;

            if (BoardSlotType == SlotType.PlayerNeutral)
                return CompareBool(target_id == data.GetPlayerNeutralID(), oper);
            else if (BoardSlotType == SlotType.PlayerSelf)
                return CompareBool(target_id == caster_id, oper);
            else if (BoardSlotType == SlotType.PlayerOpponent)
                return CompareBool(target_id != data.GetPlayerNeutralID() && target_id != caster_id, oper);
            else return false;
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            int caster_id = caster.player_id;
            int target_id = data.slotInform.GetSlotData(target.slot).owner_p_id;

            if (BoardSlotType == SlotType.PlayerNeutral)
                return CompareBool(target_id == data.GetPlayerNeutralID(), oper);
            else if (BoardSlotType == SlotType.PlayerSelf)
                return CompareBool(target_id == caster_id, oper);
            else if (BoardSlotType == SlotType.PlayerOpponent)
                return CompareBool(target_id != data.GetPlayerNeutralID() && target_id != caster_id, oper);
            else return false;
        }


        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            int caster_id = caster.player_id;
            int target_id = data.slotInform.GetSlotData(target).owner_p_id;

            if (BoardSlotType == SlotType.PlayerNeutral)
                return CompareBool(target_id == data.GetPlayerNeutralID(), oper);
            else if (BoardSlotType == SlotType.PlayerSelf)
                return CompareBool(target_id == caster_id, oper);
            else if (BoardSlotType == SlotType.PlayerOpponent)
                return CompareBool(target_id != data.GetPlayerNeutralID() && target_id != caster_id, oper);
            else return false;
        }
    }
}
public enum SlotType
{
    PlayerNeutral = -1,
    PlayerSelf = 0, 
    PlayerOpponent = 1, 
}