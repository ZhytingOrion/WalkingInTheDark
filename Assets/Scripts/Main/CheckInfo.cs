using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckInfo : MonoBehaviour
{
    private ChooseMode cntMode;

    private void OnTriggerEnter(Collider collider)
    {
        switch(cntMode)
        {
            case ChooseMode.NewGame:
                SceneManager.LoadScene("Level");
                break;
            case ChooseMode.Load:
                //todo: 读取游戏信息
                SceneManager.LoadScene("Level");
                break;
            case ChooseMode.Quit:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public void SetMode(ChooseMode mode)
    {
        this.cntMode = mode;   
    }
}
