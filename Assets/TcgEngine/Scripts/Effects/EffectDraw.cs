using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;
using static UnityEngine.GraphicsBuffer;

namespace TcgEngine
{
    /// <summary>
    /// Effect to draw cards
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Draw", order = 10)]
    public class EffectDraw : EffectData
    {
        public drawPlayer draw_player;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster)
        {
            Player player = logic.GameData.GetPlayer(caster.player_id);
            Player opponent_player = logic.GameData.GetOpponentPlayer(caster.player_id);
            if (draw_player == drawPlayer.PlayerTargetSelf || draw_player == drawPlayer.PlayerOwnerSelf)
                logic.DrawCard(player, ability.value);
            if (draw_player == drawPlayer.PlayerTargetOpponent || draw_player == drawPlayer.PlayerOwnerOpponent)
                logic.DrawCard(opponent_player, ability.value);
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Player target)
        {
            Player player_owner = logic.GameData.GetPlayer(caster.player_id);
            Player opponent_player_owner = logic.GameData.GetOpponentPlayer(caster.player_id);
            if (draw_player == drawPlayer.PlayerTargetSelf)
                logic.DrawCard(target, ability.value);
            if (draw_player == drawPlayer.PlayerTargetOpponent)
                logic.DrawCard(logic.GameData.GetOpponentPlayer(target.player_id), ability.value);

            if (draw_player == drawPlayer.PlayerOwnerSelf)
                logic.DrawCard(player_owner, ability.value);
            if (draw_player == drawPlayer.PlayerOwnerOpponent)
                logic.DrawCard(opponent_player_owner, ability.value);
        }

        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            Player player = logic.GameData.GetPlayer(target.player_id);
            Player opponent_player = logic.GameData.GetOpponentPlayer(target.player_id);
            Player player_owner = logic.GameData.GetPlayer(caster.player_id);
            Player opponent_player_owner = logic.GameData.GetOpponentPlayer(caster.player_id);

            if (draw_player == drawPlayer.PlayerTargetSelf)
                logic.DrawCard(player, ability.value);
            if (draw_player == drawPlayer.PlayerTargetOpponent)
                logic.DrawCard(opponent_player, ability.value);

            if (draw_player == drawPlayer.PlayerOwnerSelf)
                logic.DrawCard(player_owner, ability.value);
            if (draw_player == drawPlayer.PlayerOwnerOpponent)
                logic.DrawCard(opponent_player_owner, ability.value);
        }

    }
}
public enum drawPlayer
{
    PlayerTargetSelf = 10,
    PlayerTargetOpponent = 20,
    PlayerOwnerSelf = 30,
    PlayerOwnerOpponent = 40,
}