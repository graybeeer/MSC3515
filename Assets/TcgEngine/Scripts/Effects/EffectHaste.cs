using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.Gameplay;
using UnityEngine;

[CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Curse", order = 11)]
public class EffectHaste :  EffectData
{
    public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
    {
        //int haste_value = CalculateHaste(logic, ability, caster, target);
        //target.AddStatus(StatusType.hasted, haste_value, ability.duration);
    }
    public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
    {
        //int haste_value = CalculateHaste(logic, ability, caster, target);
        //target.AddOngoingStatus(StatusType.hasted, haste_value);
    }
    int CalculateHaste(GameLogic logic, AbilityData ability, Card caster, Card target, int hasteNum = 0)
    {
        List<int> can_haste_arrow = new List<int>(); //저주를 걸 수 있는(이동 가능한 방향) 숫자키 방향 배열
        bool[] target_haste_arrow = CheckHasted(target); //저주 당하는 카드가 이미당한 저주 방향키
        int haste_value = 0;
        System.Random rand = new System.Random();


        return haste_value;
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
