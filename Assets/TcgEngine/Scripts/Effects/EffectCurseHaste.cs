using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;
using System.Linq;
using Unity.Mathematics;

namespace TcgEngine
{
    /// <summary>
    /// 보드카드의 화살표 저주,가속
    /// 화살표를 이동가능하게 하거나 불가능하게 바꾼다.
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/CurseHaste", order = 11)]
    public class EffectCurseHaste : EffectData
    {
        [Header("활성화시 어빌리티 밸류값 = 랜덤으로 n회 저주, 비활성화시 n방향으로 한번 저주")]
        public bool RandomCurse; //활성화시 어빌리티 밸류값 = 랜덤으로 n개 저주, 비활성화시 n방향으로 한번 저주
        //활성화시 ability.value가 1이면 1개 저주, 3개면 3개 화살표 저주하는 셈
        [Header("체크하면 헤이스트로 계산됨")]
        public bool Haste; //체크하면 헤이스트로 계산됨.
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int[] curse_haste_value = new int[2]; //1-저주 , 2-헤이스트 값
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
            int[] curse_haste_value = new int[2]; //1-저주 , 2-헤이스트 값
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
            int[] curse_haste_value = { 0, 0 }; //1-저주 , 2-헤이스트 값

            List<int> can_curse_arrow = new List<int>(); //저주를 걸 수 있는(이동 가능한 방향) 숫자키 방향 배열
            bool[] target_cursed_arrow = CheckCursed(target); //저주 당하는 카드가 이미당한 저주 방향키
            bool[] target_hasted_arrow = CheckHasted(target); //저주 당하는 카드의 헤이스트된 방향키
            int curse_count;
            System.Random rand = new System.Random();

            for (int i = 0; i < 9; i++)
            {
                if ((target_hasted_arrow[i] || target.card_arrow[i]) && !target_cursed_arrow[i] && i != 4) //키패드 5번, 정가운데는 애초에 이동불가능한 잉여 화살표이므로 제외
                {
                    can_curse_arrow.Add(i);
                }
            }

            if (RandomCurse) //랜덤 저주면
            {
                curse_count = ability.value;
                var picked = can_curse_arrow.OrderBy(x => rand.Next()).Take(curse_count).ToList(); //저주 가능한 방향키중 저주 밸류만큼 숫자 뽑기
                foreach (int i in picked)
                {
                    if (target_hasted_arrow[i])
                        curse_haste_value[1] -= (int)Mathf.Pow(2, i);
                    else curse_haste_value[0] += (int)Mathf.Pow(2, i);
                }
                
            }
            else //저주 방향이 정해져있으면
            {

                int curseNum = math.max(ability.value - 1, 0);

                if (!can_curse_arrow.Contains(curseNum))
                    return curse_haste_value;

                if (target_hasted_arrow[curseNum])
                    curse_haste_value[1] -= (int)Mathf.Pow(2, curseNum);
                else curse_haste_value[0] += (int)Mathf.Pow(2, curseNum);
                curse_count = 1;
            }

            //모든 이동가능한 화살표가 저주받으면 카드 사망
            if (curse_count >= can_curse_arrow.Count)
            {
                logic.KillCard(caster, target);
            }

            return curse_haste_value;
        }
        int[] CalculateHaste(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            int[] curse_haste_value = { 0, 0 }; //1-저주 , 2-헤이스트 값

            List<int> can_haste_arrow = new List<int>(); //저주를 걸 수 있는(이동 가능한 방향) 숫자키 방향 배열
            bool[] target_cursed_arrow = CheckCursed(target); //저주 당하는 카드가 이미당한 저주 방향키
            bool[] target_hasted_arrow = CheckHasted(target); //저주 당하는 카드의 헤이스트된 방향키
            int haste_count;
            System.Random rand = new System.Random();

            for (int i = 0; i < 9; i++)
            {
                if ((target_cursed_arrow[i] || !target.card_arrow[i]) && !target_hasted_arrow[i] && i != 4)
                {
                    can_haste_arrow.Add(i);

                }
            }

            if (RandomCurse) //랜덤 헤이스트면
            {
                haste_count = ability.value;
                var picked = can_haste_arrow.OrderBy(x => rand.Next()).Take(haste_count).ToList(); //저주 가능한 방향키중 저주 밸류만큼 숫자 뽑기
                foreach (int i in picked)
                {
                    if (target_cursed_arrow[i])
                        curse_haste_value[0] -= (int)Mathf.Pow(2, i);
                    else curse_haste_value[1] += (int)Mathf.Pow(2, i);
                }
            }
            else //헤이스트 방향이 정해져 있으면
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
        public static int CalculateArrow(bool[] arrow)
        {
            int temp = 0;
            for (int i = 0; i < 9; i++)
            {
                if (arrow[i])
                    temp += (int)Mathf.Pow(2, i);
            }
            return temp;
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