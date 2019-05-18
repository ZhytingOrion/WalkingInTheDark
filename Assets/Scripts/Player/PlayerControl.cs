using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Vector3 lastLoc;
    private Vector3 newLoc;
    public GameObject HeadObject;
    private Vector3 oldHeadPos;

	// Use this for initialization
	void Start () {
        newLoc = this.transform.position;
        oldHeadPos = HeadObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        lastLoc = newLoc;
        newLoc = this.transform.position;
        //oldHeadPos = HeadObject.transform.localPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        //HeadObject.transform.localPosition = this.oldHeadPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            this.GetComponent<AudioSource>().Play();
            Debug.Log("Die");
        }
        else if (other.tag == "GuideRoad")
        {
            other.gameObject.layer = LayerMask.NameToLayer("BlindWorld");
        }
        else if (other.tag == "RoadStep")
        {
            GameObject step = other.transform.Find("Step").gameObject;
            switch (JudgePlayerDirection())                                 //根据玩家行动的方向，调整脚印的方向。由于Environment有Y轴Rotation为-90，故此处都-90，也可以用LocalRotation
            {
                case Direction.left:
                    step.transform.rotation = Quaternion.Euler(90, -90, 0);
                    break;
                case Direction.right:
                    step.transform.rotation = Quaternion.Euler(90, 90, 0);
                    break;
                case Direction.up:
                    step.transform.rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case Direction.down:
                    step.transform.rotation = Quaternion.Euler(90, 180, 0);
                    break;
                default:
                    break;
            }
            step.SetActive(true);
        }
    }

    public Direction JudgePlayerDirection()      //判断玩家的方向
    {
        float diffY = newLoc.y - lastLoc.y;
        float diffX = newLoc.x - lastLoc.x;
        float diffZ = newLoc.z - lastLoc.z;
        if(Mathf.Abs(diffX)>Mathf.Abs(diffZ))    //玩家方向左右朝向是X轴，前后方向是Z轴，上下方向是Y轴
        {
            if (diffX < 0) return Direction.left;
            else return Direction.right;
        }
        else
        {
            if (diffZ > 0) return Direction.up;
            else return Direction.down;
        }
    }
}
