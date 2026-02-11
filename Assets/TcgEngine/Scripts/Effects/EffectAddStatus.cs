using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using UnityEngine;
namespace TcgEngine
{
    /// <summary>
    /// 쓸모없음
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/AddStatus", order = 10)]
    public class EffectAddStatus : EffectData
    {
        public StatusData status;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            target.AddStatus(status, ability.value, ability.duration);
        }
        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            target.AddOngoingStatus(status, ability.value);
        }
    }
}