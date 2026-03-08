using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TcgEngine
{
    [System.Serializable]
    public class SlotInform
    {
        public List<SlotData> slotData = new List<SlotData>();
        public void AddSlotData(Slot slot, int controlPlayerID, bool deep)
        {
            /*
            if (ContainsSlot(slot))
                slotData.Remove(GetSlotData(slot)); //기존에 있던 슬롯 정보는 삭제. 새로운 정보로 대체됨

            SlotData temp = new SlotData();
            temp.slot_x = slot.x;
            temp.slot_y = slot.y;
            temp.owner_p_id = controlPlayerID;
            temp.isDeep = deep;
            slotData.Add(temp);
            */
            AddSlotData(slot.x, slot.y, controlPlayerID, deep);
        }
        public void AddSlotData(int temp_slot_x,int temp_slot_y, int controlPlayerID, bool deep)
        {
            if (ContainsSlot(temp_slot_x,temp_slot_y))
                slotData.Remove(GetSlotData(temp_slot_x, temp_slot_y)); //기존에 있던 슬롯 정보는 삭제. 새로운 정보로 대체됨

            SlotData temp = new SlotData();
            temp.slot.x = temp_slot_x;
            temp.slot.y = temp_slot_y;
            temp.owner_p_id = controlPlayerID;
            temp.isDeep = deep;
            slotData.Add(temp);
        }
        
        public bool ContainsSlot(int x, int y)
        {
            foreach (var tempSlot in slotData)
            {
                if (tempSlot.slot.x == x && tempSlot.slot.y == y)
                    return true;
            }
            return false;
        }
        public bool CheckSlot(Slot slot)
        {
            return ContainsSlot(slot.x, slot.y);
        }
        public SlotData GetSlotData(int x, int y)
        {
            foreach (var tempSlot in slotData)
            {
                if (tempSlot.slot.x == x && tempSlot.slot.y == y)
                    return tempSlot;
            }
            Debug.LogError("존재하지 않는 슬롯");
            return new SlotData();
        }
        public SlotData GetSlotData(Slot slot)
        {
            return GetSlotData(slot.x, slot.y);
        }
        public List<SlotData> GetSlotDataList()
        {
            return slotData;
        }
    }
    [System.Serializable]
    public struct SlotData
    {
        public Slot slot;
        public int owner_p_id;
        public bool isDeep;
    }
}