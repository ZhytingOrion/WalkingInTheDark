using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
/// <summary>
/// 改变Prefab
/// 注：通过名字匹配搜索被替换目标
/// （被选中物体的所有子物体.name包含newPrefab.name则替换）
/// </summary>
public class MakePrefabsInstances : EditorWindow
{

    [MenuItem("Tools/ChangePrefab")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(MakePrefabsInstances));
    }

    static GameObject tonewPrefab;

    void OnGUI()
    {
        tonewPrefab = (GameObject)EditorGUILayout.ObjectField(tonewPrefab, typeof(GameObject), true, GUILayout.MinWidth(100f));
        if (isChange)
        {
            GUILayout.Button("正在变...");
        }
        else
        {
            if (GUILayout.Button("变变变！"))
                Change();
        }
    }

    static bool isChange = false;

    public static void Change()
    {
        if (tonewPrefab == null)
            return;

        isChange = true;
        List<GameObject> destroy = new List<GameObject>();
        Object[] labels = Selection.GetFiltered(typeof(GameObject), SelectionMode.Deep);
        foreach (Object item in labels)
        {
            GameObject tempGO = (GameObject)item; // (GameObject)item;
            //只要搜到的物体包含新Prefab的名字，就会被替换
            //if (tempGO.name.Contains(tonewPrefab.name))
            {
                //GameObject gameObj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                GameObject newGO = (GameObject)PrefabUtility.InstantiatePrefab(tonewPrefab);
                newGO.transform.SetParent(tempGO.transform.parent);
                newGO.name = tempGO.name;
                newGO.transform.localPosition = tempGO.transform.localPosition;
                newGO.transform.localRotation = tempGO.transform.localRotation;
                newGO.transform.localScale = tempGO.transform.localScale;

                destroy.Add(tempGO);
            }
        }
        foreach (GameObject item in destroy)
        {
            DestroyImmediate(item.gameObject);
        }
        isChange = false;
    }
}