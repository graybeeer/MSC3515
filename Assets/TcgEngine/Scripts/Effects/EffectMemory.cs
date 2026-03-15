using System.Collections;
using System.Collections.Generic;
using TcgEngine.Gameplay;
using UnityEngine;
namespace TcgEngine
{
    /// <summary>
    /// 체인 어빌리티를 위한 데이터 저장
    /// </summary>

    [CreateAssetMenu(fileName = "effect", menuName = "TcgEngine/Effect/Memory", order = 1)]

    public class EffectMemory : EffectData
    {
        public memoryType memory_type;
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Card target)
        {
            if (memory_type == memoryType.MemoryCount)
            {
                if (ability.value == 0)
                    Debug.Log("ability.value = 0");
                caster.memory.CountAdd(ability, ability.value);
            }
            if (memory_type == memoryType.MemoryCard)
                caster.memory.Add(ability, target.uid);
            if (memory_type == memoryType.Else)
                caster.memory.Add(ability, target.uid);

            //Debug.Log("저장됨");
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Slot target)
        {
            if (memory_type == memoryType.MemoryCount)
            {
                if (ability.value == 0)
                    Debug.Log("ability.value = 0");
                caster.memory.CountAdd(ability, ability.value);
            }
            if (memory_type == memoryType.MemorySlot)
                caster.memory.Add(ability, target);
            if (memory_type == memoryType.Else)
                caster.memory.Add(ability, target);
        }
        public override void DoEffect(GameLogic logic, AbilityData ability, Card caster, Player target)
        {
            if (memory_type == memoryType.MemoryCount)
            {
                if (ability.value == 0)
                    Debug.Log("ability.value = 0");
                caster.memory.CountAdd(ability, ability.value);
            }
            if (memory_type == memoryType.MemoryCount)
                caster.memory.CountAdd(ability, ability.value);
            if (memory_type == memoryType.Else)
                caster.memory.Add(ability, target);
        }
    }
    public enum memoryType
    {
        MemoryCount = 10,
        MemoryCard = 20,
        MemorySlot = 30,
        Else = 100,
    }
}
