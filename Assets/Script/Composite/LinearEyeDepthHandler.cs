using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LinearEyeDepthHandler : MonoBehaviour
{
    [SerializeField] private ComputeShader _depthProcessing;
    [SerializeField] private ComputeShader _invertedDepthColor;
    [SerializeField] private Shader _getDepthBuffer;

    private Camera _camera;
    private Material _depthMaterial;

    private RenderTexture _depthTexture;
    private RenderTexture _depthTextureProcessed;
    private RenderTexture _invertedColorTexture;
    private Texture2D _tempTexture;

    void Start()
    {
        InitializeCameras();
        InitializeMaterials();
        InitializeTextures();
        InitializeComputeShaders();
    }

    void Update()
    {
        CheckMouseAndShowDepth().Forget();
    }

    private void InitializeCameras()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    private void InitializeMaterials()
    {
        _depthMaterial = new Material(_getDepthBuffer);
    }

    private void InitializeTextures()
    {
        // Initialize render textures with a floating point format for depth
        _depthTexture = CreateRenderTexture(RenderTextureFormat.RFloat);
        _depthTextureProcessed = CreateRenderTexture(RenderTextureFormat.ARGBFloat, true);
        _invertedColorTexture = CreateRenderTexture(RenderTextureFormat.ARGBFloat, true);

        // Create a 1x1 texture for reading depth value
        _tempTexture = new Texture2D(1, 1, TextureFormat.RFloat, false);
    }

    private RenderTexture CreateRenderTexture(RenderTextureFormat format, bool randomWrite = false)
    {
        var rt = new RenderTexture(Screen.width, Screen.height, 24, format)
        {
            enableRandomWrite = randomWrite
        };
        rt.Create();
        return rt;
    }

    private void InitializeComputeShaders()
    {
        _depthProcessing.SetTexture(0, "Result", _depthTextureProcessed);
        _depthProcessing.SetFloat("NearClip", _camera.nearClipPlane);
        _depthProcessing.SetFloat("FarClip", _camera.farClipPlane);

        _invertedDepthColor.SetTexture(0, "Input", _depthTextureProcessed);
        _invertedDepthColor.SetTexture(0, "Result", _invertedColorTexture);
    }

    public async UniTask CheckMouseAndShowDepth()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            float depth = await GetDistanceAsync(mousePosition);
            Debug.Log($"Depth at mouse position ({mousePosition.x}, {mousePosition.y}): {depth}");
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, _depthTexture, _depthMaterial);

        // Set the depth texture in compute shader, and run the compute shader
        _depthProcessing.SetTexture(0, "DepthTexture", _depthTexture);
        _depthProcessing.Dispatch(0, _depthTextureProcessed.width / 32, _depthTextureProcessed.height / 32, 1);

        _invertedDepthColor.SetTexture(0, "DepthTexture", _depthTextureProcessed);
        _invertedDepthColor.Dispatch(0, _invertedColorTexture.width / 32, _invertedColorTexture.height / 32, 1);

        Graphics.Blit(_invertedColorTexture, dest);
    }

    public async UniTask<float> GetDistanceAsync(Vector2 screenCoordinates)
    {
        // Convert screen coordinates to texture coordinates
        int texX = (int)(screenCoordinates.x * _depthTexture.width / Screen.width);
        int texY = _depthTexture.height - (int)(screenCoordinates.y * _depthTexture.height / Screen.height) - 1;

        // Read pixel from the processed depth texture
        RenderTexture.active = _depthTextureProcessed;
        _tempTexture.ReadPixels(new Rect(texX, texY, 1, 1), 0, 0);
        _tempTexture.Apply();

        // The depth value is stored in the red channel
        float depthValue = _tempTexture.GetPixel(0, 0).r;

        // Convert the depth value from view space to world space
        Vector3 worldSpacePoint = _camera.ScreenToWorldPoint(new Vector3(screenCoordinates.x, screenCoordinates.y, depthValue));

        // Subtract the camera position to get the actual distance from the camera
        float actualDistance = Vector3.Distance(worldSpacePoint, _camera.transform.position);

        return actualDistance;
    }

    private void OnDestroy()
    {
        // Cleanup when the script is destroyed
        if (_depthTexture != null) _depthTexture.Release();
        if (_depthTextureProcessed != null) _depthTextureProcessed.Release();
        if (_invertedColorTexture != null) _invertedColorTexture.Release();
        if (_tempTexture != null) Destroy(_tempTexture);
        if (_depthMaterial != null) Destroy(_depthMaterial);
    }
}