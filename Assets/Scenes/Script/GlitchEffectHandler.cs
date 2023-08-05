using UnityEngine;
using ComputeShaderHandlerUtils;

public class GlitchEffectHandler : ComputeShaderHandler
{
    [SerializeField] ComputeShader _mainComputeShader = null;
    [SerializeField, Range(0, 1)] float _glitchAmount = 0.01f;
    [SerializeField, Range(0, 100)] float _lineDensity = 50.0f;
    [SerializeField, Range(0, 50000)] float _randomSeed1 = 43758.5453f;
    [SerializeField, Range(0, 50000)] float _randomSeed2 = 12345.6789f;
    [SerializeField, Range(0, 10)] float _effectRate1 = 2.0f;
    [SerializeField, Range(0, 10)] float _effectRate2 = 3.0f;
    [SerializeField, Range(0, 10)] float _effectRate3 = 4.0f;



    ThreadSize _threadSize;

    public override void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture)
    {
        // Get the number of threads in a group
        var kernelIndex = _mainComputeShader.FindKernel("GlitchEffect");
        _mainComputeShader.GetKernelThreadGroupSizes(kernelIndex, out _threadSize.x, out _threadSize.y, out _threadSize.z);

        // Set parameters and run the compute shader
        _mainComputeShader.SetTexture(kernelIndex, "inputTexture", inputTexture);
        _mainComputeShader.SetTexture(kernelIndex, "outputTexture", outputTexture);
        _mainComputeShader.SetFloat("Time", Time.time);
        _mainComputeShader.SetFloat("GLITCH_AMOUNT", _glitchAmount);
        _mainComputeShader.SetFloat("LINE_DENSITY", _lineDensity);
        _mainComputeShader.SetFloat("RANDOM_SEED1", _randomSeed1);
        _mainComputeShader.SetFloat("RANDOM_SEED2", _randomSeed2);
        _mainComputeShader.SetFloat("EFFECT_RATE1", _effectRate1);
        _mainComputeShader.SetFloat("EFFECT_RATE2", _effectRate2);
        _mainComputeShader.SetFloat("EFFECT_RATE3", _effectRate3);

        _mainComputeShader.Dispatch(kernelIndex, inputTexture.width / (int)_threadSize.x, inputTexture.height / (int)_threadSize.y, (int)_threadSize.z);
    }
}
