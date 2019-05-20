using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChange : MonoBehaviour {

    private Material m_Mat;

	// Use this for initialization
	void Start () {
        m_Mat = this.GetComponent<Renderer>().sharedMaterial;
	}

    public void ChangeMat(Material mat)
    {
        this.GetComponent<Renderer>().sharedMaterial = mat;
    }

    public void RecoverMat()
    {
        this.GetComponent<Renderer>().sharedMaterial = m_Mat;
    }
}
