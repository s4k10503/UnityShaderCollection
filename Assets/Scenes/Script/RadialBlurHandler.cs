using UnityEngine;
using ComputeShaderHandlerUtils;

public class RadialBlurHandler : ComputeShaderHandler
{
    [SerializeField] ComputeShader _mainComputeShader = null;
    [SerializeField, Range(2, 100)] int _sampleCount = 50;
    [SerializeField, Range(0, 0.5f)] float _blurIntensity = 0f;

    ThreadSize _threadSize;

    public override void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture)
    {
        // 一つのグループの中に何個のスレッドがあるか
        var kernelIndex = _mainComputeShader.FindKernel("RadialBlur");
        _mainComputeShader.GetKernelThreadGroupSizes(kernelIndex, out _threadSize.x, out _threadSize.y, out _threadSize.z);

        // パラメーターを設定してコンピュートシェーダーを実行
        _mainComputeShader.SetTexture(kernelIndex, "inputTexture", inputTexture);
        _mainComputeShader.SetTexture(kernelIndex, "outputTexture", outputTexture);
        _mainComputeShader.SetInt("sampleCount", _sampleCount);
        _mainComputeShader.SetFloat("blurIntensity", _blurIntensity);

        _mainComputeShader.Dispatch(kernelIndex, inputTexture.width / (int)_threadSize.x, inputTexture.height / (int)_threadSize.y, (int)_threadSize.z);
    }
}

