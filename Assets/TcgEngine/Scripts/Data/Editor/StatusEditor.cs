using System.Collections;
using System.Collections.Generic;
using TcgEngine;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(StatusData),true)]
public class StatusEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StatusData statusData = (StatusData)target;



        if (GUILayout.Button("�� �����͸� �����ϴ� ������ ã��"))
        {
            statusData.FindReferencingData();
        }
        

    }
}
