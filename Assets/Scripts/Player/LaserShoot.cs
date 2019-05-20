
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    SteamVR_LaserPointer slp;   //射线对象
    SteamVR_TrackedController stc;    //控制器对象
    SteamVR_TrackedObject trackedObject;  //手柄位置

    public GameObject menuPlane;

    GameObject target = null;    //指向可以拾取的物体
    
    public Transform player;  //玩家    
    public Transform dic;   //方向
    public float speed;   //速度

    private Color m_color;
    public List<Color> colorList = new List<Color>();
    public List<string> tagsCanBeShot = new List<string>(); 
    
    void Start()
    {
        slp = GetComponent<SteamVR_LaserPointer>();    //得到射线对象
        slp.PointerIn += OnpointerIn;    //响应射线的进入事件
        slp.PointerOut += OnpointerOut;    //响应射线的离开事件
        stc = GetComponent<SteamVR_TrackedController>();    //得到手柄控制器的对象
        stc.TriggerClicked += OnTriggerClicked;    //响应手柄扣动事件
        stc.TriggerUnclicked += OnTriggerUnclicked;    //响应手柄松开事件
        stc.MenuButtonClicked += OnMenuClicked;    //圆盘上方的菜单键

        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedObject)
        {
            var device = SteamVR_Controller.Input((int)trackedObject.index);
            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Vector2 cc = device.GetAxis();
                float angle = VectorAngle(new Vector2(1, 0), cc); //调用角度计算公式           
                    
                //下，这里是控制器圆盘的上下左右控制
                if (angle > 45 && angle < 135)
                {
                    m_color = colorList[2];
                    slp.changeColor(m_color);
                }

                //上
                if (angle < -45 && angle > -135)
                {
                    m_color = colorList[0];
                    slp.changeColor(m_color);
                }

                //左  
                if ((angle < 180 && angle > 135) || (angle < -135 && angle > -180))
                {
                    m_color = colorList[3];
                    slp.changeColor(m_color);
                }

                //右  
                if ((angle > 0 && angle < 45) || (angle > -45 && angle < 0))
                {
                    m_color = colorList[1];
                    slp.changeColor(m_color);
                }

            }
        }
    }

    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;

        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }

    void OnpointerIn(object sender, PointerEventArgs e) //射线进入事件
    {
        GameObject obj = e.target.gameObject;//得到指向的物体
        string tag = obj.tag;
        if (tagsCanBeShot.Contains(tag))
        {
            target = obj;  //用全局变量记录这个物体
        }
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
        if (GameInfo.Instance.CntGameState != GameState.Play) return;      //游戏结束的情况下不能再进行标记
        if (target != null)
        {
            Transform laserFlag = target.transform.Find("LaserFlag(Clone)");
            if (laserFlag != null)  //已经有标记了
            {
                laserFlag.GetComponent<Renderer>().material.color = m_color;
                return;
            }

            GameObject flag = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LaserFlag"));
            flag.transform.parent = target.transform;
            flag.transform.localPosition = Vector3.zero;
            flag.layer = LayerMask.NameToLayer("BlindWorld");
            flag.GetComponent<Renderer>().material.color = m_color;
        }
    }

    void OnTriggerUnclicked(object sender, ClickedEventArgs e)//用来响应扳机松开事件的行为
    {

    }

    void OnMenuClicked(object sender, ClickedEventArgs e)   //圆盘上方的菜单键
    {
        if (menuPlane == null) return;
        GameInfo.Instance.CntGameState = GameState.Stop;
        menuPlane.SetActive(true);
    }

}