Shader "Custom/Dissolve" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture (RGB)", 2D) = "white" {}
		_DissolveMaterial("Dissolve Material (RGB)", 2D) = "white" {}
		_DissolveFactor("Dissolve Factor", Range(0.0, 1.0)) = 0.5
	}
	
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Cull Off
		CGPROGRAM
		
		#pragma surface surf Lambert addshadow
		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_DissolveMaterial;
			float _DissolveFactor;
			float3 worldPos;
		};

		sampler2D _MainTex;
		sampler2D _DissolveMaterial;
		float _DissolveFactor;
		fixed4 _Color;

		void surf(Input IN, inout SurfaceOutput o) 
		{
			float3 localPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
			localPos.y += 1.1;

			float3 dissolveTexture = tex2D(_DissolveMaterial, IN.uv_DissolveMaterial).rgb - _DissolveFactor*localPos.y*3.5;
			clip(dissolveTexture);

			half3 emission = half3(0, 0, 0);
			if (any(dissolveTexture < 0.05))
				emission = half3(1, 1, 1);
			
			o.Emission = emission;
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
		}
		ENDCG

	}
	Fallback "Diffuse"
}