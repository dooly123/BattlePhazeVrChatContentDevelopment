Shader "BattlePhazeScreen/3D/Toggleable OU SBS"
{
	Properties
	{
		_ScreenTexture("Screen Texture", 2D) = "white" {}
		[Toggle(_)]_OverUnder("Over-Under", Int) = 0
		[Toggle(_)]_SwapEyes("Swap-Eyes", Int) = 0
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha vertex:vertexDataFunc 
		struct Input
		{
			float2 vertexToFrag80;
		};
		uniform sampler2D _ScreenTexture;
		uniform int _SwapEyes;
		uniform int _OverUnder;
		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 temp_cast_0 = (0.5).xx;
			float2 temp_cast_1 = (0.0).xx;
			float2 temp_cast_2 = (0.5).xx;
			float2 temp_cast_3 = (0.5).xx;
			float2 temp_cast_5 = (0.5).xx;
			float2 temp_cast_6 = (0.5).xx;
			float2 temp_cast_7 = (0.5).xx;
			float2 temp_cast_8 = (0.0).xx;
			float2 uv_TexCoord63 = v.texcoord.xy * temp_cast_0 + temp_cast_1;
			float2 uv_TexCoord62 = v.texcoord.xy * temp_cast_2 + temp_cast_3;
			float localStereoEyeIndex73 = ( unity_StereoEyeIndex );
			float AdjustedEyeIndex65 = lerp( localStereoEyeIndex73 , ( -localStereoEyeIndex73 + 1.0 ) , (float)_SwapEyes);
			float lerpResult64 = lerp( uv_TexCoord63.x , uv_TexCoord62.x , AdjustedEyeIndex65);
			float2 appendResult71 = (float2(lerpResult64 , v.texcoord.xy.y));
			float2 uv_TexCoord37 = v.texcoord.xy * temp_cast_5 + temp_cast_6;
			float2 uv_TexCoord36 = v.texcoord.xy * temp_cast_7 + temp_cast_8;
			float2 appendResult53 = (float2(v.texcoord.xy.x , lerp( uv_TexCoord37.y , uv_TexCoord36.y , AdjustedEyeIndex65)));
			float2 lerpResult81 = lerp( appendResult71 , appendResult53 , (float)_OverUnder);
			o.vertexToFrag80 = lerpResult81;
		}
		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}
		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = tex2D( _ScreenTexture, i.vertexToFrag80 ).rgb;
			o.Alpha = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}