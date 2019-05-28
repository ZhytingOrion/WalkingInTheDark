using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarControl : MonoBehaviour {

    /// <summary>
    /// 1.前方有车/人：减速，保持车距：与人距离过近：刹车
    /// 2.绿灯行，仅剩3秒时判断自己与人行横道线的距离决定停/行；红灯时开到前方最后一辆车后/斑马线前停
    /// 3.根据目的地寻路
    /// </summary>
    /// 

    private NavMeshAgent agent = null;  //汽车导航
    public GameObject dest;  //汽车导航
    private Vector3 agentDest;
    //private Vector3 nextAgentDest = Vector3.down;   //设定为一个不可能的值
    public float remainDistToSetNextDest = 0.01f;
    private bool isRed = false;
    private float maxSpeed;
    private bool isSomethingAhead = false;
    private Quaternion lastRotation;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        agentDest = dest.transform.position;
        agent.SetDestination(agentDest);
        maxSpeed = this.agent.speed;
        lastRotation = this.transform.rotation;
        agent.speed = Random.Range(3f, 4f);
    }
	
	// Update is called once per frame
	void Update () {
        if (isRed)
        {
            agent.velocity = Vector3.zero;
        }
        Ray ray = new Ray(this.transform.position + this.transform.up * 0.5f, this.transform.forward);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 3.0f))    //判断前方三米路况
        {
            string tag = hitInfo.transform.tag;   //前方的物体标签 
            switch (tag)
            {
                case "Car":                                               //前方有车
                    float eular = Quaternion.Angle(this.lastRotation, this.transform.rotation);          //根据车辆偏转角度判断自己是左转、右转还是直行
                    if (eular <= 0)                                       //左转/掉头车辆优先级最高
                        StopTheCar();                                     //非左转/掉头车辆停车等待对方前进
                    this.isSomethingAhead = true;                         //标记前方有物体
                    break;
                case "Player":                                            //前方有玩家
                    StopTheCar();                                         //停车
                    //播放刹车音效
                    break;
                case "People":                                            //前方有NPC
                    StopTheCar();                                         //停车，此处为省资源不播放刹车音效，效果不好再加上
                    break;
                default:
                    break;
            }
        }
        else if (this.isSomethingAhead)
        {
            this.isSomethingAhead = false;
            if (!this.isRed)
            {
                RestartTheCar();
            }
        }

        lastRotation = this.transform.rotation;
    }

    public void setDestination(Vector3 point)
    {
        agent.SetDestination(point);
        this.agentDest = point;
    }

    public void isTrafficLightRed(bool isRed)
    {
        this.isRed = isRed;
        if (isRed)
        {
            StopTheCar();
        }
        else
        {
            RestartTheCar();
        }
    }

    private void StopTheCar()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        //尝试关掉RigidBody和NavMeshAgent是否成功
        //this.GetComponent<NavMeshAgent>().enabled = false;
        Destroy(this.GetComponent<Rigidbody>());
    }

    private void RestartTheCar()
    {
        agent.isStopped = false;
        agent.SetDestination(this.agentDest);
        if (this.GetComponent<Rigidbody>() == null)
        {
            this.gameObject.AddComponent<Rigidbody>();
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<Rigidbody>().mass = 1000000f;
        }
    }
}
