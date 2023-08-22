using UnityEngine;
using ComputeShaderHandlerUtils;

public class TwirlEffectHandler : ComputeShaderHandler
{
    [SerializeField] ComputeShader _mainComputeShader = null;
    [SerializeField, Range(-10.0f, 10.0f)] float _strength = 0.0f;
    [SerializeField, Range(0, 1)] float _radiusPercentage = 0.0f;

    ThreadSize _threadSize;

    public override void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture)
    {
        // 一つのグループの中に何個のスレッドがあるか
        var kernelIndex = _mainComputeShader.FindKernel("Twirl");
        _mainComputeShader.GetKernelThreadGroupSizes(kernelIndex, out _threadSize.x, out _threadSize.y, out _threadSize.z);

        // パラメーターを設定してコンピュートシェーダーを実行
        _mainComputeShader.SetTexture(kernelIndex, "inputTexture", inputTexture);
        _mainComputeShader.SetTexture(kernelIndex, "outputTexture", outputTexture);
        _mainComputeShader.SetFloat("strength", _strength);
        _mainComputeShader.SetFloat("radiusPercentage", _radiusPercentage);

        _mainComputeShader.Dispatch(kernelIndex, inputTexture.width / (int)_threadSize.x, inputTexture.height / (int)_threadSize.y, (int)_threadSize.z);
    }
}
