using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/ClearAbility", order = 10)]
    public class EffectClearAbility : EffectData
    {
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            target.ClearAllAbility();
        }
    }
}