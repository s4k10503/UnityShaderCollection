using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAngleTransparencyHandler : SurfaceShaderHandler
{
    [SerializeField, Range(0, 1.5f)] float _alphaMultiplier = 1.5f;

    public override void UpdateShader(Material targetMaterial)
    {
        targetMaterial.SetFloat("_AlphaMultiplier", _alphaMultiplier);
    }
}