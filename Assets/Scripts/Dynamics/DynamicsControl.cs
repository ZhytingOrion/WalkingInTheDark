using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicsControl : MonoBehaviour
{

    public List<GameObject> dynamicLevelShow = new List<GameObject>();
    public List<GameObject> finishToDestory = new List<GameObject>();

    void OnEnable()
    {
        GameInfo.Instance.OnGameLevelChange += showDynamics;
        GameInfo.Instance.OnGameStateChange += deleteThing;
    }

    private void showDynamics(int level)
    {
        if (dynamicLevelShow.Count <= level) return;
        if (dynamicLevelShow[level] == null) return;
        dynamicLevelShow[level].SetActive(true);
    }

    private void deleteThing(GameState state)
    {
        if(state == GameState.Finish)
        {
            for(int i = 0; i<finishToDestory.Count; ++i)
            {
                Destroy(finishToDestory[i]);
            }
        }
    }
}
