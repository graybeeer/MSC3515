using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using TcgEngine.UI;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(AbilityData))]
public class AbilityEditor : Editor
{
    public override void OnInspectorGUI() 
    { 
        base.OnInspectorGUI(); 
        AbilityData abilityData = (AbilityData)target;
        if (GUILayout.Button("이 데이터를 참조하는 데이터 찾기"))
        {
            abilityData.FindReferencingData();
        }

    }
}
