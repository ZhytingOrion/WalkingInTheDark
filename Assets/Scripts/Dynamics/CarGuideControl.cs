using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGuideControl : MonoBehaviour {

    public List<GameObject> nav = new List<GameObject>();
    public float redTime = 30.0f;
    public float greenTime = 10.0f;
    public float startTime = 0.0f;
    private float time = 0.0f;
    private bool isRed = false;    //先绿灯再红灯
    private WalkingLightsState lightState = WalkingLightsState.Green;
    private GameObject Car = null;
    private GameObject lastCar = null;
    public List<GameObject> walkingLights = new List<GameObject>();

	// Use this for initialization
	void Start () {
        this.time = this.startTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(this.Car!=null)
        {
            //Debug.Log("Car:" + this.gameObject.name + " - " + this.Car.name);
        }
        time += Time.deltaTime; 
        if(time >= (greenTime + redTime - 3.0f) && lightState != WalkingLightsState.FlashGreen)
        {
            this.lightState = WalkingLightsState.FlashGreen;
            turnTrafficLight();
        }
        if(time >= greenTime + redTime && this.isRed)   //绿灯时间
        {
            this.isRed = false;
            this.lightState = WalkingLightsState.Red;
            turnTrafficLight();
            time = 0.0f;
        }
        if(time >= greenTime && !this.isRed)        //红灯时间
        {
            this.isRed = true;
            this.lightState = WalkingLightsState.Green;
            turnTrafficLight();
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
                this.Car = other.gameObject;
                other.GetComponent<CarControl>().isTrafficLightRed(this.isRed);
                int random = Random.Range(0, nav.Count);
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
        if(this.Car!=null && !this.isRed)
        {
            this.Car.GetComponent<CarControl>().isTrafficLightRed(this.isRed);
        }
        for(int i = 0; i<walkingLights.Count; ++i)
        {
            walkingLights[i].GetComponent<WalkingLightsControl>().TurnTrafficLights(this.lightState);
        }
    }

}
