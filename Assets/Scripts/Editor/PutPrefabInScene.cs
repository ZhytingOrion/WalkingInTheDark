using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PutPrefabInScene : EditorWindow
{

    [MenuItem("Tools/PutPrefabScene")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(PutPrefabInScene));
    }

    private GameObject topleft;
    private GameObject downright;

    private GameObject parent = null;

    private string prefabName;

    private float width = 0.25f;
    private float height = 0.25f;
    private float Y = 0.02f;

    private bool isAdding = false;

    void OnGUI()
    {
        EditorGUILayout.LabelField("定位点：从上到下为上左、下右、高度Y");
        topleft = (GameObject)EditorGUILayout.ObjectField(topleft, typeof(GameObject), true, GUILayout.MinWidth(100f));
        downright = (GameObject)EditorGUILayout.ObjectField(downright, typeof(GameObject), true, GUILayout.MinWidth(100f));
        Y = EditorGUILayout.FloatField(Y, GUILayout.MinWidth(100f));

        EditorGUILayout.LabelField("待添加的预制体");
        prefabName = EditorGUILayout.TextField(prefabName, GUILayout.MinWidth(100f));
        EditorGUILayout.LabelField("待添加的预制体的父节点");
        parent = (GameObject)EditorGUILayout.ObjectField(parent, typeof(GameObject), true, GUILayout.MinWidth(100f));

        EditorGUILayout.LabelField("预制体中心间距宽、高：");
        width = EditorGUILayout.FloatField(width, GUILayout.MinWidth(100f));
        height = EditorGUILayout.FloatField(height, GUILayout.MinWidth(100f));

        if (isAdding)
        {
            GUILayout.Button("正在添加...");
        }
        else
        {
            if (GUILayout.Button("添加！"))
                AddingPrefabs();
        }
    }

    private void AddingPrefabs()
    {
        float leftX = topleft.transform.position.x;
        float rightX = downright.transform.position.x;
        float topZ = topleft.transform.position.z;
        float downZ = downright.transform.position.z;

        for(float x = leftX + width * 0.5f; x < rightX; x += width)
        {
            for(float z = downZ + height * 0.5f; z < topZ; z += height)
            {
                GameObject adding = Instantiate(Resources.Load<GameObject>(prefabName));
                adding.transform.position = new Vector3(x, Y, z);
                if (parent != null)
                    adding.transform.parent = parent.transform;
            }
        }
    }
}
