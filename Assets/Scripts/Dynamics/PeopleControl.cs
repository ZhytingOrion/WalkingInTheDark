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
    private bool isAvoiding = false;

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
        if(!isAvoiding)
        {
            StartCoroutine(AvoidSomething());
        }
    }

    private void JudgeAvoid()
    {
        while (!isAvoiding)
        {
            StartCoroutine(AvoidSomething());
        }
    }

    private IEnumerator AvoidSomething()
    {
        this.isAvoiding = true;
        Vector3 origin = this.transform.position + this.transform.up * 0.5f;
        Ray ray = new Ray(origin, this.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1.5f))    //判断前方一米五路况
        {
            //前方有障碍物，转弯 --> 人向右走，则先判断可否向右躲避障碍物，反之同理。目的：尽量避免走到车道上。

            float peopleDirection = Quaternion.ToEulerAngles(this.transform.localRotation).y;
            if ((peopleDirection < 0 && peopleDirection > -180)  || (peopleDirection > 180 && peopleDirection < 360))
            {

                Ray rayLeft = new Ray(origin, -this.transform.right);
                if (Physics.Raycast(rayLeft, out hitInfo, 0.5f))    //左边0.5米内是否有障碍物
                {
                    Ray rayRight = new Ray(origin, this.transform.right);
                    if (Physics.Raycast(rayRight, out hitInfo, 0.5f))    //右边0.5米内是否有障碍物
                    { }
                    else
                    {
                        //向右转弯
                        yield return turnAround(90);
                        yield return turnAround();
                    }
                }
                else
                {
                    //向左转弯
                    yield return turnAround(-90);
                    yield return turnAround();
                }
            }
            else    //向右方向走，因此先考虑右转避开障碍物
            {
                Ray rayRight = new Ray(origin, this.transform.right);
                if (Physics.Raycast(rayRight, out hitInfo, 0.5f))    //右边0.5米内是否有障碍物
                {
                    Ray rayLeft = new Ray(origin, -this.transform.right);
                    if (Physics.Raycast(rayLeft, out hitInfo, 0.5f))    //左边0.5米内是否有障碍物
                    { }
                    else
                    {
                        //向右转弯
                        yield return turnAround(-90);
                        yield return turnAround();
                    }
                }
                else
                {
                    //向左转弯
                    yield return turnAround(90);
                    yield return turnAround();
                }
            }
        }
        this.isAvoiding = false;
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

        float lerpSpeed = rotateSpeed / Mathf.Abs(angle);

        for (float lerp = 0.0f; lerp < 1.0f; lerp += lerpSpeed * Time.deltaTime)
        {
            this.transform.rotation = Quaternion.Lerp(raw, dest, lerp);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        turnAround();
    }
}
