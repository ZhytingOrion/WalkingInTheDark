Shader "Custom/OutlineShader"
{
	Properties
	{
		_OutlineColor ("Outline Color", COLOR) = (1,1,1,1)
		_Outline("Outline Width", Range(0,1)) = 0.5
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		//考虑第一步colormask 0，zwrite on 写深度
		//第二步扩展法线，写颜色
		
		Pass
		{
			Cull Back
			ZWrite On
			ColorMask 0

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = fixed4(0,0,0,0);
				return col;
			}
			ENDCG
		}

		Pass
		{
			Cull Front
			Blend SrcAlpha DstAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _OutlineColor;
			float _Outline;

			v2f vert(appdata v)
			{
				v2f o;
				float4 pos = mul(UNITY_MATRIX_MV, v.vertex);
				float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				pos = pos + float4(normalize(normal), 0) * _Outline;
				o.vertex = mul(UNITY_MATRIX_P, pos);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _OutlineColor;
			}
			ENDCG
		}
	}
}
