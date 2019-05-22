using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFlagControl : MonoBehaviour
{
    public GameObject blue;
    public GameObject green;
    public GameObject yellow;
    public GameObject red;
    public Color m_color = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        //blue = this.transform.Find("MagicBlue").gameObject;
        //green = this.transform.Find("MagicGreen").gameObject;
        //yellow = this.transform.Find("MagicYellow").gameObject;
        //red = this.transform.Find("MagicRed").gameObject;
    }

    public void ChangeColor(Color color)
    {
        ChangeFlagState(this.m_color, false);
        ChangeFlagState(color, true);
        m_color = color;
    }

    private void ChangeFlagState(Color color, bool state)
    {
        if(color == Color.blue)
        {
            blue.SetActive(state);
        }
        else if(color == Color.red)
        {
            red.SetActive(state);
        }
        else if(color == Color.yellow)
        {
            yellow.SetActive(state);
        }
        else if(color == Color.green)
        {
            green.SetActive(state);
        }
    }
}
