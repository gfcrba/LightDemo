Shader "Unlit/VolumetricFog"
{
	Properties
	{
		_HeightFalloff("Height falloff", float) = 0.1
		_FogGlobalDensity("Fog density", float) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float _HeightFalloff;
			float _FogGlobalDensity;

			float ComputeVolumetricFog(in float3 worldPos, in float3 cameraToWorldPos)
			{
				// NOTE:
				float cVolFogHeightDensityAtViewer = exp( -_HeightFalloff * _WorldSpaceCameraPos.z );
				float fogInt = length(cameraToWorldPos) * cVolFogHeightDensityAtViewer;
				const float cSlopeThreshold = 0.01;
				if (abs(cameraToWorldPos.z) > cSlopeThreshold)
				{
					float t = _HeightFalloff * cameraToWorldPos.z;
					fogInt *= (1.0 - exp(-t)) / t;
				}

				return exp(-_FogGlobalDensity * fogInt);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 pos = i.vertex.xyz - _WorldSpaceCameraPos;
				float fog = ComputeVolumetricFog(i.vertex.xyz, pos);
				return float4(1.0, 0.0, 0.0, 1.0) * fog;
			}
			ENDCG
		}
	}
}
