using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreVoice : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    private AudioSource m_audioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = this.GetComponent<AudioSource>();

        m_audioSource.clip = clips[0];
        m_audioSource.Play();
        StartCoroutine(changeClip(clips[0].length));

    }
    
    private IEnumerator changeClip(float time)
    {
        yield return new WaitForSeconds(time);
        while (true)
        {
            int index = Random.Range(0, clips.Count);
            m_audioSource.clip = clips[index];
            m_audioSource.Play();
            yield return new WaitForSeconds(clips[index].length);
        }
    }
}
