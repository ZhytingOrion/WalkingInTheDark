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
    private Vector3 stoppedPoint;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        agentDest = dest.transform.position;
        agent.SetDestination(agentDest);
    }
	
	// Update is called once per frame
	void Update () {
        if (isRed)
        {
            agent.velocity = Vector3.zero;
        }
        //if(agent.remainingDistance<remainDistToSetNextDest && this.nextAgentDest!=Vector3.down)   //到达当前设定的目标点
        //{
        //    Debug.Log("到达当前目标点，切换下一个目标点");
        //    this.agentDest = this.nextAgentDest;
        //    agent.SetDestination(this.agentDest);
        //    this.nextAgentDest = Vector3.down;
        //}
	}

    public void setDestination(Vector3 point)
    {
        agent.SetDestination(point);
        this.agentDest = point;
        //if(this.nextAgentDest != Vector3.down)   //如果有下一个目标：直接新设定目标
        //{
        //    Debug.Log("有下一个目标，设定目标");
        //    this.agentDest = this.nextAgentDest;
        //    agent.SetDestination(this.agentDest);
        //    this.nextAgentDest = Vector3.down;
        //}
        //this.nextAgentDest = point;
    }

    public void isTrafficLightRed(bool isRed)
    {
        this.isRed = isRed;
        if (isRed)
        {
            stoppedPoint = this.transform.position;
            Debug.Log(stoppedPoint + "红灯停");
            //agent.SetDestination(this.transform.position);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            //this.GetComponent<NavMeshAgent>().enabled = false;
        }
        else
        {
            //this.GetComponent<NavMeshAgent>().enabled = true;
            agent.isStopped = false;
            agent.SetDestination(this.agentDest);
        }
    }
}
