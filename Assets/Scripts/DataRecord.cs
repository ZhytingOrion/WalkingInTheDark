using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataRecord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//玩家数据类
[System.Serializable]
public class PlayerData
{
    //游戏设置
    //public bool isMusicOn = true;///<是否打开音乐
    //public bool isSoundOn = true;///<是否打开音效  

    //玩家数据
    public int life = 5;///<生命
    public int cntLevel = 1;///<达到的关卡

    //public List<GameObject>
}
