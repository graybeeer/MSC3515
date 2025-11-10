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
            bool[] target_cursed = new bool[9];
            bool[] target_hasted = new bool[9];
        }
        public override void DoOngoingEffect(GameLogic logic, AbilityData ability, Card caster, Card target) 
        { 
        }
    }

}