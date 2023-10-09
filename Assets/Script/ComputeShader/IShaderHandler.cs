using UnityEngine;

public interface IShaderHandler
{
    void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture);
}
