using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TcgEngine
{

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/Alive", order = 10)]
    public class ConditionAlive : ConditionData
    {
        [Header("Card is Alive")]
        public ConditionOperatorBool oper;

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            Player player = data.GetPlayer(target.player_id);
            return CompareBool(player.GetDiscardCard(target.uid) == null, oper);
        }
    }
}
