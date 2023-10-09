using UnityEngine;

public class ComputeShaderHandler : MonoBehaviour, IShaderHandler
{
    public virtual void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture)
    {
        // Override in subclasses
    }
}
