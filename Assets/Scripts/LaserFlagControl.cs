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
    private Color colorYellow = new Color(1, 235f / 255f, 4f / 255f, 1);

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
        Debug.Log("COLOR!!!!!!!!!!!!!!" + color.ToString());
        Debug.Log("COLOR!!!!!!!!!!!!!!" + colorYellow.ToString());
        Debug.Log("COLOR!!!!!!!!!!!!!!" + Color.yellow.ToString());

        Color nowYellow = new Color(1, 0.920f, 0.016f, 1f);
        Debug.Log("nowYellow and Color.yellow" + (nowYellow == Color.yellow).ToString());

        if(color == Color.blue)
        {
            blue.SetActive(state);
        }
        else if(color == Color.red)
        {
            red.SetActive(state);
        }
        else if(color == colorYellow)
        {
            Debug.Log("Enter Yellow");
            yellow.SetActive(state);
        }
        else if(color == Color.green)
        {
            green.SetActive(state);
        }
    }
}
