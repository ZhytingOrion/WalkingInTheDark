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
                break;
            case 2:
                //将车道地面改为BlindWorld
                Debug.Log("CarRoadNumber: " + gameObjectsinCarRoad.Length);
                for (int i = 0; i < gameObjectsinCarRoad.Length; ++i)
                {
                    GameObject g = gameObjectsinCarRoad[i];
                    if (g.GetComponent<MatChange>() == null) continue;
                    g.GetComponent<MatChange>().ChangeMat(CarRoadMat);
                    g.layer = LayerMask.NameToLayer("BlindWorld");
                }
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
                break;
        }
    }

    private void GameStateChange(GameState state)
    {
        if (state == GameState.Finish)
        {
            //开启漫游模式

            //视觉恢复
            m_camera.cullingMask = -1;
            m_camera.clearFlags = CameraClearFlags.Skybox;
            StartCoroutine(recoverSaturation(this.ChangeSaturationTime));
        }
    }

    private IEnumerator recoverSaturation(float time)
    {
        SaturabilityEffect effect = m_camera.GetComponent<SaturabilityEffect>();
        for(float i = 0; i<time; i+=Time.deltaTime)
        {
            effect.saturation = Mathf.Clamp01(i * 1.0f / time);
            yield return null;
        }
    }
}
