Shader "Custom/TorchLightShader" {
	SubShader {
		Tags { "RenderType"="Opaque" }
		ZWrite Off
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		struct Input {
			float4 color: COLOR;
		};
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = float4(1,0,0,0);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
