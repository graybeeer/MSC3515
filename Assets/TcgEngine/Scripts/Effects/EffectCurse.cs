using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;
using static UnityEngine.Rendering.DebugUI;
using System.Linq;
using Unity.Mathematics;
using static UnityEngine.GraphicsBuffer;
using TcgEngine.AI;
using UnityEditor.Playables;

namespace TcgEngine
{
    /// <summary>
    /// Effects that heals a card or player (hp)
    /// It cannot restore more than the original hp, use AddStats to go beyond original
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Curse", order = 10)]
    public class EffectCurse : EffectData
    {
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int curse_value = CalculateCurse(logic, ability, caster, target);
            target.AddStatus(StatusType.cursed, curse_value, ability.duration);
        }
        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int curse_value = CalculateCurse(logic, ability, caster, target);
            target.AddOngoingStatus(StatusType.cursed, curse_value);
        }

        int CalculateCurse(GameLogic logic, AbilityData ability, Card caster, Card target, int curseNum = 0)
        {
            //ability.value가 1이면 1개 저주, 3개면 3개 화살표 저주하는 셈

            List<int> can_curse_arrow = new List<int>(); //저주를 걸 수 있는(이동 가능한 방향) 숫자키 방향 배열
            bool[] target_cursed_arrow = CheckCursed(target); //저주 당하는 카드가 이미당한 저주 방향키
            int curse_value = 0;
            System.Random rand = new System.Random();

            for (int i = 0; i < 9; i++)
            {
                if (target.card_arrow[i] && !target_cursed_arrow[i] && i != 4) //키패드 5번, 정가운데는 애초에 이동불가능한 잉여 화살표이므로 제외
                {
                    can_curse_arrow.Add(i);
                }
            }
            var picked = can_curse_arrow.OrderBy(x => rand.Next()).Take(ability.value).ToList(); //저주 가능한 방향키중 저주 밸류만큼 숫자 뽑기
            foreach (int i in picked)
            {
                curse_value += (int)Mathf.Pow(2, i);
            }

            //모든 이동가능한 화살표가 저주받으면 카드 사망
            if (ability.value >= can_curse_arrow.Count)
            {
                logic.KillCard(caster, target);
            }

            return curse_value;
        }


        public static bool[] CheckCursed(Card target)
        {
            bool[] temp_curse_arrow = new bool[9];

            if (target.HasStatus(StatusType.cursed))
            {
                int curse_value = target.GetStatusValue(StatusType.cursed);

                for (int i = 8; i >= 0; i--)
                {
                    if ((curse_value - (int)Mathf.Pow(2, i)) >= 0)
                    {
                        temp_curse_arrow[i] = true;
                        curse_value -= (int)Mathf.Pow(2, i);
                    }
                    else temp_curse_arrow[i] = false;
                }
            }

            return temp_curse_arrow;

        }
    }
}