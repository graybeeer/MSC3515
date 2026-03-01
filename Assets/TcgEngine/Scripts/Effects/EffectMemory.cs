using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using UnityEngine;
namespace TcgEngine
{
    /// <summary>
    /// Effect to gain/lose mana (player)
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Memory", order = 1)]

    public class EffectMemory : EffectData
    {
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            ability.memory_card_uid.Add(target.uid);
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Slot target)
        {
            ability.memory_slot.Add(target);
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Player target)
        {
            ability.memory_player.Add(target);
        }
    }
}
