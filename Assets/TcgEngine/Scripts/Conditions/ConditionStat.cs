using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TcgEngine
{
    public enum ConditionStatType
    {
        None = 0,
        Attack = 10,
        HP = 20,
        Mana = 30,
        ArrowNum = 40,
    }
    public enum ConditionPlayerValueType
    {
        selfHandCount = 10,
        opponentHandCount = 11,
    }
    /// <summary>
    /// Compares basic card or player stats such as attack/hp/mana
    /// </summary>

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/Stat", order = 10)]
    public class ConditionStat : ConditionData
    {
        [Header("Card stat is")]
        public ConditionStatType type;
        public ConditionOperatorInt oper;
        public int value;
        [Header("체크하면 value의 값은 무시되고 아래의 해당밸류값과 비교됨")]
        public bool value_type;
        public ConditionPlayerValueType Num;


        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            if (value_type)
            {
                if (Num == ConditionPlayerValueType.selfHandCount)
                    value = data.GetPlayer(caster.player_id).cards_hand.Count;
                if (Num == ConditionPlayerValueType.opponentHandCount)
                    value = data.GetOpponentPlayer(caster.player_id).cards_hand.Count;
            }
                

            if (type == ConditionStatType.Attack)
            {
                return CompareInt(target.GetAttack(), oper, value);
            }

            if (type == ConditionStatType.HP)
            {
                return CompareInt(target.GetHP(), oper, value);
            }

            if (type == ConditionStatType.Mana)
            {
                return CompareInt(target.GetMana(), oper, value);
            }
            if(type == ConditionStatType.ArrowNum)
            {
                return CompareInt(target.GetActiveArrowNum(), oper, value);
            }

            return false;
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            if (type == ConditionStatType.HP)
            {
                return CompareInt(target.hp, oper, value);
            }

            if (type == ConditionStatType.Mana)
            {
                return CompareInt(target.mana, oper, value);
            }

            return false;
        }
    }
}