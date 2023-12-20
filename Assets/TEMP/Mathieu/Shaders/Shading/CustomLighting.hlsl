void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten) {
	
#if SHADERGRAPH_PREVIEW

	Direction = float3(0.5, 0.5, 0);
	Color = 1;
	DistanceAtten = 1.0f;
	ShadowAtten = 1.0f; 

#else	


		// grab the shadow coordinates
	#if SHADOWS_SCREEN
		half4 clipPos = TransformWorldToHClip(WorldPos);
		half4 shadowCoord = ComputeScreenPos(clipPos);
	#else
		half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
	#endif 

		// grab the main light
	#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
		Light mainLight = GetMainLight(shadowCoord);
	#else
		Light mainLight = GetMainLight();
	#endif


	Direction = mainLight.direction;
	Color = mainLight.color; 
	DistanceAtten = mainLight.distanceAttenuation;
	ShadowAtten = mainLight.shadowAttenuation; 

#endif

}