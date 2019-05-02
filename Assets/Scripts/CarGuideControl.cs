using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGuideControl : MonoBehaviour {

    public List<GameObject> nav = new List<GameObject>();
    public float redTime = 30.0f;
    public float greenTime = 10.0f;
    private float time = 0.0f;
    private bool isRed = false;    //先绿灯再红灯
    private GameObject Car = null;
    private GameObject lastCar = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(this.Car!=null)
        {
            //Debug.Log("Car:" + this.gameObject.name + " - " + this.Car.name);
        }
        time += Time.deltaTime; 
        if(time >= greenTime + redTime && this.isRed)   //绿灯时间
        {
            this.isRed = false;
            Debug.Log("绿灯了");
            turnTrafficLight();
            time = 0.0f;
        }
        if(time >= greenTime && !this.isRed)        //红灯时间
        {
            this.isRed = true;
            //turnTrafficLight();
        }        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            this.lastCar = this.Car;
            this.Car = other.gameObject;
            if (this.lastCar != this.Car)  //被汽车碰撞
            {
                Debug.Log("Car在道路：" + this.gameObject.name);
                this.Car = other.gameObject;
                other.GetComponent<CarControl>().isTrafficLightRed(this.isRed);
                int random = Random.Range(0, nav.Count);
                Debug.Log("Next Destination:" + nav[random].name);
                other.GetComponent<CarControl>().setDestination(nav[random].transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Car")
        {
            if (this.Car == other.gameObject)    //如果离开的车是前面记录的进来的车，则移除该记录
            {
                this.Car = null;
            }
        }
    }

    private void turnTrafficLight()
    {
        if(this.Car!=null)
        {
            this.Car.GetComponent<CarControl>().isTrafficLightRed(this.isRed);
        }
    }

}
