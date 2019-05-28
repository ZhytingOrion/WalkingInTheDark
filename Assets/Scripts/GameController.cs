using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject CameraObject;
    private Camera m_camera;

    private GameObject[] gameObjectsinPeople;     //所有标签为people的对象
    private GameObject[] gameObjectsinRoad;     //所有标签为Road的对象
    private GameObject[] gameObjectsinCarRoad;     //所有标签为CarRoad的对象
    private GameObject[] gameObjectsinTree;     //所有标签为Tree的对象
    private GameObject[] gameObjectsinWalkingLights;     //所有标签为WalkingLights的对象

    public Material PeopleMat;  //人物的替换材质
    public Material RoadMat;  //人行道的替换材质
    public Material CarRoadMat;  //车道的替换材质
    public Material TreeMat;   //树的替换材质
    public Material WalkingLightsMat;   //红绿灯的替换材质

    //public GaussianBlur gaussianBlur;
    public SaturabilityEffect saturabilityEffect;

    public float ChangeSaturationTime = 3.0f;

    private void OnEnable()
    {
        GameInfo.Instance.OnGameLevelChange += this.LevelChange;
        GameInfo.Instance.OnGameStateChange += this.GameStateChange;
    }

    // Use this for initialization
    void Start()
    {
        if (CameraObject != null)
            m_camera = CameraObject.GetComponent<Camera>();
        gameObjectsinPeople = GameObject.FindGameObjectsWithTag("People");
        gameObjectsinRoad = GameObject.FindGameObjectsWithTag("Road");
        gameObjectsinCarRoad = GameObject.FindGameObjectsWithTag("CarRoad");
        gameObjectsinTree = GameObject.FindGameObjectsWithTag("Tree");
        gameObjectsinWalkingLights = GameObject.FindGameObjectsWithTag("WalkingLights");
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
                GameObjRender(new List<GameObject>(gameObjectsinRoad), RoadMat);
                //for (int i = 0; i<gameObjectsinRoad.Length; ++i)
                //{
                //    GameObject g = gameObjectsinRoad[i];
                //    if (g.GetComponent<MatChange>() == null) continue;
                //    g.GetComponent<MatChange>().ChangeMat(RoadMat);
                //    g.layer = LayerMask.NameToLayer("BlindWorld");
                //}
                //gaussianBlur.BlurRadius = 2.0f;
                break;
            case 2:
                //将车道地面改为BlindWorld
                GameObjRender(new List<GameObject>(gameObjectsinCarRoad), CarRoadMat);
                //for (int i = 0; i < gameObjectsinCarRoad.Length; ++i)
                //{
                //    GameObject g = gameObjectsinCarRoad[i];
                //    if (g.GetComponent<MatChange>() == null) continue;
                //    g.GetComponent<MatChange>().ChangeMat(CarRoadMat);
                //    g.layer = LayerMask.NameToLayer("BlindWorld");
                //}
                //gaussianBlur.BlurRadius = 1.6f;
                break;
            case 3:
                //将静物改成BlindWorld
                GameObjRender(new List<GameObject>(gameObjectsinWalkingLights), WalkingLightsMat);
                //for (int i = 0; i < gameObjectsinStaticEnv.Length; ++i)
                //{
                //    GameObject g = gameObjectsinStaticEnv[i];
                //    if (g.GetComponent<MatChange>() == null) continue;
                //    g.GetComponent<MatChange>().ChangeMat(StaticEnvMat);
                //    g.layer = LayerMask.NameToLayer("BlindWorld");
                //}
                //gaussianBlur.BlurRadius = 1.2f;
                break;
            case 4:
                //将人改成BlindWorld
                GameObjRender(new List<GameObject>(gameObjectsinTree), TreeMat);
                //for (int i = 0; i < gameObjectsinPeople.Length; ++i)
                //{
                //    GameObject g = gameObjectsinPeople[i];
                //    if (g.GetComponent<MatChange>() == null) continue;
                //    g.GetComponent<MatChange>().ChangeMat(PeopleMat);
                //    g.layer = LayerMask.NameToLayer("BlindWorld");
                //}
                //gaussianBlur.BlurRadius = 0.8f;
                break;
            case 5:
                //将人改成BlindWorld
                GameObjRender(new List<GameObject>(gameObjectsinPeople), PeopleMat);
                //for (int i = 0; i < gameObjectsinPeople.Length; ++i)
                //{
                //    GameObject g = gameObjectsinPeople[i];
                //    if (g.GetComponent<MatChange>() == null) continue;
                //    g.GetComponent<MatChange>().ChangeMat(PeopleMat);
                //    g.layer = LayerMask.NameToLayer("BlindWorld");
                //}
                //gaussianBlur.BlurRadius = 0.8f;
                break;
            case 6:
                //将材质球还原，挂上饱和度后处理
                for(int i = 0; i<gameObjectsinRoad.Length; ++i)
                {
                    GameObject g = gameObjectsinRoad[i];
                    Transform lasershot = g.transform.Find("LaserFlag(Clone)");
                    if (lasershot != null) Destroy(lasershot.gameObject);
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                for (int i = 0; i < gameObjectsinCarRoad.Length; ++i)
                {
                    GameObject g = gameObjectsinCarRoad[i];
                    Transform lasershot = g.transform.Find("LaserFlag(Clone)");
                    if (lasershot != null) Destroy(lasershot.gameObject);
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                for (int i = 0; i < gameObjectsinPeople.Length; ++i)
                {
                    GameObject g = gameObjectsinPeople[i];
                    Transform lasershot = g.transform.Find("LaserFlag(Clone)");
                    if (lasershot != null) Destroy(lasershot.gameObject);
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                for (int i = 0; i < gameObjectsinTree.Length; ++i)
                {
                    GameObject g = gameObjectsinTree[i];
                    Transform lasershot = g.transform.Find("LaserFlag(Clone)");
                    if (lasershot != null) Destroy(lasershot.gameObject);
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                for (int i = 0; i < gameObjectsinWalkingLights.Length; ++i)
                {
                    GameObject g = gameObjectsinWalkingLights[i];
                    Transform lasershot = g.transform.Find("LaserFlag(Clone)");
                    if (lasershot != null) Destroy(lasershot.gameObject);
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().RecoverMat();
                }
                //gaussianBlur.BlurRadius = 0.4f;
                saturabilityEffect.enabled = true;

                GameInfo.Instance.FinishGame();

                break;
        }
    }

    private void GameObjRender(List<GameObject> objs, Material mat)
    {
        for (int i = 0; i < objs.Count; ++i)
        {
            GameObject g = objs[i];
            Transform lasershot = g.transform.Find("LaserFlag(Clone)");
            if (lasershot != null) Destroy(lasershot.gameObject);
            if (g.GetComponent<MatChange>() == null) continue;
            g.GetComponent<MatChange>().ChangeMat(mat);
            g.layer = LayerMask.NameToLayer("BlindWorld");
        }
    }

    public void ResetLevel(int level)      //重新载入游戏的时候根据关卡渲染对应画面
    {
        switch(level)
        {
            case 1:
                GameObjRender(new List<GameObject>(gameObjectsinRoad), RoadMat);
                break;
            case 2:
                GameObjRender(new List<GameObject>(gameObjectsinRoad), RoadMat);
                GameObjRender(new List<GameObject>(gameObjectsinCarRoad), CarRoadMat);
                break;
            case 3:
                GameObjRender(new List<GameObject>(gameObjectsinRoad), RoadMat);
                GameObjRender(new List<GameObject>(gameObjectsinCarRoad), CarRoadMat);
                GameObjRender(new List<GameObject>(gameObjectsinWalkingLights), WalkingLightsMat);
                break;
            case 4:
                GameObjRender(new List<GameObject>(gameObjectsinRoad), RoadMat);
                GameObjRender(new List<GameObject>(gameObjectsinCarRoad), CarRoadMat);
                GameObjRender(new List<GameObject>(gameObjectsinTree), TreeMat);
                GameObjRender(new List<GameObject>(gameObjectsinWalkingLights), WalkingLightsMat);
                break;
            case 5:
                GameObjRender(new List<GameObject>(gameObjectsinRoad), RoadMat);
                GameObjRender(new List<GameObject>(gameObjectsinCarRoad), CarRoadMat);
                GameObjRender(new List<GameObject>(gameObjectsinTree), TreeMat);
                GameObjRender(new List<GameObject>(gameObjectsinPeople), PeopleMat);
                GameObjRender(new List<GameObject>(gameObjectsinWalkingLights), WalkingLightsMat);
                break;
            case 6:
               
                saturabilityEffect.enabled = true;
                GameInfo.Instance.FinishGame();
                break;
            default:
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
