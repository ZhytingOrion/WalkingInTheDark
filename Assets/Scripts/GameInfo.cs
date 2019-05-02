
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo{
    private static GameInfo instance = null;    //单例
    public static GameInfo Instance
    {
        get
        {
            if (instance == null) instance = new GameInfo();
            return instance;
        }
    }

    private int cntLevel = 0;    //当前游戏关卡
    public int CntLevel
    {
        get
        {
            return cntLevel;
        }
    }

    private GameState cntGameState = GameState.Play;   //当前游戏状态
    public GameState CntGameState
    {
        get { return cntGameState; }
        set { cntGameState = value; }
    }

    //事件 优化程序性能 监听Level和GameState的改变
    public event System.Action<int> OnGameLevelChange;
    public event System.Action<GameState> OnGameStateChange;

    public void LevelPass()     //通过关卡
    {
        if(this.cntLevel == 5)  //通过所有关卡，游戏结束
        {
            cntGameState = GameState.Finish;
            OnGameStateChange(cntGameState);
        }
        else                    //进入下一关卡
        {
            cntLevel++;
            OnGameLevelChange(cntLevel);
            //此处考虑将游戏设计为暂停状态，等玩家按下按键再成为Play状态。
        }
    }

    private int colliderTimes = 0;     //碰撞次数，用于复盘
    public int ColliderTimes
    {
        get { return colliderTimes; }
    }

    public void AddColliderTimes()
    {
        colliderTimes++;
    }
}
