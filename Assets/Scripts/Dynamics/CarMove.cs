using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

    public float carSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(carSpeed * Time.deltaTime, 0, 0);
        if (this.transform.position.x > 20.0f) this.transform.position = new Vector3(-20.0f, 0, 0);
	}
}
