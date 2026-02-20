using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using UnityEngine;

namespace TcgEngine
{
    //보드 위 카드 장수만큼 드로우 효과 구현
    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/AddStatCount", order = 10)]
    public class EffectAddStatCount : EffectData
    {
        public EffectStatType type;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            if (type == EffectStatType.Attack)
                caster.attack += ability.value;
            if (type == EffectStatType.HP)
                caster.hp += ability.value;
            if (type == EffectStatType.Mana)
                caster.mana += ability.value;
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Slot target)
        {
            if (type == EffectStatType.Attack)
                caster.attack += ability.value;
            if (type == EffectStatType.HP)
                caster.hp += ability.value;
            if (type == EffectStatType.Mana)
                caster.mana += ability.value;
        }
        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            if (type == EffectStatType.Attack)
                caster.attack_ongoing += ability.value;
            if (type == EffectStatType.HP)
                caster.hp_ongoing += ability.value;
            if (type == EffectStatType.Mana)
                caster.mana_ongoing += ability.value;
        }
    }

}