using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

namespace TcgEngine
{
    //서버에서만 작동
    public class CardMemory
    {
        private List<AbilityMemory> memories = new List<AbilityMemory>();

        public void Remove(AbilityData ability)
        {
            memories.Remove(GetMemory(ability));
        }
        public void Chain(AbilityData iability, AbilityData chain_ability)
        {
            AbilityMemory iability_memory = GetMemory(iability);
            AbilityMemory chain_ability_memory = GetMemory(chain_ability);

            chain_ability_memory.memory_card.AddRange(iability_memory.memory_card);
            chain_ability_memory.memory_slot.AddRange(iability_memory.memory_slot);
            chain_ability_memory.memory_player.AddRange(iability_memory.memory_player);
            chain_ability_memory.memory_turn.AddRange(iability_memory.memory_turn);

            //Debug.Log("iability_memory.memory_card:" + iability_memory.memory_card.Count);
            //Debug.Log("chain_ability_memory.memory_card:" + chain_ability_memory.memory_card.Count);
        }
        public void Add(AbilityData ability, string card_uid)
        {
            //Debug.Log("추가:"+card_uid);
            GetMemory(ability).memory_card.Add(card_uid);
        }
        
        public void Add(AbilityData ability, Player player)
        {
            GetMemory(ability).memory_player.Add(player);
        }
        public void Add(AbilityData ability, Slot slot)
        {
            GetMemory(ability).memory_slot.Add(slot);
        }
        public void Add(AbilityData ability, int turn)
        {
            GetMemory(ability).memory_turn.Add(turn);
        }
        public bool Contains(AbilityData ability, string card_uid)
        {
            //Debug.Log(GetMemory(ability).memory_card.Contains(card_uid)+ " "+card_uid);
            return GetMemory(ability).memory_card.Contains(card_uid);
        }
        public bool Contains(AbilityData ability, Slot slot)
        {
            return GetMemory(ability).memory_slot.Contains(slot);
        }
        public bool Contains(AbilityData ability, Player player)
        {
            return GetMemory(ability).memory_player.Contains(player);
        }
        public bool Contains(AbilityData ability, int num)
        {
            return GetMemory(ability).memory_turn.Contains(num);
        }

        public void Clear()
        {
            memories.Clear();
        }
        public AbilityMemory GetMemory(AbilityData ability) 
        {
            foreach (AbilityMemory mem in memories)
            {
                if (mem.ability == ability)
                    return mem;
            }

            //기존에 없는경우 새로 생성
            AbilityMemory temp = new AbilityMemory(ability);
            memories.Add(temp);
            return temp;
        }
        public List<AbilityMemory> GetAbilityMemoryList()
        {
            return memories;
        }
        
    }
    
    public struct AbilityMemory
    {
        public AbilityData ability;
        public List<string> memory_card;
        public List<Player> memory_player;
        public List<Slot> memory_slot;
        public List<int> memory_turn;

        public AbilityMemory(AbilityData ability)
        {
            this.ability = ability;
            this.memory_card = new List<string>();
            this.memory_player = new List<Player>();
            this.memory_slot = new List<Slot>();
            this.memory_turn = new List<int>();
        }
    }
}