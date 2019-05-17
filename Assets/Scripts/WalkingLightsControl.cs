using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingLightsControl : MonoBehaviour
{
    private GameObject redLight;
    private GameObject greenLight;

    // Start is called before the first frame update
    void Start()
    {
        redLight = this.transform.Find("RedLight").gameObject;
        greenLight = this.transform.Find("GreenLight").gameObject;
        redLight.SetActive(true);
        greenLight.SetActive(false);
    }

    public void TurnTrafficLights(bool isRed)
    {
        if(!isRed)
        {
            redLight.SetActive(true);
            greenLight.SetActive(false);
        }
        else
        {
            redLight.SetActive(false);
            greenLight.SetActive(true);
        }
    }
}
