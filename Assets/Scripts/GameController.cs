using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject CameraObject;
    private Camera m_camera;

    private GameObject[] gameObjectsinPeople;     //所有标签为people的对象
    private GameObject[] gameObjectsinRoad;     //所有标签为Road的对象
    private GameObject[] gameObjectsinCarRoad;     //所有标签为CarRoad的对象
    private GameObject[] gameObjectsinStaticEnv;     //所有标签为静物的对象

    public Material PeopleMat;  //人物的替换材质
    public Material RoadMat;  //人行道的替换材质
    public Material CarRoadMat;  //车道的替换材质
    public Material StaticEnvMat;   //静物的替换材质

    //public GaussianBlur gaussianBlur;
    public SaturabilityEffect saturabilityEffect;

    public float ChangeSaturationTime = 3.0f;

    private void OnEnable()
    {
        GameInfo.Instance.OnGameLevelChange += this.LevelChange;
        GameInfo.Instance.OnGameStateChange += this.GameStateChange;
    }

	// Use this for initialization
	void Start () {
        if(CameraObject!=null)
            m_camera = CameraObject.GetComponent<Camera>();
        gameObjectsinPeople = GameObject.FindGameObjectsWithTag("People");
        gameObjectsinRoad = GameObject.FindGameObjectsWithTag("Road");
        gameObjectsinCarRoad = GameObject.FindGameObjectsWithTag("CarRoad");
        gameObjectsinStaticEnv = GameObject.FindGameObjectsWithTag("Tree");
        //gaussianBlur = CameraObject.GetComponent<GaussianBlur>();
        saturabilityEffect = CameraObject.GetComponent<SaturabilityEffect>();
        saturabilityEffect.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LevelChange(int level)
    {
        switch(level)
        {
            case 1:
                //将人行道layer改为BlindWorld
                for(int i = 0; i<gameObjectsinRoad.Length; ++i)
                {
                    GameObject g = gameObjectsinRoad[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().ChangeMat(RoadMat);
                    g.layer = LayerMask.NameToLayer("BlindWorld");
                }
                //gaussianBlur.BlurRadius = 2.0f;
                break;
            case 2:
                //将车道地面改为BlindWorld
                for (int i = 0; i < gameObjectsinCarRoad.Length; ++i)
                {
                    GameObject g = gameObjectsinCarRoad[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().ChangeMat(CarRoadMat);
                    g.layer = LayerMask.NameToLayer("BlindWorld");
                }
                //gaussianBlur.BlurRadius = 1.6f;
                break;
            case 3:
                //将静物改成BlindWorld
                for (int i = 0; i < gameObjectsinStaticEnv.Length; ++i)
                {
                    GameObject g = gameObjectsinStaticEnv[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().ChangeMat(StaticEnvMat);
                    g.layer = LayerMask.NameToLayer("BlindWorld");
                }
                //gaussianBlur.BlurRadius = 1.2f;
                break;
            case 4:
                //将人改成BlindWorld
                for (int i = 0; i < gameObjectsinPeople.Length; ++i)
                {
                    GameObject g = gameObjectsinPeople[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().ChangeMat(PeopleMat);
                    g.layer = LayerMask.NameToLayer("BlindWorld");
                }
                //gaussianBlur.BlurRadius = 0.8f;
                break;
            case 5:
                //将材质球还原，挂上饱和度后处理
                for(int i = 0; i<gameObjectsinRoad.Length; ++i)
                {
                    GameObject g = gameObjectsinRoad[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                for (int i = 0; i < gameObjectsinCarRoad.Length; ++i)
                {
                    GameObject g = gameObjectsinCarRoad[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                for (int i = 0; i < gameObjectsinPeople.Length; ++i)
                {
                    GameObject g = gameObjectsinPeople[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                for (int i = 0; i < gameObjectsinStaticEnv.Length; ++i)
                {
                    GameObject g = gameObjectsinStaticEnv[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                //gaussianBlur.BlurRadius = 0.4f;
                saturabilityEffect.enabled = true;
                break;
        }
    }

    private void GameStateChange(GameState state)
    {
        if (state == GameState.Finish)
        {
            //开启漫游模式
            GameObject lefthand = CameraObject.transform.parent.Find("Controller (left)").gameObject;
            GameObject righthand = CameraObject.transform.parent.Find("Controller (right)").gameObject;
            lefthand.GetComponent<SteamVR_Teleporter>().enabled = true;
            lefthand.GetComponent<SteamVR_LaserPointer>().color = Color.blue;
            lefthand.GetComponent<SteamVR_LaserPointer>().thickness = 0.005f;

            //视觉恢复
            m_camera.cullingMask = -1;
            m_camera.clearFlags = CameraClearFlags.Skybox;
            StartCoroutine(recoverSaturation(this.ChangeSaturationTime));
            //gaussianBlur.enabled = false;

            //触觉可视化关闭
            Destroy(lefthand.GetComponent<TouchThings>());
            Destroy(righthand.GetComponent<TouchThings>());

            //删除RoadStep:
            GameObject[] roadSteps = GameObject.FindGameObjectsWithTag("RoadStep");
            for(int i = 0; i<roadSteps.Length;++i)
            {
                Destroy(roadSteps[i]);
            }

            //删除LaserShot
            GameObject[] shots = GameObject.FindGameObjectsWithTag("LaserShot");
            for (int i = 0; i < shots.Length; ++i)
            {
                Destroy(shots[i]);
            }
        }
    }

    private IEnumerator recoverSaturation(float time)
    {
        for(float i = 0; i<time; i+=Time.deltaTime)
        {
            saturabilityEffect.saturation = Mathf.Clamp01(i * 1.0f / time);
            yield return null;
        }
        saturabilityEffect.enabled = false;
    }
}
