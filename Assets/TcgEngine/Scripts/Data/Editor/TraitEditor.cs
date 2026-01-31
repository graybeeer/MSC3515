using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TraitData), true)]
public class TraitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TraitData traitData = (TraitData)target;



        if (GUILayout.Button("이 데이터를 참조하는 데이터 찾기"))
        {
            traitData.FindReferencingData();
        }


    }
}
