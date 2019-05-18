using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchThings : MonoBehaviour
{
    private Dictionary<GameObject, Material> originMats = new Dictionary<GameObject, Material>();
    public Material outlineMat;
    private int BlindWorldLayer;
    private int DefaultLayer;

    private void Start()
    {
        BlindWorldLayer = LayerMask.NameToLayer("BlindWorld");
        DefaultLayer = LayerMask.NameToLayer("Default");
        this.gameObject.layer = BlindWorldLayer;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.layer == BlindWorldLayer)
        {
            return;     //可见的情况就不需要触摸轮廓了
        }

        Renderer[] renderers = collider.transform.GetComponentsInChildren<Renderer>(false);

        for(int i = 0; i<renderers.Length; ++i)
        {
            if (renderers[i].gameObject.layer == BlindWorldLayer) continue;
            if (!originMats.ContainsKey(renderers[i].gameObject))
                originMats.Add(renderers[i].gameObject, renderers[i].sharedMaterial);
            renderers[i].sharedMaterial = outlineMat;
            renderers[i].gameObject.layer = BlindWorldLayer;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.layer == BlindWorldLayer) return;     //可见的情况就不需要触摸轮廓了

        Renderer[] renderers = collider.transform.GetComponentsInChildren<Renderer>(false);

        for (int i = 0; i < renderers.Length; ++i)
        {            
            if (originMats.ContainsKey(renderers[i].gameObject))
            {
                renderers[i].sharedMaterial = originMats[renderers[i].gameObject];
                originMats.Remove(renderers[i].gameObject);
            }
            renderers[i].gameObject.layer = DefaultLayer;
        }
    }
}
