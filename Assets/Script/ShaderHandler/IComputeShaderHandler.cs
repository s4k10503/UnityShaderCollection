using UnityEngine;

public interface IComputeShaderHandler
{
    void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture);
}
