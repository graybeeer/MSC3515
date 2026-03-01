using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{
    /// <summary>
    /// Effect that adds an ability to a card
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/AddAbility", order = 10)]
    public class EffectAddAbility : EffectData
    {
        [Header("현재 턴 저장됨 + 거기에 ability 밸류값이 플러스됨")]
        public AbilityData gain_ability;

        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            target.AddAbility(gain_ability, ability.value + logic.GameData.turn_count);
        }

        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            target.AddOngoingAbility(gain_ability);
        }
    }
}
public enum check_addability
{
    not,
    player,
    card,
    slot
}