// The Contrast function
void Contrast(float4 input, float contrast, out float4 result)
{
    input.rgb = (input.rgb - 0.5) * contrast + 0.5;
    result = input;
}