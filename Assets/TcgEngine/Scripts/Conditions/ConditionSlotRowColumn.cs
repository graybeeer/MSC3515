using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TcgEngine
{
    /// <summary>
    /// SlotRange check each axis variable individualy for range between the caster and target
    /// If you want to check the travel distance instead (all at once) use SlotDist
    /// </summary>

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/SlotRowColumn", order = 11)]
    public class ConditionSlotRowColumn : ConditionData
    {
        [Header("시전자와 같은 행 열 확인")]
        public RowColumn type;
        public ConditionOperatorBool oper;
        // Start is called before the first frame update
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            if (type == RowColumn.Row)
                return CompareBool(target.slot.y == caster.slot.y, oper);
            if (type == RowColumn.Column)
                return CompareBool(target.slot.x == caster.slot.x, oper);
            if (type == RowColumn.RowColumn)
                return CompareBool(target.slot.x == caster.slot.x || target.slot.y == caster.slot.y, oper);
            else return false;
        }

        // Update is called once per frame
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            if (type == RowColumn.Row)
                return CompareBool(target.y == caster.slot.y, oper);
            if (type == RowColumn.Column)
                return CompareBool(target.x == caster.slot.x, oper);
            if (type == RowColumn.RowColumn)
                return CompareBool(target.x == caster.slot.x || target.y == caster.slot.y, oper);
            else return false;
        }

    }
}
public enum RowColumn
{
    Row, //행
    Column, //열
    RowColumn, //같은 행렬 동시에
}