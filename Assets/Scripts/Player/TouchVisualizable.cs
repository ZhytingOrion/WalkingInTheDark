using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TouchVisualizable : MonoBehaviour
{
    public GameObject lefthand;
    public GameObject righthand;
    public GameObject blindrod;
    public Camera m_camera;
    public Material OutlineMat;
    public List<GameObject> things;
    private CommandBuffer buf;

    //void OnEnable()
    //{
    //    //List<GameObject> touchthings = TouchVisualizeInfo.Instance.GetTouchThings();
    //    List<GameObject> touchthings = things;
    //    m_camera = Camera.main;

    //    buf = new CommandBuffer();
    //    buf.name = "TestCommandBuffer";
    //    for (int i = 0; i < touchthings.Count; ++i)
    //    {
    //        buf.DrawRenderer(touchthings[i].GetComponent<Renderer>(), OutlineMat);
    //    }

    //    m_camera.AddCommandBuffer(CameraEvent.AfterForwardOpaque, buf);
    //}

    //void OnDisable()
    //{
    //    m_camera.RemoveCommandBuffer(CameraEvent.AfterForwardOpaque, buf);
    //    buf.Dispose();
    //}

    // Start is called before the first frame update
    void Start()
    {
        //List<GameObject> touchthings = TouchVisualizeInfo.Instance.GetTouchThings();
        List<GameObject> touchthings = things;
        m_camera = Camera.main;

        buf = new CommandBuffer();
        buf.name = "TestCommandBuffer";
        for (int i = 0; i < touchthings.Count; ++i)
        {
            buf.DrawRenderer(touchthings[i].GetComponent<Renderer>(), OutlineMat);
        }

        m_camera.AddCommandBuffer(CameraEvent.BeforeImageEffects, buf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnWillRenderObject()
    //{
    //    Vector3 lefthandscreenPos = m_camera.WorldToScreenPoint(lefthand.transform.position);
    //    Vector3 righthandscreenPos = m_camera.WorldToScreenPoint(righthand.transform.position);
    //    Vector3 blindrodscreenPos = m_camera.WorldToScreenPoint(blindrod.transform.position);

    //    //List<GameObject> touchthings = TouchVisualizeInfo.Instance.GetTouchThings();
    //    List<GameObject> touchthings = things;

    //    CommandBuffer buf = new CommandBuffer();
    //    for(int i = 0; i<touchthings.Count; ++i)
    //    {
    //        buf.DrawRenderer(touchthings[i].GetComponent<Renderer>(), OutlineMat);
    //    }

    //    m_camera.AddCommandBuffer(CameraEvent.AfterForwardOpaque, buf);
    //}
}
