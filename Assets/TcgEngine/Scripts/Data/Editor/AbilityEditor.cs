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
        if (GUILayout.Button("�� �����͸� �����ϴ� ������ ã��"))
        {
            abilityData.FindReferencingData();
        }

    }
}
