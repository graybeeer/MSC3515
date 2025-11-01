using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.Client;
using UnityEngine;


[CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/SlotCheckInvade", order = 11)]
public class ConditionSlotCheckInvade : ConditionData
{

    [Header("�ش� ī�尡 ���� ������ �ִ��� üũ(üũ�ϸ� ��� ����, üũ ���ϸ� �� ����)")]
    public bool opponent;
    [Space(10)]
    [Header("���� ���ڶ�����")]
    public bool deep;

    public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
    {
        BSlot now_slot = BSlot.Get(caster.slot);
        int CheckID;

        if (opponent)
            CheckID = GameClient.Get().GetOpponentID(caster.player_id);
        else
            CheckID = caster.player_id;

        if (now_slot.owner_p_id == CheckID)
            if (deep && now_slot.deep)
                return true;
            else if (!deep)
                return true;
            else
                return false;

        return false;
    }

}