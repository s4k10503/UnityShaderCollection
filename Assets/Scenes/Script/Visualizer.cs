using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;

sealed class Visualizer : MonoBehaviour
{
    [SerializeField] private ImageSource _source = null;
    [SerializeField] private RawImage _preview = null;
    [SerializeField] private ComputeShaderHandler _shaderHandler = null;

    private RenderTexture _sourceTexture;
    private RenderTexture _previewTexture;
    private RenderTexture _tempTexture;

    private void Start()
    {
        InitializeTextures();
    }

    private void Update()
    {
        RunImageProcessing();
    }

    private void InitializeTextures()
    {
        _sourceTexture = _source.Texture as RenderTexture;
        _previewTexture = CreateRenderTexture(_sourceTexture.width, _sourceTexture.height);
        _tempTexture = CreateRenderTexture(_sourceTexture.width, _sourceTexture.height);
    }

    private RenderTexture CreateRenderTexture(int width, int height)
    {
        var renderTexture = new RenderTexture(width, height, 0);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        return renderTexture;
    }

    private void RunImageProcessing()
    {        
        _shaderHandler.RunShader(_sourceTexture, _tempTexture, _previewTexture);
        _preview.texture = _previewTexture;
    }
}
