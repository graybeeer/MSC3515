using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TcgEngine
{
    [System.Serializable]
    public class CardGrave 
    {
        public List<DeathCard> graveCards = new List<DeathCard>();
        public void AddGrave(Slot slot, string cardDataID, int turn, Player player)
        {
            DeathCard temp = new DeathCard();
            temp.deathSlot = slot;
            temp.deathCardDataID = cardDataID;
            temp.deathturn = turn;
            temp.controlPlayer = player;
            graveCards.Add(temp);
        }

        
    }
    [System.Serializable]
    public struct DeathCard
    {
        public Slot deathSlot;
        public string deathCardDataID;
        public int deathturn;
        public Player controlPlayer;
    }
}
