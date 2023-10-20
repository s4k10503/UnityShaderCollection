using UnityEngine;

/// <summary>
/// Interface for handling surface shaders.
/// </summary>
public interface IMaterialShaderHandler
{
    /// <summary>
    /// Updates the shader parameters for the given material.
    /// </summary>
    /// <param name="targetMaterial">The material whose shader parameters will be updated.</param>
    void UpdateShader(Material targetMaterial);
}
