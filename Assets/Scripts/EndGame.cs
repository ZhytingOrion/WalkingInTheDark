using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    public List<GameObject> Ends = new List<GameObject>();
    public List<GameObject> Starts = new List<GameObject>();
    public GameObject Player;

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
        //if(other.gameObject.name=="End")
        //{
        //    Destroy(other.gameObject);
        //}
        if(other.gameObject.tag == "Player")
        {
            PassLevel();
        }
    }

    private void PassLevel()
    {
        GameInfo.Instance.LevelPass();
        if (GameInfo.Instance.CntGameState == GameState.Finish)          //游戏结束，删除目的地
        {
            Destroy(this.gameObject);
            return;
        }
        int cntGameLevel = GameInfo.Instance.CntLevel;                  //移动起点终点到对应位置
        Transform end = Ends[cntGameLevel].transform;
        this.transform.position = end.position;
        this.transform.rotation = end.rotation;
        Transform start = Starts[cntGameLevel].transform;
        Player.transform.position = start.position;
        Player.transform.rotation = start.rotation;
    }
}
