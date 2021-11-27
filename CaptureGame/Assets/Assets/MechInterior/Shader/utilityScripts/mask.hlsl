void mask_float(float val1, float3 col1, float3 col2, out float3 col_out)
{
    col_out = col1;
    if(val1 > 0.25f)
    {
        col_out = col2;
    }
}