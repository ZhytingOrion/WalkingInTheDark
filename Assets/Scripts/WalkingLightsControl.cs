using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkingLightsState    //红绿灯状态
{
    Red,           //红灯
    Green,         //绿灯
    FlashGreen     //绿灯在闪
}

public class WalkingLightsControl : MonoBehaviour
{
    private GameObject redLight;
    private GameObject greenLight;
    private AudioSource audioSource = null;   //自带的audioSource组件

    public AudioClip RedSound;
    public AudioClip GreenSound;
    public AudioClip FlashGreenSound;

    public WalkingLightsState state;   //状态

    // Start is called before the first frame update
    void Start()
    {
        redLight = this.transform.Find("RedLight").gameObject;
        greenLight = this.transform.Find("GreenLight").gameObject;
        audioSource = this.GetComponent<AudioSource>();
        showLightState(this.state);
    }

    private void showLightState(WalkingLightsState state)
    {
        switch (state)
        {
            case WalkingLightsState.Red:
                if (audioSource != null)
                {
                    audioSource.Stop();
                    audioSource.clip = RedSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                redLight.SetActive(true);
                greenLight.SetActive(false);
                break;
            case WalkingLightsState.Green:
                if (audioSource != null)
                {
                    audioSource.Stop();
                    audioSource.clip = GreenSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                redLight.SetActive(false);
                greenLight.SetActive(true);
                break;
            case WalkingLightsState.FlashGreen:
                if (audioSource != null)
                {
                    audioSource.Stop();
                    audioSource.clip = FlashGreenSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                redLight.SetActive(false);
                greenLight.SetActive(true);
                StartCoroutine(flashGreenLight(3.0f, 0.2f));
                break;
            default:
                break;
        }
    }

    private IEnumerator flashGreenLight(float time, float flashTime)
    {
        bool greenLightOn = true;
        yield return new WaitForSeconds(flashTime);
        for (float t = flashTime; t<time; t+= flashTime)
        {
           greenLightOn = !greenLightOn;
           greenLight.SetActive(greenLightOn);
           yield return new WaitForSeconds(flashTime);
        }
        greenLight.SetActive(false);
    }

    public void TurnTrafficLights(WalkingLightsState state)
    {
        showLightState(state);
    }
}
