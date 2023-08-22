// Twirl effect function
void ApplyTwirl(Texture2D<float4> inputTexture, uint2 id, float strength, float radiusPercentage, out float4 resultColor)
{
    // Get the dimensions of the input texture
    uint textureWidth, textureHeight;
    inputTexture.GetDimensions(textureWidth, textureHeight);

    // Calculate the effective radius based on the percentage
    float effectiveRadius = radiusPercentage / 2.0f;

    // Normalize pixel coordinates to [0, 1] range
    float2 uv = float2(id.x, id.y) / float2(textureWidth, textureHeight);

    // Compute the twirl effect
    float2 center = float2(0.5, 0.5);
    float2 offset = uv - center;
    float radius = length(offset);
    float angle = atan2(offset.y, offset.x);
    if (radius < effectiveRadius)
    {
        angle += strength * (1.0f - radius / effectiveRadius);
    }
    float2 uvTwirled = center + float2(cos(angle), sin(angle)) * radius;

    // Scale up the twirled coordinates to original texture size
    uvTwirled *= float2(textureWidth, textureHeight);

    // Sample the color from the input texture using the twirled coordinates
    resultColor = inputTexture[uvTwirled];
}

