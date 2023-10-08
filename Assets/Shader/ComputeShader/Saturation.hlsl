void RGBtoHSV(float4 color, out float3 hsv)
{
    float r = color.r;
    float g = color.g;
    float b = color.b;

    float minV = min(min(r, g), b);
    float maxV = max(max(r, g), b);
    float delta = maxV - minV;

    float h = 0.0;
    float s;
    float v = maxV;

    if (maxV != 0.0)
        s = delta / maxV;
    else
        s = 0.0;

    if (s != 0.0)
    {
        if (r == maxV)
            h = (g - b) / delta;
        else if (g == maxV)
            h = 2.0 + (b - r) / delta;
        else
            h = 4.0 + (r - g) / delta;

        h *= 60.0;

        if (h < 0.0)
            h += 360.0;
    }

    hsv = float3(h, s, v);
}

void HSVtoRGB(float3 hsv, out float4 color)
{
    float h = hsv.x;
    float s = hsv.y;
    float v = hsv.z;

    if (s == 0.0)
    {
        color = float4(v, v, v, 1.0);
        return;
    }

    h /= 60.0;
    int i = floor(h);
    float f = h - i;
    float p = v * (1.0 - s);
    float q = v * (1.0 - s * f);
    float t = v * (1.0 - s * (1.0 - f));

    if (i == 0)
        color = float4(v, t, p, 1.0);
    else if (i == 1)
        color = float4(q, v, p, 1.0);
    else if (i == 2)
        color = float4(p, v, t, 1.0);
    else if (i == 3)
        color = float4(p, q, v, 1.0);
    else if (i == 4)
        color = float4(t, p, v, 1.0);
    else
        color = float4(v, p, q, 1.0);
}

void ChangeSaturation(float4 color, float saturation, out float4 result)
{
    float3 hsv;
    RGBtoHSV(color, hsv);
    hsv.y *= saturation;
    HSVtoRGB(hsv, result);
    result.a = 1.0;
}