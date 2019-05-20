using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRenders : MonoBehaviour
{
    public List<GameObject> hps = new List<GameObject>();

    void OnEnable()
    {
        GameInfo.Instance.OnHPChange += HPrender;
    }

    // Start is called before the first frame update
    void Start()
    {
        HPrender(GameInfo.Instance.HP);
    }

    private void HPrender(int hp)
    {
        int total = hps.Count;
        for(int i = 0; i<10; i++)
        {
            if ((total-i) <= hp) hps[i].SetActive(true);
            else hps[i].SetActive(false);
        }
    }
}
