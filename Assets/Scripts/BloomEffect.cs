﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BloomEffect : MonoBehaviour {
    public Transform sunTransform;
    public Color starColor = Color.white;
    [Range(1, 20)]
    public float blurSize = 1f;
    [Range(1, 400)]
    public float circleSize = 200f;


    private Shader StarBloom;
    private Material m_material;
    
    // Use this for initialization
    void OnEnable () {
        //StarBloom = ;
        m_material = new Material(Shader.Find("Hidden/StarBloom"));
        //m_material.SetVector("_SunPosition", sunTransform.position);
        Debug.Log(sunTransform.position);
    }
	
	// Update is called once per frame
	void Update()
    {
        Vector3 screenCoordinate = Camera.main.WorldToScreenPoint(sunTransform.position);
        Vector3 viewportCoordinate = Camera.main.WorldToViewportPoint(sunTransform.position);
        if (viewportCoordinate.x > 0 && viewportCoordinate.x < 1.1 && viewportCoordinate.y > -0.1 && viewportCoordinate.y < 1.0 && viewportCoordinate.z > -0.1)
        {
            screenCoordinate = new Vector3(screenCoordinate.x, Screen.height - screenCoordinate.y , screenCoordinate.z);
            m_material.SetVector("_SunPosition", screenCoordinate);
        }
        else
        {
            screenCoordinate = new Vector3(-1,-1,-1);
            m_material.SetVector("_SunPosition", screenCoordinate);
        }
        m_material.SetFloat("_Size", blurSize);
        m_material.SetFloat("_CircleBlurSize", circleSize);
        m_material.SetVector("_StarColor", starColor);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_material);
    }
}
