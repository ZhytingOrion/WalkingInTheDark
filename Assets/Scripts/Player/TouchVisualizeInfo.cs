using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchVisualizeInfo
{
    //单例模式
    private static TouchVisualizeInfo _instance;
    public static TouchVisualizeInfo Instance
    {
        get
        {
            if (_instance == null) _instance = new TouchVisualizeInfo();
            return _instance;
        }
    }

    //触觉接触到的障碍物
    private List<GameObject> touchThings;
    public List<GameObject> GetTouchThings()
    {
        return touchThings;
    }

    //添加碰到的障碍物
    public void AddTouchThings(GameObject touchThing)
    {
        touchThings.Add(touchThing);
    }

    //移除障碍物
    public void RemoveTouchThings(GameObject touchThing)
    {
        touchThings.Remove(touchThing);
    }
}
