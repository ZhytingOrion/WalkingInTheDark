using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUI : MonoBehaviour
{
    SteamVR_LaserPointer slp;   //射线对象
    SteamVR_TrackedController stc;    //控制器对象

    GameObject target = null;    //指向可以拾取的物体

    // Start is called before the first frame update
    void Start()
    {
        slp = GetComponent<SteamVR_LaserPointer>();    //得到射线对象
        slp.PointerIn += OnpointerIn;    //响应射线的进入事件
        slp.PointerOut += OnpointerOut;    //响应射线的离开事件
        stc = GetComponent<SteamVR_TrackedController>();    //得到手柄控制器的对象
        stc.TriggerClicked += OnTriggerClicked;    //响应手柄扣动事件
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnpointerIn(object sender, PointerEventArgs e) //射线进入事件
    {
        GameObject obj = e.target.gameObject;//得到指向的物体
        target = obj;
    }

    void OnpointerOut(object sender, PointerEventArgs e)//射线离开事件
    {
        if (target != null)  //如果是在能标记的物体上离开
        {
            target = null;  //不再记录这个物体了
        }
    }

    void OnTriggerClicked(object sender, ClickedEventArgs e)//用来响应扳机扣动事件的行为
    {
        if (target != null)
        {
            // do something
        }
    }
    
}
