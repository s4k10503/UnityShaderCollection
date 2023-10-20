using UnityEngine;

public class ComputeShaderHandler : MonoBehaviour, IComputeShaderHandler
{
    public virtual void RunShader(RenderTexture inputTexture, RenderTexture tempTexture, RenderTexture outputTexture)
    {
        // Override in subclasses
    }
}
