using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TcgEngine
{
    /// <summary>
    /// Defines all traits and stats data
    /// </summary>

    [CreateAssetMenu(fileName = "TraitData", menuName = "TcgEngine/TraitData", order = 1)]
    public class TraitData : ScriptableObject
    {
        public string id;
        public string title;
        public Sprite icon;

        [TextArea(5, 10)]
        public string description;

        public static List<TraitData> trait_list = new List<TraitData>();

        [Space(30)]
        [Header("이 데이터를 참조하는 다른 데이터")]
        public List<ScriptableObject> referencingComponents = new List<ScriptableObject>();

        public void FindReferencingData()
        {
            referencingComponents.Clear();
            CardData.Load();
            foreach (var data in CardData.card_list)
            {
                if (data.HasTrait(this))
                    referencingComponents.Add(data);
            }
            foreach (var data in CardData.card_list)
            {
                if (data.HasStat(this))
                    referencingComponents.Add(data);
            }
        }

        public string GetTitle()
        {
            return title;
        }

        public static void Load(string folder = "")
        {
            if (trait_list.Count == 0)
                trait_list.AddRange(Resources.LoadAll<TraitData>(folder));
        }

        public static TraitData Get(string id)
        {
            foreach (TraitData team in GetAll())
            {
                if (team.id == id)
                    return team;
            }
            return null;
        }

        public static List<TraitData> GetAll()
        {
            return trait_list;
        }
    }

    [System.Serializable]
    public struct TraitStat
    {
        public TraitData trait;
        public int value;
    }
}