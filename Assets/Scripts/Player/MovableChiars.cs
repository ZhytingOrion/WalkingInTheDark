using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableChiars : MonoBehaviour
{
    SteamVR_TrackedController stc;    //控制器对象
    SteamVR_TrackedObject trackedObject;  //手柄位置

    public Transform player;  //玩家    
    public Transform dic;   //方向
    public float speed;   //速度

    void Start()
    {
        stc = GetComponent<SteamVR_TrackedController>();    //得到手柄控制器的对象

        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedObject)
        {
            var device = SteamVR_Controller.Input((int)trackedObject.index);
            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && GameInfo.Instance.CntGameState != GameState.Guide)
            {
                Vector2 cc = device.GetAxis();
                float angle = VectorAngle(new Vector2(1, 0), cc); //调用角度计算公式           
                                                                  //下，这里是控制器圆盘的上下左右控制
                if (angle > 45 && angle < 135)
                {
                    player.transform.position -= dic.forward * Time.deltaTime * speed;
                }

                //上  
                if (angle < -45 && angle > -135)
                {
                    player.transform.position += dic.forward * Time.deltaTime * speed;
                }

                //左  
                if ((angle < 180 && angle > 135) || (angle < -135 && angle > -180))
                {
                    player.transform.position -= dic.right * Time.deltaTime * speed;
                }

                //右  
                if ((angle > 0 && angle < 45) || (angle > -45 && angle < 0))
                {
                    player.transform.position += dic.right * Time.deltaTime * speed;
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
}
