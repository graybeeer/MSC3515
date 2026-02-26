using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TcgEngine
{
    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/PlayerStat", order = 10)]
    public class ConditionPlayerStat : ConditionData
    {
        [Header("")]
        public ConditionPlayerValueType player_type;
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
            Player opponent_player = data.GetOpponentPlayer(target.player_id);
            if (player_type == ConditionPlayerValueType.SelfSummonCount)
            {
                return CompareInt(target.GetBoardcardNumExceptHero(), oper, value);
            }

            if (player_type == ConditionPlayerValueType.OpponentSummonCount)
            {
                return CompareInt(opponent_player.GetBoardcardNumExceptHero(), oper, value);
            }

            if (player_type == ConditionPlayerValueType.SelfHandCount)
            {
                return CompareInt(target.cards_hand.Count, oper, value);
            }
            if (player_type == ConditionPlayerValueType.OpponentHandCount)
            {
                return CompareInt(opponent_player.cards_hand.Count, oper, value);
            }
            return false;
        }
    }
}