using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public Transform CameraConfig;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(0, CameraConfig.localEulerAngles.y, 0);
        transform.localPosition = new Vector3(CameraConfig.localPosition.x, 0, CameraConfig.localPosition.z);
    }
}
