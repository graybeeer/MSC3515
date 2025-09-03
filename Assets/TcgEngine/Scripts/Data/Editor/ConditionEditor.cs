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
        GUILayout.Label("�� �����͸� �����ϴ� �ٸ� ������");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("referencingComponents"));


        if (GUILayout.Button("�� �����͸� �����ϴ� ������ ã��"))
        {
            conditionData.FindReferencingData();
        }
        

    }
    
}
