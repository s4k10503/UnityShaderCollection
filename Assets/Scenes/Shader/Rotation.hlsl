// The Rotate function
void Rotation(Texture2D<float4> InputTexture, float2 uv, float RotationAngle, out float4 sample)
{
    uint Width, Height;
    InputTexture.GetDimensions(Width, Height);

    uv = uv * 2.0 - 1.0;

    float angle = RotationAngle;
    float cosAngle = cos(angle);
    float sinAngle = sin(angle);
    float2x2 rot = float2x2(cosAngle, -sinAngle, sinAngle, cosAngle);

    float2 uvRotated = mul(rot, uv);
    uvRotated = uvRotated / 2.0 + 0.5;
    uvRotated = clamp(uvRotated, 0.0, 1.0);

    if (any(uvRotated < 0.0) || any(uvRotated > 1.0))
    {
        sample = float4(0.0, 0.0, 0.0, 0.0);
    }
    else
    {
        uvRotated = uvRotated * float2(Width, Height) - 0.5;
        int2 uvInt = int2(uvRotated);
        float2 uvFrac = frac(uvRotated);

        float4 tl = InputTexture[uvInt]; // Top-left
        float4 tr = InputTexture[uvInt + int2(1, 0)]; // Top-right
        float4 bl = InputTexture[uvInt + int2(0, 1)]; // Bottom-left
        float4 br = InputTexture[uvInt + int2(1, 1)]; // Bottom-right

        float4 t = lerp(tl, tr, uvFrac.x); // Top interpolation
        float4 b = lerp(bl, br, uvFrac.x); // Bottom interpolation

        sample = lerp(t, b, uvFrac.y); // Final interpolation
    }
}
