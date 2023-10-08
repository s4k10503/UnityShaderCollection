using UnityEngine;
using ComputeShaderHandlerUtils;

public class PixelArtHandler : ComputeShaderHandler
{
    [SerializeField] ComputeShader _mainComputeShader = null;
    [SerializeField, Range(0, 2)] float _saturation = 1.0f;
    [SerializeField, Range(0, 2)] float _contrast = 1.0f;
    [SerializeField, Range(1, 64)] int _mosaicSize = 1;
    [SerializeField, Range(1, 256)] int _colorLevel = 256;
    
    ThreadSize _threadSize;
    
    public override void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture)
    {
        // 一つのグループの中に何個のスレッドがあるか
        var kernelIndex = _mainComputeShader.FindKernel("PixelArt");
        _mainComputeShader.GetKernelThreadGroupSizes(kernelIndex, out _threadSize.x, out _threadSize.y, out _threadSize.z);

        // パラメーターを設定してコンピュートシェーダーを実行
        _mainComputeShader.SetTexture(kernelIndex, "inputTexture", inputTexture);
        _mainComputeShader.SetTexture(kernelIndex, "tempTexture", tempTexture);
        _mainComputeShader.SetTexture(kernelIndex, "outputTexture", outputTexture);
        _mainComputeShader.SetInt("mosaicSize", _mosaicSize);
        _mainComputeShader.SetInt("colorLevel", _colorLevel);
        _mainComputeShader.SetInts("dimensions", new int[] { inputTexture.width, inputTexture.height });
        _mainComputeShader.SetFloat("saturation", _saturation);
        _mainComputeShader.SetFloat("contrast", _contrast);

        _mainComputeShader.Dispatch(kernelIndex, inputTexture.width / (int)_threadSize.x, inputTexture.height / (int)_threadSize.y, (int)_threadSize.z);
    }
}