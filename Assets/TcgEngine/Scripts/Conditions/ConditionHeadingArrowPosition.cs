using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TcgEngine
{

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/HeadingArrowPosition", order = 10)]
    public class ConditionHeadingArrowPosition : ConditionData
    {
        [Header("해당 마커들이 향하는 곳에 위치하는지 condition")]
        public bool[] arrows = new bool[9];
        [Header("체크할 경우 위의 마커번호는 무시하고 카드의 이동마커로 위치 체크")]
        public bool cardArrow;

        public ConditionOperatorBool oper;
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {

            if (cardArrow)
                return CompareBool(data.HeadingMoveArrow(caster, target.slot), oper);
            else
            {
                return CompareBool(data.HeadingMoveArrow(caster, target.slot, arrows), oper);
            }
        }
        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            if (cardArrow)
                return CompareBool(data.HeadingMoveArrow(caster, target), oper);
            else
            {
                return CompareBool(data.HeadingMoveArrow(caster, target, arrows), oper);
            }
        }

    }
}