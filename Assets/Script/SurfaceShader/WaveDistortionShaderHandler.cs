using UnityEngine;

public class WaveDistortionShaderHandler : SurfaceShaderHandler
{
    [SerializeField, Range(0, 100f)] float waveSpeed = 0.0f;

    [SerializeField, Range(0, 100f)] float waveAmount = 0.0f;

    public override void UpdateShader(Material targetMaterial)
    {
        if (targetMaterial != null)
        {
            targetMaterial.SetFloat("_WaveSpeed", waveSpeed);
            targetMaterial.SetFloat("_WaveAmount", waveAmount);
        }
    }
}
