void Shading_float(in float3 Normal, in float RampSmoothness, in float3 ClipSpacePos, in float3 WorldPos, in float4 RampTinting,
	in float RampOffset, out float3 RampOutput, out float3 Direction)
{

	// set the shader graph node previews
#ifdef SHADERGRAPH_PREVIEW
	RampOutput = float3(0.5, 0.5, 0);
	Direction = float3(0.5, 0.5, 0);
#else

	// grab the shadow coordinates
#if SHADOWS_SCREEN
	half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif 

	// grab the main light
#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
	Light light = GetMainLight(shadowCoord);
#else
	Light light = GetMainLight();
#endif

	// dot product for toonramp
	half d = dot(Normal, light.direction) * 0.5 + 0.5;

	// toonramp in a smoothstep
	half ramp = smoothstep(RampOffset, RampOffset + RampSmoothness, d);
	// multiply with shadows;
	ramp *= light.shadowAttenuation;
	// add in lights and extra tinting
	RampOutput = light.color * (ramp + RampTinting);
	// output direction for rimlight
	Direction = light.direction;
#endif

}