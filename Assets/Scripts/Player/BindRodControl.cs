using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KnockInfo
{
    public string ObjectTag;          //物体标签
    public AudioClip knockSound;      //对应的音效
    public float knockShakeStrength;  //震动强度
    public float knockShakeTime;      //震动时间
}

public class BindRodControl : MonoBehaviour {

    SteamVR_TrackedObject Hand;
    SteamVR_Controller.Device device;

    bool IsShock = false;  //布尔型变量IsShock

    public List<KnockInfo> knockInfos = new List<KnockInfo>();
    private Dictionary<string, KnockInfo> knockInfoDic = new Dictionary<string, KnockInfo>();

    void Awake()
    {
        for (int i = 0; i < knockInfos.Count; ++i)
        {
            knockInfoDic.Add(knockInfos[i].ObjectTag, knockInfos[i]);
        }
    }

    // Use this for initialization
    void Start()
    {
        Hand = GetComponent();  //获得SteamVR_ TrackedObject组件
    }

    SteamVR_TrackedObject GetComponent()   //获得SteamVR_ TrackedObject组件
    {
        try
        {
            return this.transform.parent.GetComponent<SteamVR_TrackedObject>();
        }
        catch (Exception e)
        {
            Debug.LogError("SteamVR_TrackedObject does not exist!");
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        //防止Start函数没加载成功，保证SteamVR_ TrackedObject组件获取成功！
        if (Hand.isValid)
        {
            Hand = GetComponent();
        }
        device = SteamVR_Controller.Input((int)Hand.index);    //根据index，获得手柄 
                                                               //如果手柄的Trigger键被按下了
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            IsShock = false;  //每次按下，IsShock为false,才能保证手柄震动
            StartCoroutine(Shock(0.5f, 500)); //开启协程Shock()
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        IsShock = false;
        KnockInfo knockInfo = null;
        string tag = collision.gameObject.tag;
        knockInfoDic.TryGetValue(tag, out knockInfo);
        if(knockInfo != null)
        {
            AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
            if(audioSource != null)
            {
                audioSource.clip = knockInfo.knockSound;
                audioSource.Play();
            }
            StartCoroutine(Shock(knockInfo.knockShakeTime, (ushort)knockInfo.knockShakeStrength));
        }
        else StartCoroutine(Shock(0.5f, 500));
    }
    
    private void OnTriggerExit(Collider collision)
    {
        IsShock = false;
    }

    //定义了一个协程
    IEnumerator Shock(float durationTime, ushort strength)
    {
        //Invoke函数，表示durationTime秒后，执行StopShock函数；
        Invoke("StopShock", durationTime);

        //协程一直使得手柄产生震动，直到布尔型变量IsShock为false;
        while (!IsShock)
        {
            device.TriggerHapticPulse(strength);
            yield return new WaitForEndOfFrame();
        }
    }

    void StopShock()
    {
        IsShock = true; //关闭手柄的震动
    }
}
