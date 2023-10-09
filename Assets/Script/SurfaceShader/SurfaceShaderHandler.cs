using UnityEngine;

/// <summary>
/// Base class for handling surface shaders.
/// </summary>
public class SurfaceShaderHandler : MonoBehaviour, ISurfaceShaderHandler
{
    /// <summary>
    /// The material whose shader parameters will be updated.
    /// </summary>
    public Material TargetMaterial;

    /// <summary>
    /// Updates the shader parameters for the given material.
    /// </summary>
    /// <param name="targetMaterial">The material whose shader parameters will be updated.</param>
    public virtual void UpdateShader(Material targetMaterial)
    {
        // Override in subclasses
    }

    void Update()
    {
        UpdateShader(TargetMaterial);
    }
}
