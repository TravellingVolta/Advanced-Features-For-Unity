// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// BEGIN unlit_shader
Shader "Custom/SimpleUnlitShader"
{
	Properties
	{
		_Color("Color", Color) = (1.0,1.0,1.0,1)

	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM

			// 定义哪个功能会被着色器使用

			// The 'vert' function will be used as
			// the vertex shader.
			#pragma vertex vert

			// The 'frag' function will be used as
			// the fragment shader.
			#pragma fragment frag

			// Include a number of useful utilities from Unity.
			#include "UnityCG.cginc"

			float4 _Color;

			// 这个结构被赋予每个顶点的顶点着色器
			struct appdata
			{
				// 顶点在世界空间中的位置
				float4 vertex : POSITION;

			};

			//这个结构被赋予每个片段的片段着色器
			struct v2f
			{
				// 片段在屏幕空间中的位置
				float4 vertex : SV_POSITION;
			};

			// 给定一个顶点，将其转换
			v2f vert(appdata v)
			{
				v2f o;

				// 将顶点从世界空间转换为视口空间，方法是将其与Unity提供的矩阵相乘（来自 UnityCG.cginc）
				o.vertex = UnityObjectToClipPos(v.vertex);

				// 将它传回片段着色器
				return o;
			}

			// 给定附近顶点的插值信息，返回最终颜色
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col;

				// 渲染提供的颜色
				col = _Color;

				// 随着时间消逝，开始时是黑色，逐渐淡入_Color
				col *= abs(_SinTime[3]);

				return col;
			}
		ENDCG
		}
	}
}