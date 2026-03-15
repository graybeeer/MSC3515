using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.Client;
using UnityEngine;


[CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/SlotCheckDeep", order = 11)]
public class ConditionSlotDeep : ConditionData
{
    [Header("진영 끝자락인지")] 
    public ConditionOperatorBool oper;
    public ConditionEffectTrigger trigger;

    public override bool IsTriggerConditionMet(Game data, AbilityData ability, Card caster)
    {
        if (trigger == ConditionEffectTrigger.Caster)
            return CompareBool(data.slotInform.GetSlotData(caster.slot).isDeep, oper);
        else return false;
    }
    public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
    {
        if (trigger == ConditionEffectTrigger.Caster)
            return CompareBool(data.slotInform.GetSlotData(caster.slot).isDeep, oper);
        if (trigger == ConditionEffectTrigger.Target)
            return CompareBool(data.slotInform.GetSlotData(target.slot).isDeep, oper);
        else return false;
    }
    public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
    {
        if (trigger == ConditionEffectTrigger.Caster)
            return CompareBool(data.slotInform.GetSlotData(caster.slot).isDeep, oper);
        if (trigger == ConditionEffectTrigger.Target)
            return CompareBool(data.slotInform.GetSlotData(target).isDeep, oper);
        else return false;
    }

}