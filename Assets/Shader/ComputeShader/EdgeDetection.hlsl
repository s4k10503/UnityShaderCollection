void EdgeDetection(RWTexture2D<float4> input, uint2 id, out float4 edgeModifiedColor)
{
    // Sobel operator kernels
    int3x3 sobelX = int3x3(-1, 0, 1, -2, 0, 2, -1, 0, 1);
    int3x3 sobelY = int3x3(-1, -2, -1, 0, 0, 0, 1, 2, 1);

    float4 sumX = 0;
    float4 sumY = 0;
    
    for (int j = -1; j <= 1; j++)
    {
        for (int i = -1; i <= 1; i++)
        {
            float4 color = input[id.xy + int2(i, j)];
            sumX += color * sobelX[j + 1][i + 1];
            sumY += color * sobelY[j + 1][i + 1];
        }
    }
    
    // Calculate the magnitude of the gradients
    float4 edgeColor = sqrt(sumX * sumX + sumY * sumY);
    edgeModifiedColor = input[id.xy] - edgeColor;
}