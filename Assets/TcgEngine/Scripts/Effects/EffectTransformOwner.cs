using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/TransformOwner", order = 10)]
    public class EffectTransformOwner : EffectData
    {
        [Header("주인은")]
        public OwnerType ownerType;

        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {

            Player player = logic.GameData.GetPlayer(caster.player_id);
            if (ownerType == OwnerType.PlayerSelf)
            {
                logic.TransformBoardCardOwner(player, target);
            }
            else if (ownerType == OwnerType.PlayerOpponent)
            {
                logic.TransformBoardCardOwner(logic.GameData.GetOpponentPlayer(player.player_id), target);
            }
            else if (ownerType == OwnerType.PlayerNeutral)
            {
                Debug.LogError("중립 유닛은 없음");
            }
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Slot target)
        {
        }
    }
}
public enum OwnerType
{
    PlayerSelf,
    PlayerNeutral,
    PlayerOpponent,
}