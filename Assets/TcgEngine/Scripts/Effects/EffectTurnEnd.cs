using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{
    /// <summary>
    /// 턴 종료 효과
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/TurnEnd", order = 13)]
    public class EffectTurnEnd : EffectData
    {
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster)
        {
            logic.NextStep();
        }
            
    }
}
