using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class AddChangeMatScript : EditorWindow {

    private string tag;
    private bool isAdding = false;

    [MenuItem("Tools/AddChangeMatScript")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(AddChangeMatScript));
    }

    void OnGUI()
    {
        tag = EditorGUILayout.TagField(tag);

        if (isAdding)
        {
            GUILayout.Button("正在添加...");
        }
        else
        {
            if (GUILayout.Button("添加MatChange脚本"))
                AddingScripts();
        }
    }

    private void AddingScripts()
    {
        isAdding = true;
        GameObject[] gs = GameObject.FindGameObjectsWithTag(tag);
        for(int i = 0; i<gs.Length; ++i)
        {
            GameObject g = gs[i];
            try
            {
                if (g.GetComponent<MatChange>() != null) continue;
                Renderer r = g.GetComponent<Renderer>();
                if (r == null) continue;
                if (r.sharedMaterial == null) continue;                
            }
            catch(Exception e)
            {
                continue;
            }
            g.AddComponent<MatChange>();
        }
        isAdding = false;
    }
}
