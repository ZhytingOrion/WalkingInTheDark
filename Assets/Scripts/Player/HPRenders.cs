using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRenders : MonoBehaviour
{
    public List<GameObject> hps = new List<GameObject>();

    private AudioSource m_audioSource;
    private int lastHP;

    public AudioClip DeathClip;
    public AudioClip GotHeartClip;
    public AudioClip LoseHeartClip;

    void OnEnable()
    {
        GameInfo.Instance.OnHPChange += HPrender;
    }

    // Start is called before the first frame update
    void Start()
    {
        HPrender(GameInfo.Instance.HP);
        m_audioSource = this.GetComponent<AudioSource>();
        this.lastHP = GameInfo.Instance.HP;
    }

    private void HPrender(int hp)
    {
        int total = hps.Count;
        for(int i = 0; i<10; i++)
        {
            if ((total-i) <= hp) hps[i].SetActive(true);
            else hps[i].SetActive(false);
        }
        if(hp == 0)
        {
            //死亡了，播放死亡音效
            //m_audioSource.clip = DeathClip;
            //if (m_audioSource.isPlaying) m_audioSource.Stop();
            //m_audioSource.Play();
            //GameInfo.Instance.CntGameState = GameState.Stop;
        }
        else if(hp < this.lastHP)
        {
            //扣血了，播放扣血音效
            //m_audioSource.clip = LoseHeartClip;
            //if (m_audioSource.isPlaying) m_audioSource.Stop();
            //m_audioSource.Play();
        }
        else
        {
            //回血了，播放回血音效
            //m_audioSource.clip = GotHeartClip;
            //if (m_audioSource.isPlaying) m_audioSource.Stop();
            //m_audioSource.Play();
        }
    }
}
