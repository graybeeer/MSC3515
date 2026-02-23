using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{
    //Sends the target card to a pile of your choice (deck/discard/hand)
    //Dont use to send to board since it needs a slot, use EffectPlay instead to send to board
    //Also dont send to discard from the board because it wont trigger OnKill effects, use EffectDestroy instead

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/SendPile", order = 10)]
    public class EffectSendPile : EffectData
    {
        public PileType pile;
        [Header("어느 플레이어의 공간으로 보낼건지")]
        public SendPlayerType type;

        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            Game data = logic.GetGameData();
            Player player = data.GetPlayer(target.player_id);
            Player sendPlayer = null;
            if (type == SendPlayerType.self)
            {
                sendPlayer = data.GetPlayer(caster.player_id);
            }
            else if (type == SendPlayerType.opponent)
            {
                sendPlayer = data.GetOpponentPlayer(caster.player_id);
            }
            else if (type == SendPlayerType.original_owner)
            {
                sendPlayer = data.GetPlayer(target.player_id);
            }
            else
            {
                Debug.LogError("sendplayer 타입 오류");
            }

            if (pile == PileType.Deck)
            {
                player.RemoveCardFromAllGroups(target);
                sendPlayer.cards_deck.Add(target);
                target.Clear();
            }

            if (pile == PileType.Hand)
            {
                player.RemoveCardFromAllGroups(target);
                sendPlayer.cards_hand.Add(target);
                target.Clear();
            }

            if (pile == PileType.Discard)
            {
                player.RemoveCardFromAllGroups(target);
                sendPlayer.cards_discard.Add(target);
                target.Clear();
            }

            if (pile == PileType.Temp)
            {
                player.RemoveCardFromAllGroups(target);
                sendPlayer.cards_temp.Add(target);
                target.Clear();
            }
        }
    }

    public enum PileType
    {
        None = 0,
        Board = 10,
        Hand = 20,
        Deck = 30,
        Discard = 40,
        Secret = 50,
        Equipped = 60,
        Temp = 90,
    }
    public enum SendPlayerType
    {
        self,
        opponent,
        original_owner,
    }
}
