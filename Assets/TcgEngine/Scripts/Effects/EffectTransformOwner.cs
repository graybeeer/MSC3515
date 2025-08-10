using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{
    /// <summary>
    /// Effect to transform a card into another card
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/TransformOwner", order = 10)]
    public class EffectTransformOwner : EffectData
    {

        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            Player player = logic.GameData.GetPlayer(caster.player_id);
            logic.TransformBoardCardOwner(player, target);
        }
    }
}