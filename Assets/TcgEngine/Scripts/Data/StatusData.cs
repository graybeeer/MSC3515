using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TcgEngine
{

    public enum StatusType
    {
        None = 0,

        AddAttack = 4,      //Attack status can be used for attack boost limited for X turns 
        AddHP = 5,          //HP status can be used for hp boost limited for X turns 
        AddManaCost = 6,    //Mana Cost status can be used for mana cost increase/reduction limited for X turns 

        Stealth = 10,       //Cant be attacked until do action
        Invincibility = 12, //Cant be attacked for X turns
        Shell = 13,         //Receives no damage the first time
        Protection = 14,    //Taunt, gives SuperProtected to other cards

        Armor = 18,         //Receives less damage
        SpellImmunity = 19, //Cant be targeted/damaged by spells

        Deathtouch = 20,    //Kills when attacking a character
        Fury = 22,          //Can attack twice per turn
        Intimidate = 23,    //Target doesnt counter when attacking
        Flying = 24,         //Can ignore taunt
        Trample = 26,         //Extra damage is assigned to player
        LifeSteal = 28,      //Heal player when fighting

        Silenced = 30,      //All abilities canceled 모든 종류의 능력 봉인됨
        Paralysed = 32,     //Cant do any actions for X turns / 마비됨 - 아무 행동(이동, 공격, 액티브 능력사용)도 못함
        cursed = 33,          //저주 - 이동 화살표 중 일부 사용 못하게 됨(중첩가능), 모든 이동화살표 이동 불가능해지면 사망
        Poisoned = 34,     //Lose hp each start of turn
        Sleep = 36,         //Doesnt untap at the start of turn

        Mercy = 40,         //자비 - 상대 영웅을 공격할 수 없음
        Protected = 43, //적 캐릭터(영웅제외)에게 공격받지 않는다
        SuperProtected = 44,     //Cards that are protected by taunt / 적 유닛에게 공격받지 않는다

        hasted = 50, //헤이스트- 저주랑 정 반대로 추가로 이동가능한 방향이 생김
        DeathCount =52, //데쓰카운트 - 일정 턴이 지나면 이 유닛은 사망한다.
    }

    /// <summary>
    /// Defines all status effects data
    /// Status are effects that can be gained or lost with abilities, and that will affect gameplay
    /// Status can have a duration
    /// </summary>

    [CreateAssetMenu(fileName = "status", menuName = "TcgEngine/StatusData", order = 7)]
    public class StatusData : ScriptableObject
    {
        public StatusType effect;

        [Header("Display")]
        public string title;
        public Sprite icon;

        [TextArea(3, 5)]
        public string desc;

        [Header("FX")]
        public GameObject status_fx;

        [Header("AI")]
        public int hvalue;

        public static List<StatusData> status_list = new List<StatusData>();

        public string GetTitle()
        {
            return title;
        }

        public string GetDesc()
        {
            return GetDesc(1);
        }

        public string GetDesc(int value)
        {
            string des = desc.Replace("<value>", value.ToString());
            return des;
        }

        public static void Load(string folder = "")
        {
            if (status_list.Count == 0)
                status_list.AddRange(Resources.LoadAll<StatusData>(folder));
        }

        public static StatusData Get(StatusType effect)
        {
            foreach (StatusData status in GetAll())
            {
                if (status.effect == effect)
                    return status;
            }
            return null;
        }

        public static List<StatusData> GetAll()
        {
            return status_list;
        }
    }
}