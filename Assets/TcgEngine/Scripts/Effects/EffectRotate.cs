using System;
using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;
using UnityEngine;
namespace TcgEngine
{
    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Rotate", order = 11)]
    public class EffectRotate : EffectData
    {
        [Header("비활성화시 기본 시계방향 회전, 활성화시 반시계 방향 회전 ")]
        public bool AntiClock; //반시계 방향으로 회전인지
        [Header("회전 횟수 ")]
        public int count; //회전 횟수
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            bool[] target_cursed = EffectCurseHaste.CheckCursed(target);
            bool[] target_hasted = EffectCurseHaste.CheckHasted(target);
            bool[] target_arrow = target.card_arrow;
            bool[] target_temp = new bool[9];
            bool[] target_cursed_temp = new bool[9];
            bool[] target_hasted_temp = new bool[9];

            int rotate_count = AntiClock ? -count : count;
            target_temp = RotateKeypad(target_arrow, rotate_count);
            target.card_arrow = target_temp; //화살표 회전

            target_cursed_temp = RotateKeypad(target_cursed, rotate_count);
            target_hasted_temp = RotateKeypad(target_hasted, rotate_count);
            target.RemoveStatus(StatusType.cursed); //기존 헤이스트랑 저주 데이터 지우기
            target.RemoveStatus(StatusType.hasted);
            
            target.AddStatus(StatusType.cursed, EffectCurseHaste.CalculateArrow(target_cursed_temp), ability.duration); //회전된 헤이스트랑 저주 추가
            target.AddStatus(StatusType.hasted, EffectCurseHaste.CalculateArrow(target_hasted_temp), ability.duration);

        }
        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target) 
        { 
        }
        public static T[] RotateKeypad<T>(T[] keypad, int times)
        {
            if (keypad == null)
                throw new ArgumentNullException(nameof(keypad));
            if (keypad.Length != 9)
                throw new ArgumentException("3x3 그리드는 반드시 9칸이어야 합니다.");

            bool clockwise = times >= 0;
            times = Math.Abs(times) % 8; // 8회 회전 시 원상복귀

            for (int t = 0; t < times; t++)
            {
                T[] temp = (T[])keypad.Clone();

                if (clockwise)
                {
                    // 시계 방향 회전
                    keypad[0] = temp[3];
                    keypad[1] = temp[0];
                    keypad[2] = temp[1];
                    keypad[3] = temp[6];
                    keypad[4] = temp[4];
                    keypad[5] = temp[2];
                    keypad[6] = temp[7];
                    keypad[7] = temp[8];
                    keypad[8] = temp[5];
                }
                else
                {
                    // 반시계 방향 회전
                    keypad[0] = temp[1];
                    keypad[1] = temp[2];
                    keypad[2] = temp[5];
                    keypad[3] = temp[0];
                    keypad[4] = temp[4];
                    keypad[5] = temp[8];
                    keypad[6] = temp[3];
                    keypad[7] = temp[6];
                    keypad[8] = temp[7];
                }
            }

            return keypad;
        }
    }

}