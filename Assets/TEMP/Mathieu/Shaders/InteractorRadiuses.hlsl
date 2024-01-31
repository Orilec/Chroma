float4 _ShaderInteractorsPositions[200];
float _ShaderInteractorsRadiuses[200];

float _ShapeCutoff;
float _ShapeSmoothness;


void InteractorRadiuses_float(in float3 WorldPos, out float Shapes)
{
#ifdef SHADERGRAPH_PREVIEW
    Shapes = 0;
#else
    float spheres = 0;
    for (int i = 0; i < 200; i++)
    {
        // spheres
        float3 dis = distance(_ShaderInteractorsPositions[i].xyz, WorldPos);
        float sphereR = 1 - saturate(dis / _ShaderInteractorsRadiuses[i]).r;
        sphereR = (smoothstep(_ShapeCutoff, _ShapeCutoff + _ShapeSmoothness, sphereR));
        spheres += (sphereR);

    }
    // all combined      
    Shapes = saturate(spheres);
#endif
}