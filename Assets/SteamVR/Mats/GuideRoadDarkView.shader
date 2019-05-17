Shader "Unlit/GuideRoadBlindView"
{
	Properties
	{
		_Outline("Outline Width", Float) = 0.2
		_ColorF("Color Front", COLOR) = (0.25,0.5,1,0.5)
		_ColorB("Color Back", COLOR) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 color : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float4 _ColorF;
			float4 _ColorB;
			float _Outline;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 viewPoint = mul(UNITY_MATRIX_MV, v.vertex);
				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float NdotV = dot(-viewPoint, norm);

				float2 offset = TransformViewToProjection(norm.xy);
				//o.vertex.xy += offset * _Outline * floor(-NdotV + 1);

				if (NdotV <= 0.1 && NdotV >= -0.1) o.color = _ColorB;
				else o.color = float4(0, 0, 0, 0);
				//o.color = float4(floor(-NdotV + 1), floor(-NdotV + 1), floor(-NdotV + 1), 1);
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return i.color;
			}
			ENDCG
		}

	}
}
