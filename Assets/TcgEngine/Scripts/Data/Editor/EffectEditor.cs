using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(EffectData),true)]
public class EffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EffectData abilityData = (EffectData)target;

        GUILayout.Space(30);
        GUILayout.Label("�� �����͸� �����ϴ� �ٸ� ������");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("referencingComponents"));


        if (GUILayout.Button("�� �����͸� �����ϴ� ������ ã��"))
        {
            abilityData.FindReferencingData();
        }
        

    }
}
