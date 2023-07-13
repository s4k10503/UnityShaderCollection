// The Linear2Gamma function
void Linear2Gamma(float4 color, out float4 gammaCorrectedColor)
{
    gammaCorrectedColor = color;
    gammaCorrectedColor.rgb = pow(max(color.rgb, float3(0, 0, 0)), float3(1.0 / 2.2, 1.0 / 2.2, 1.0 / 2.2));
    gammaCorrectedColor.a = 1.0;
}
