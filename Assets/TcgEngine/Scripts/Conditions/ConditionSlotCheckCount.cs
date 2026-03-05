using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.Client;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;


[CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/SlotCheckCount", order = 11)]
public class ConditionSlotCheckCount : ConditionData
{
    [Header("어떤 슬롯")]
    public ConditionPlayerType playerType;
    public ConditionOperatorInt oper;
    public int count;

    public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
    {
        int temp_self_int = 0;
        int temp_opponent_int = 0;
        int temp_neutral_int = 0;

        foreach (SlotData temp in data.slotInform.GetSlotDataList())
        {
            if (temp.owner_p_id == caster.player_id && data.GetSlotCard(Slot.Get(temp.slot_x, temp.slot_y)) != null)
                temp_self_int++;
            if (temp.owner_p_id == data.GetOpponentID(caster.player_id) && data.GetSlotCard(Slot.Get(temp.slot_x, temp.slot_y)) != null)
                temp_opponent_int++;
            if (temp.owner_p_id == data.GetPlayerNeutralID() && data.GetSlotCard(Slot.Get(temp.slot_x, temp.slot_y)) != null)
                temp_neutral_int++;
        }

        if (playerType == ConditionPlayerType.Self)
            return CompareInt(temp_self_int, oper, count);
        if (playerType == ConditionPlayerType.Opponent)
            return CompareInt(temp_opponent_int, oper, count);
        if (playerType == ConditionPlayerType.Netural)
            return CompareInt(temp_neutral_int, oper, count);
        else
            Debug.LogError("condition slot check 오류"); return false;
    }

}