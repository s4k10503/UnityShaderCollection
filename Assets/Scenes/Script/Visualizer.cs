using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;

sealed class Visualizer : MonoBehaviour
{
    [SerializeField] ImageSource _source = null;
    [SerializeField] RawImage _preview = null;
    [SerializeField] ComputeShaderHandler _shaderHandler = null;

    public RenderTexture _sourceTexture;
    private RenderTexture _previewTexture;
    private RenderTexture _tempTexture;

    void Start()
    {
        _sourceTexture = _source.Texture as RenderTexture;

        _previewTexture = new RenderTexture(_sourceTexture.width, _sourceTexture.height, 0);
        _previewTexture.enableRandomWrite = true;
        _previewTexture.Create();

        _tempTexture = new RenderTexture(_sourceTexture.width, _sourceTexture.height, 0);
        _tempTexture.enableRandomWrite = true;
        _tempTexture.Create();
    }

    void Update()
    {
        ImageProcessing();
    }

    private void ImageProcessing()
    {        
        _shaderHandler.RunComputeShader(_sourceTexture, _tempTexture, _previewTexture);
        _preview.texture = _previewTexture;
    }
}