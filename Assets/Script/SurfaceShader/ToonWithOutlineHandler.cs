using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonWithOutlineHandler : SurfaceShaderHandler
{
    [SerializeField, Range(0, 0.1f)] float _outlineWidth = 0.0f;

    public override void UpdateShader(Material targetMaterial)
    {
        targetMaterial.SetFloat("_OutlineWidth", _outlineWidth);
    }
}