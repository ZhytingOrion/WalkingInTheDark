using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    public List<GameObject> Ends = new List<GameObject>();
    public List<GameObject> Starts = new List<GameObject>();
    public GameObject Player;

    void OnEnable()
    {
        GameInfo.Instance.OnGameLevelChange += ResetLevel;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
            PassLevel();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (GameInfo.Instance.CntGameState != GameState.Play) return;
        //if(other.gameObject.name=="End")
        //{
        //    Destroy(other.gameObject);
        //}
        if(other.gameObject.tag == "Player")
        {
            PassLevel();
        }
    }

    public void ResetLevel(int level)
    {
        if (GameInfo.Instance.CntGameState == GameState.Finish) return;
        if (GameInfo.Instance.CntLevel == 6) return;
        int cntGameLevel = level;                  //移动起点终点到对应位置
        Transform end = Ends[cntGameLevel].transform;
        this.transform.position = end.position;
        this.transform.rotation = end.rotation;
        Transform start = Starts[cntGameLevel].transform;
        Player.transform.position = start.position;
        Player.transform.rotation = start.rotation;
    }

    private void PassLevel()
    {
        GameInfo.Instance.LevelPass();
        if (GameInfo.Instance.CntLevel == 6)          //游戏结束，删除目的地
        {
            Destroy(this.gameObject);
            return;
        }        
    }
    
}
