using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCam : MonoBehaviour {
    private Camera AttachedCamera;
    private Camera TempCam;
    public Shader onliLightShader;
    public Shader onliLightShader2;
    // Use this for initialization
    void Start () {
        AttachedCamera = GetComponent<Camera>();
        TempCam = new GameObject().AddComponent<Camera>();
        TempCam.enabled = false;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        TempCam.CopyFrom(AttachedCamera);
        TempCam.clearFlags = CameraClearFlags.Color;
        TempCam.backgroundColor = Color.black;
        TempCam.cullingMask = 1 << LayerMask.NameToLayer("Enemy");
        TempCam.targetTexture = destination;
        TempCam.RenderWithShader(onliLightShader, "");
    }
}
