using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearDepthHandler : MaterialShaderHandler
{
    private Camera _camera;

    [Header("Camera Clipping Planes")]
    [SerializeField, Range(0.01f, 10f)] float _nearClipPlane = 0.3f;
    [SerializeField, Range(10f, 1000f)] float _farClipPlane = 1000f;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, TargetMaterial);
    }

    public override void UpdateShader(Material targetMaterial)
    {
        _camera.nearClipPlane = _nearClipPlane;
        _camera.farClipPlane = _farClipPlane;
    }
}
