using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using UnityEngine;
namespace TcgEngine
{
    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Reverse", order = 11)]
    public class EffectReverse : EffectData
    {
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            bool[] target_cursed = EffectCurseHaste.CheckCursed(target);
            bool[] target_hasted = EffectCurseHaste.CheckHasted(target);
            int target_cursed_reversed = EffectCurseHaste.CalculateArrow(target_hasted); //반전하려면 헤이스트랑 저주는 수치 거꾸로
            int target_hasted_reversed = EffectCurseHaste.CalculateArrow(target_cursed); 

            target.card_arrow = ReverseArrow(target.card_arrow); //카드 화살표 반전

            target.RemoveStatus(StatusType.cursed); //기존 헤이스트랑 저주 데이터 지우기
            target.RemoveStatus(StatusType.hasted);

            target.AddStatus(StatusType.cursed, target_cursed_reversed, ability.duration); //반전된 헤이스트랑 저주 추가
            target.AddStatus(StatusType.hasted, target_hasted_reversed, ability.duration);
        }
        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target) 
        { 
        }
        public static bool[] ReverseArrow(bool[] arrow)
        {
            bool[] temp = new bool[9];
            for (int i = 0; i < arrow.Length; i++)
            {
                if (i != 4)
                    temp[i] = !arrow[i];
            }
            return temp;
        }
    }

}