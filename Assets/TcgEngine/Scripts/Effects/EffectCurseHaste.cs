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

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/CurseHaste", order = 11)]
    public class EffectCurseHaste : EffectData
    {
        [Header("Ȱ��ȭ�� �����Ƽ ����� = �������� nȸ ����, ��Ȱ��ȭ�� n�������� �ѹ� ����")]
        public bool RandomCurse; //Ȱ��ȭ�� �����Ƽ ����� = �������� n�� ����, ��Ȱ��ȭ�� n�������� �ѹ� ����
        //Ȱ��ȭ�� ability.value�� 1�̸� 1�� ����, 3���� 3�� ȭ��ǥ �����ϴ� ��
        [Header("üũ�ϸ� ���̽�Ʈ�� ����")]
        public bool Haste; //üũ�ϸ� ���̽�Ʈ�� ����.
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int[] curse_haste_value = new int[2]; //1-���� , 2-���̽�Ʈ ��
            if (Haste)
                curse_haste_value = CalculateHaste(logic, ability, caster, target);
            else
                curse_haste_value = CalculateCurse(logic, ability, caster, target);
            
            
            if (curse_haste_value[0] != 0)
                target.AddStatus(StatusType.cursed, curse_haste_value[0], ability.duration);

            if (curse_haste_value[1] != 0)
                target.AddStatus(StatusType.hasted, curse_haste_value[1], ability.duration);

        }
        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int[] curse_haste_value = new int[2]; //1-���� , 2-���̽�Ʈ ��
            if (Haste)
                curse_haste_value = CalculateHaste(logic, ability, caster, target);
            else
                curse_haste_value = CalculateCurse(logic, ability, caster, target);

            if (curse_haste_value[0] != 0)
                target.AddOngoingStatus(StatusType.cursed, curse_haste_value[0]);
            if (curse_haste_value[1] != 0)
                target.AddOngoingStatus(StatusType.hasted, curse_haste_value[1]);
        }

        int[] CalculateCurse(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int[] curse_haste_value = { 0, 0 }; //1-���� , 2-���̽�Ʈ ��

            List<int> can_curse_arrow = new List<int>(); //���ָ� �� �� �ִ�(�̵� ������ ����) ����Ű ���� �迭
            bool[] target_cursed_arrow = CheckCursed(target); //���� ���ϴ� ī�尡 �̴̹��� ���� ����Ű
            bool[] target_hasted_arrow = CheckHasted(target); //���� ���ϴ� ī���� ���̽�Ʈ�� ����Ű
            int curse_count;
            System.Random rand = new System.Random();

            for (int i = 0; i < 9; i++)
            {
                if ((target_hasted_arrow[i] || target.card_arrow[i]) && !target_cursed_arrow[i] && i != 4) //Ű�е� 5��, ������� ���ʿ� �̵��Ұ����� �׿� ȭ��ǥ�̹Ƿ� ����
                {
                    can_curse_arrow.Add(i);
                }
            }

            if (RandomCurse) //���� ���ָ�
            {
                curse_count = ability.value;
                var picked = can_curse_arrow.OrderBy(x => rand.Next()).Take(curse_count).ToList(); //���� ������ ����Ű�� ���� �����ŭ ���� �̱�
                foreach (int i in picked)
                {
                    if (target_hasted_arrow[i])
                        curse_haste_value[1] -= (int)Mathf.Pow(2, i);
                    else curse_haste_value[0] += (int)Mathf.Pow(2, i);
                }
                
            }
            else //���� ������ ������������
            {

                int curseNum = math.max(ability.value - 1, 0);

                if (!can_curse_arrow.Contains(curseNum))
                    return curse_haste_value;

                if (target_hasted_arrow[curseNum])
                    curse_haste_value[1] -= (int)Mathf.Pow(2, curseNum);
                else curse_haste_value[0] += (int)Mathf.Pow(2, curseNum);
                curse_count = 1;
            }

            //��� �̵������� ȭ��ǥ�� ���ֹ����� ī�� ���
            if (curse_count >= can_curse_arrow.Count)
            {
                logic.KillCard(caster, target);
            }

            return curse_haste_value;
        }
        int[] CalculateHaste(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int[] curse_haste_value = { 0, 0 }; //1-���� , 2-���̽�Ʈ ��

            List<int> can_haste_arrow = new List<int>(); //���ָ� �� �� �ִ�(�̵� ������ ����) ����Ű ���� �迭
            bool[] target_cursed_arrow = CheckCursed(target); //���� ���ϴ� ī�尡 �̴̹��� ���� ����Ű
            bool[] target_hasted_arrow = CheckHasted(target); //���� ���ϴ� ī���� ���̽�Ʈ�� ����Ű
            int haste_count;
            System.Random rand = new System.Random();

            for (int i = 0; i < 9; i++)
            {
                if ((target_cursed_arrow[i] || !target.card_arrow[i]) && !target_hasted_arrow[i] && i != 4)
                {
                    can_haste_arrow.Add(i);

                }
            }

            if (RandomCurse) //���� ���̽�Ʈ��
            {
                haste_count = ability.value;
                var picked = can_haste_arrow.OrderBy(x => rand.Next()).Take(haste_count).ToList(); //���� ������ ����Ű�� ���� �����ŭ ���� �̱�
                foreach (int i in picked)
                {
                    if (target_cursed_arrow[i])
                        curse_haste_value[0] -= (int)Mathf.Pow(2, i);
                    else curse_haste_value[1] += (int)Mathf.Pow(2, i);
                }
            }
            else //���̽�Ʈ ������ ������ ������
            {
                int hasteNum = math.max(ability.value - 1, 0);

                if (!can_haste_arrow.Contains(hasteNum))
                    return curse_haste_value;

                if (target_cursed_arrow[hasteNum])
                    curse_haste_value[0] -= (int)Mathf.Pow(2, hasteNum);
                else curse_haste_value[1] += (int)Mathf.Pow(2, hasteNum);
                haste_count = 1;
            }

            return curse_haste_value;
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
        public static bool[] CheckHasted(Card target)
        {
            bool[] temp_haste_arrow = new bool[9];

            if (target.HasStatus(StatusType.hasted))
            {
                int haste_value = target.GetStatusValue(StatusType.hasted);

                for (int i = 8; i >= 0; i--)
                {
                    if ((haste_value - (int)Mathf.Pow(2, i)) >= 0)
                    {
                        temp_haste_arrow[i] = true;
                        haste_value -= (int)Mathf.Pow(2, i);
                    }
                    else temp_haste_arrow[i] = false;
                }
            }
            return temp_haste_arrow;
        }
    }
}