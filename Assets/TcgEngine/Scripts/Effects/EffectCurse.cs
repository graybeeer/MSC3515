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
            //ability.value�� 1�̸� 1�� ����, 3���� 3�� ȭ��ǥ �����ϴ� ��

            List<int> can_curse_arrow = new List<int>(); //���ָ� �� �� �ִ�(�̵� ������ ����) ����Ű ���� �迭
            bool[] target_cursed_arrow = CheckCursed(target); //���� ���ϴ� ī�尡 �̴̹��� ���� ����Ű
            int curse_value = 0;
            System.Random rand = new System.Random();

            for (int i = 0; i < 9; i++)
            {
                if (target.card_arrow[i] && !target_cursed_arrow[i] && i != 4) //Ű�е� 5��, ������� ���ʿ� �̵��Ұ����� �׿� ȭ��ǥ�̹Ƿ� ����
                {
                    can_curse_arrow.Add(i);
                }
            }
            var picked = can_curse_arrow.OrderBy(x => rand.Next()).Take(ability.value).ToList(); //���� ������ ����Ű�� ���� �����ŭ ���� �̱�
            foreach (int i in picked)
            {
                curse_value += (int)Mathf.Pow(2, i);
            }

            //��� �̵������� ȭ��ǥ�� ���ֹ����� ī�� ���
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