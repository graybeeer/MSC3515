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

        //�߰�����- �⺻�����δ� value������ �����̳�, �̵� ������ ���⿡�� �������� �����̴� �ɼǵ� �ְ�
        public bool Random_Move;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            //������ ���� ī�带 �ɷ� value Ű�е� �������� �����̵�
            logic.ForcedMoveCard(target, Slot.GetArrowPointsSlot(target.slot, ability.value, caster.player_id));
            //target.exhausted = true; //�����̵��� ī��� Ż���Ǽ� �̵� ���� ���� �߰�-���� ����Ʈ�� ��� �ڽ� ���� Ż������ ȿ�� �ֱ�
        }

    }
}