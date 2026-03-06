using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TcgEngine
{
    /// <summary>
    /// Condition that checks the type, team and traits of a card
    /// </summary>

    [CreateAssetMenu(fileName = "condition", menuName = "TcgEngine/Condition/CardType", order = 10)]
    public class ConditionCardType : ConditionData
    {
        [Header("Card is of type")]
        [Space(10)]
        [Header("목록 내에서 해당 속성이 있으면 속한 것으로 판단 / 타입, 팀, 종족 등을 동시에 충족해야함")]
        public List<CardType> has_type;
        public List<TeamData> has_team;
        public List<TraitData> has_trait;
        public List<TraitData> has_trait_stat;

        public ConditionOperatorBool oper;

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Card target)
        {
            return CompareBool(IsTrait(target), oper);
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Player target)
        {
            return false; //Not a card
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, Slot target)
        {
            return false; //Not a card
        }

        public override bool IsTargetConditionMet(Game data, AbilityData ability, Card caster, CardData target)
        {
            bool is_type = (has_type.Count == 0) || has_type.Contains(CardType.None);
            bool is_team = has_team.Count == 0;
            bool is_trait = has_trait.Count == 0;
            bool is_trait_stat = has_trait_stat.Count == 0;
            for (int i = 0; i < has_type.Count; i++)
            {
                if (target.type == has_type[i])
                    is_type = true;
            }
            for (int i = 0; i < has_team.Count; i++)
            {
                if (target.team == has_team[i])
                    is_team = true;
            }
            for (int i = 0; i < has_trait.Count; i++)
            {
                if (target.HasTrait(has_trait[i]))
                    is_trait = true;
            }
            for (int i = 0; i < has_trait_stat.Count; i++)
            {
                if (target.HasStat(has_trait_stat[i]))
                    is_trait_stat = true;
            }
            return CompareBool(is_type && is_team && is_trait && is_trait_stat, oper);

        }

        private bool IsTrait(Card card)
        {
            bool is_type = (has_type.Count == 0) || has_type.Contains(CardType.None);
            bool is_team = has_team.Count == 0;
            bool is_trait = has_trait.Count == 0;
            bool is_trait_stat = has_trait_stat.Count == 0;
            for (int i = 0; i < has_type.Count; i++)
            {
                if (card.CardData.type == has_type[i])
                    is_type = true;
            }
            for (int i = 0; i < has_team.Count; i++)
            {
                if (card.CardData.team == has_team[i])
                    is_team = true;
            }
            for (int i = 0; i < has_trait.Count; i++)
            {
                if (card.CardData.HasTrait(has_trait[i]))
                    is_trait = true;
            }
            for (int i = 0; i < has_trait_stat.Count; i++)
            {
                if (card.HasStat(has_trait_stat[i]))
                    is_trait_stat = true;
            }
            return (is_type && is_team && is_trait && is_trait_stat);
        }
    }
}