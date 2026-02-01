using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.Client;
using UnityEngine;


[CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/SlotCheckInvade", order = 11)]
public class ConditionSlotCheckInvade : ConditionData
{

    [Header("해당 카드가 누구 진영에 있는지 체크(체크하면 상대 진영, 체크 안하면 내 진영)")]
    public bool opponent;
    [Space(10)]
    [Header("진영 끝자락인지")]
    public bool deep;

    public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
    {
        SlotData now_slot_data = data.slotInform.GetSlotData(caster.slot);
        int CheckID;

        if (opponent)
            CheckID = GameClient.Get().GetOpponentID(caster.player_id);
        else
            CheckID = caster.player_id;

        if (now_slot_data.owner_p_id == CheckID)
            if (deep && now_slot_data.isDeep)
                return true;
            else if (!deep)
                return true;
            else
                return false;

        return false;
    }

}