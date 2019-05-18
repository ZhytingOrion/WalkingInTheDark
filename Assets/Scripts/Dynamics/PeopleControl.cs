using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleControl : MonoBehaviour
{
    public List<GameObject> RoadGuideObjs = new List<GameObject>();
    private int roadGuideObjIndex = 0;
    public float Speed = 0.6f;
    public bool isStop = false;
    public float rotateSpeed = 180.0f;
    private float changeObjDistance = 0.5f;
    private Quaternion rawRotation;
    private Quaternion lookAtRotation;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(RoadGuideObjs[roadGuideObjIndex].transform);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * Speed * Time.deltaTime;    //或者用刚体的移动
        if(Vector3.Distance(this.transform.position, RoadGuideObjs[roadGuideObjIndex].transform.position) <= changeObjDistance)
        {
            roadGuideObjIndex++;
            if (roadGuideObjIndex >= RoadGuideObjs.Count)
            {
                roadGuideObjIndex = 0;
            }
            StartCoroutine(turnAround());
        }
    }

    private IEnumerator turnAround()
    {
        rawRotation = this.transform.rotation;
        this.transform.LookAt(RoadGuideObjs[roadGuideObjIndex].transform);
        lookAtRotation = this.transform.rotation;
        this.transform.rotation = rawRotation;

        float angle = Quaternion.Angle(rawRotation, lookAtRotation);
        float lerpSpeed = rotateSpeed / angle;

        for(float lerp = 0.0f; lerp<1.0f; lerp += lerpSpeed * Time.deltaTime)
        {
            this.transform.rotation = Quaternion.Lerp(rawRotation, lookAtRotation, lerp);
            yield return null;
        }

        this.transform.LookAt(RoadGuideObjs[roadGuideObjIndex].transform);
    }

    private IEnumerator turnAround(float angle)
    {
        Quaternion raw = this.transform.rotation;
        this.transform.Rotate(angle * Vector3.up);
        Quaternion dest = this.transform.rotation;
        this.transform.rotation = raw;

        float lerpSpeed = rotateSpeed / angle;

        for (float lerp = 0.0f; lerp < 1.0f; lerp += lerpSpeed * Time.deltaTime)
        {
            this.transform.rotation = Quaternion.Lerp(raw, dest, lerp);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        turnAround();
    }
}
