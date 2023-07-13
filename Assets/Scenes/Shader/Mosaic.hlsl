void Mosaic(RWTexture2D<float4> input, uint2 id, int mosaicSize, uint2 dimensions, out float4 result)
{    
    uint width = dimensions.x;
    uint height = dimensions.y;

    int2 gridSize = int2(width / mosaicSize, height / mosaicSize);
    int2 gridID = id / mosaicSize;
    float4 colorSum = float4(0.0, 0.0, 0.0, 0.0);

    for (int y = 0; y < mosaicSize; ++y)
    {
        for (int x = 0; x < mosaicSize; ++x)
        {
            int2 pixelID = gridID * mosaicSize + int2(x, y);
            colorSum += input[pixelID];
        }
    }

    float4 averageColor = colorSum / (mosaicSize * mosaicSize);

    result = averageColor;
}
