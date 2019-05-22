using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotControl : MonoBehaviour
{
    public List<AudioClip> plotClips = new List<AudioClip>();
    public List<AudioClip> plotClipsRelife = new List<AudioClip>();

    private AudioSource audioSource;

    void OnEnable()
    {
        GameInfo.Instance.OnGameLevelChange += PlayPlot;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        PlayPlot(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayPlot(int level)
    {
        int MaxLevel = GameInfo.Instance.MaxLevel;
        if(MaxLevel == level)
        {
            //播放正常剧情
            StartCoroutine(playPlotClip(plotClips[level]));
        }
        else
        {
            //播放重生剧情
            StartCoroutine(playPlotClip(plotClipsRelife[level]));
        }
    }

    private IEnumerator playPlotClip(AudioClip clip)
    {
        GameInfo.Instance.CntGameState = GameState.Guide;
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        if(GameInfo.Instance.CntGameState == GameState.Guide)
            GameInfo.Instance.CntGameState = GameState.Play;
    }
}
