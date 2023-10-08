// The rotation function
void ImageRotation(Texture2D<float4> inputTexture, uint2 id, float rotationAngle, out float4 resultColor)
{
    // Get the dimensions of the input texture
    uint textureWidth, textureHeight;
    inputTexture.GetDimensions(textureWidth, textureHeight);

    // Normalize pixel coordinates to [-0.5, 0.5] range
    float2 uv =  float2(id.x, id.y) / float2(textureWidth, textureHeight) - 0.5;

    // Compute rotation matrix based on the rotation angle
    float2x2 rotationMatrix = float2x2(cos(rotationAngle), -sin(rotationAngle), sin(rotationAngle), cos(rotationAngle));

    // Rotate the pixel position and shift back to [0, 1] range
    float2 uvRotated = mul(rotationMatrix, uv) + 0.5;

    // If rotated pixel is out of bounds, set pixel color to black
    if (any(uvRotated < 0.0) || any(uvRotated > 1.0))
    {
        resultColor = float4(0.0, 0.0, 0.0, 1.0);
    }
    else
    {
        // Scale up the rotated coordinates to original texture size
        uvRotated *= float2(textureWidth, textureHeight);

        // Compute the integer and fractional parts of the coordinates
        int2 uvInt = int2(uvRotated);
        float2 uvFrac = frac(uvRotated);

        // Get the color of four neighboring pixels
        float4 topLeftColor = inputTexture[uvInt]; // Top-left
        float4 topRightColor = inputTexture[uvInt + int2(1, 0)]; // Top-right
        float4 bottomLeftColor = inputTexture[uvInt + int2(0, 1)]; // Bottom-left
        float4 bottomRightColor = inputTexture[uvInt + int2(1, 1)]; // Bottom-right

        // Perform bilinear interpolation to compute final color of the rotated pixel
        float4 topInterpolation = lerp(topLeftColor, topRightColor, uvFrac.x); // Top interpolation
        float4 bottomInterpolation = lerp(bottomLeftColor, bottomRightColor, uvFrac.x); // Bottom interpolation
        resultColor = lerp(topInterpolation, bottomInterpolation, uvFrac.y); // Final interpolation
    }
}
