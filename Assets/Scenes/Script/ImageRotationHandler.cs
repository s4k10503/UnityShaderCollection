using UnityEngine;
using ComputeShaderHandlerUtils;

public class ImageRotationHandler : ComputeShaderHandler
{
    [SerializeField] ComputeShader _mainComputeShader = null;
    [SerializeField, Range(0, 360)] float _rotationAngle = 0.0f;

    ThreadSize _threadSize;

    public override void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture)
    {
        // 一つのグループの中に何個のスレッドがあるか
        var kernelIndex = _mainComputeShader.FindKernel("RotateImage");
        _mainComputeShader.GetKernelThreadGroupSizes(kernelIndex, out _threadSize.x, out _threadSize.y, out _threadSize.z);

        // パラメーターを設定してコンピュートシェーダーを実行
        _mainComputeShader.SetTexture(kernelIndex, "inputTexture", inputTexture);
        _mainComputeShader.SetTexture(kernelIndex, "tempTexture", tempTexture);
        _mainComputeShader.SetTexture(kernelIndex, "outputTexture", outputTexture);
        _mainComputeShader.SetFloat("rotationAngle", (_rotationAngle * Mathf.Deg2Rad));

        _mainComputeShader.Dispatch(kernelIndex, inputTexture.width / (int)_threadSize.x, inputTexture.height / (int)_threadSize.y, (int)_threadSize.z);
    }
}