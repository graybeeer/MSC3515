using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(ConditionData),true)]
public class ConditionEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ConditionData conditionData = (ConditionData)target;

        GUILayout.Space(30);
        GUILayout.Label("이 데이터를 참조하는 다른 데이터");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("referencingComponents"));


        if (GUILayout.Button("이 데이터를 참조하는 데이터 찾기"))
        {
            conditionData.FindReferencingData();
        }
        

    }
    
}
