using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TcgEngine.Gameplay;

namespace TcgEngine
{

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/ClearArrow", order = 10)]
    public class EffectClearArrow : EffectData
    {
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            for(int i = 0; i < 9; i++)
            {
                target.card_arrow[i] = target.CardData.card_arrow[i];
            }
        }
    }
}