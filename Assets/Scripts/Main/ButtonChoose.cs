using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ChooseMode
{
    NewGame,
    Continue,
    Load,
    Quit,
    Quit2Main
}

public class ButtonChoose : MonoBehaviour
{
    private GameObject board;
    public ChooseMode Mode;

    void Start()
    {
        board = this.transform.Find("ChooseBoard").gameObject;
    }

    void OnTriggerEnter(Collider collider)
    {
        board.SetActive(true);
        switch(Mode)
        {
            case ChooseMode.NewGame:
                SceneManager.LoadScene("Level");
                break;
            case ChooseMode.Load:
                //todo: 读取游戏信息
                SceneManager.LoadScene("Level");
                break;
            case ChooseMode.Continue:
                this.transform.parent.gameObject.SetActive(false);
                if (GameInfo.Instance.CntLevel < 5)
                    GameInfo.Instance.CntGameState = GameState.Play;
                else GameInfo.Instance.CntGameState = GameState.Finish;
                break;
            case ChooseMode.Quit:
                Application.Quit();
                break;
            case ChooseMode.Quit2Main:
                //todo: 保存游戏信息
                SceneManager.LoadScene("MainScene");
                break;
            default:
                break;
        }
    }
}
