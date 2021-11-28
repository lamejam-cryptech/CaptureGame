
void inputSample_float(float2 uv, float v, float threshold, float disp, out float2 newUV)
{
    newUV = uv;
    if (abs(uv.y - v) < threshold)
    {
        newUV.x += disp * (1 - abs(uv.y - v));
    }
}