using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{
    //보드 위 카드 장수만큼 드로우 효과 구현
    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/DrawCountCard_legacy", order = 99)]
    public class EffectDrawCountCard_legacy : EffectData
    {

        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            Player player = logic.GameData.GetPlayer(caster.player_id);
            logic.DrawCard(player, ability.value);
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Slot target)
        {
            Player player = logic.GameData.GetPlayer(caster.player_id);
            logic.DrawCard(player, ability.value);
        }
    }
}