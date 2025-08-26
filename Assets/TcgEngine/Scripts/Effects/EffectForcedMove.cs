using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{
    /// <summary>
    /// Effect that adds an ability to a card
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/ForcedMove", order = 10)]

    public class EffectForcedMove : EffectData
    {

        //추가예정- 기본적으로는 value값으로 움직이나, 이동 가능한 방향에서 무작위로 움직이는 옵션도 있게
        public bool Random_Move;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            //선택한 보드 카드를 능력 value 키패드 방향으로 강제이동
            logic.ForcedMoveCard(target, Slot.GetArrowPointsSlot(target.slot, ability.value, caster.player_id));
            //target.exhausted = true; //강제이동후 카드는 탈진되서 이동 공격 못함 추가-연계 이펙트로 모든 자신 유닛 탈진상태 효과 넣기
        }

    }
}