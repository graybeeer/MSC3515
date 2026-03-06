using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TcgEngine
{
    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/PlayerStat", order = 10)]
    public class ConditionPlayerStat : ConditionData
    {
        [Header("")]
        public ConditionPlayerValueType value_type;
        public ConditionPlayerType player_who;
        public ConditionEffectTrigger effector;
        public ConditionOperatorInt oper;

        public int value;

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            Player pcaster = data.GetPlayer(caster.player_id);
            Player ptarget = data.GetPlayer(target.player_id);
            if (effector == ConditionEffectTrigger.Target)
                return IsTargetConditionMet(data, ability, caster, ptarget);
            if (effector == ConditionEffectTrigger.Caster)
                return IsTargetConditionMet(data, ability, caster, pcaster);
            Debug.LogError("caster target ¼³Á¤ ¾ÈµÊ");
            return false;
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            Player effect_player;
            Player opponent_player = data.GetOpponentPlayer(target.player_id);
            if (player_who == ConditionPlayerType.Self)
                effect_player = target;
            else if (player_who == ConditionPlayerType.Opponent)
                effect_player = opponent_player;
            else effect_player = target;

            if (value_type == ConditionPlayerValueType.SummonCount)
            {
                return CompareInt(effect_player.GetBoardcardNumExceptHero(), oper, value);
            }

            if (value_type == ConditionPlayerValueType.HandCount)
            {
                return CompareInt(effect_player.cards_hand.Count, oper, value);
            }
            if (value_type == ConditionPlayerValueType.DeckCount)
            {
                return CompareInt(effect_player.cards_deck.Count, oper, value);
            }

            return false;
        }
    }
}