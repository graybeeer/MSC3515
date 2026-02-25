using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using Unity.Mathematics;
using UnityEngine;
namespace TcgEngine
{
    /// <summary>
    /// Change owner of target card to the owner of the caster (or the opponent player)
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/ChangeStatEach", order = 10)]
    public class EffectChangeStatEach : EffectData
    {
        [Header("교환할 스탯")]
        public EffectStatType type;
        // Start is called before the first frame update
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target) 
        {
            if (type == EffectStatType.HP)
                (caster.hp, target.hp) = (target.hp, caster.hp);
            if (type == EffectStatType.Mana)
                (caster.mana, target.mana) = (target.mana, caster.mana);
            if (type == EffectStatType.Arrow)
            {
                (caster.card_arrow, target.card_arrow) = (target.card_arrow, caster.card_arrow);

                int target_cursed = target.GetStatusValue(StatusType.cursed);
                int target_hasted = target.GetStatusValue(StatusType.hasted);
                int caster_cursed = caster.GetStatusValue(StatusType.cursed);
                int caster_hasted = caster.GetStatusValue(StatusType.hasted);

                target.RemoveStatus(StatusType.cursed); //기존 헤이스트랑 저주 데이터 지우기
                target.RemoveStatus(StatusType.hasted);
                caster.RemoveStatus(StatusType.cursed);
                caster.RemoveStatus(StatusType.hasted);

                if (caster_cursed != 0)
                    target.AddStatus(StatusType.cursed, caster_cursed, 0);
                if (caster_hasted != 0)
                    target.AddStatus(StatusType.hasted, caster_hasted, 0);
                if (target_cursed != 0)
                    caster.AddStatus(StatusType.cursed, target_cursed, 0);
                if (target_hasted != 0)
                    caster.AddStatus(StatusType.hasted, target_hasted, 0);
            }
        }
    }
}