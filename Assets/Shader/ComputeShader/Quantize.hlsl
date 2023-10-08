void Quantize(float4 input, int colorLevel, out float4 result)
{
    float4 color = input;

    color.rgb *= colorLevel;
    color.rgb = round(color.rgb);
    color.rgb /= colorLevel;

    result = color;
}
