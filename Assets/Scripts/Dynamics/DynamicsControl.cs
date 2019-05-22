using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicsControl : MonoBehaviour
{

    public List<GameObject> dynamicLevelShow = new List<GameObject>();

    void OnEnable()
    {
        GameInfo.Instance.OnGameLevelChange += showDynamics;
    }

    private void showDynamics(int level)
    {
        if (dynamicLevelShow.Count <= level) return;
        if (dynamicLevelShow[level] == null) return;
        dynamicLevelShow[level].SetActive(true);
    }
}
