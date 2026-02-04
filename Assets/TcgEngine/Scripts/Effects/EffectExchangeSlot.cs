using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using UnityEngine;
namespace TcgEngine
{
    /// <summary>
    /// 두 소환물의 위치 교환
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/ExchangeSlot", order = 2)]
    public class EffectExchangeSlot : EffectData
    {
        public bool change_king = false;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster)
        {
            if (change_king)
            {
                Player caster_player = logic.GameData.GetPlayer(caster.player_id);
                Player target_player = logic.GameData.GetOpponentPlayer(caster_player.player_id);
                logic.ExchangeMove(caster_player.hero, target_player.hero);
            }
            else
            {
            }
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            if (change_king)
            {
                Player caster_player = logic.GameData.GetPlayer(caster.player_id);
                Player target_player = logic.GameData.GetOpponentPlayer(caster_player.player_id);
                logic.ExchangeMove(caster_player.hero, target_player.hero);
            }
            else
            {
                logic.ExchangeMove(caster, target);
            }
        }
    }
}