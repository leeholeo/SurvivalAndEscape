Shader "Custom/blinkshader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Length("Length",Float) = 1
	}
		SubShader
		{
			// No culling or depth
			//Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;
				float _Length;
				fixed4 frag(v2f i) : SV_Target
				{

					float2 tempUV = i.uv;
					float tempLength = 0;

					if (tempUV.x < 0.5)
					{
						tempLength = length(tempUV + float2(0.5,-0.5));
					}
					else
					{
						tempLength = length(float2(1.5, 0.5) - tempUV);
					}
					tempLength *= _Length;
					fixed4 col = tex2D(_MainTex, i.uv);
					col = lerp(col, fixed4(1, 0, 0, 1), 1 - clamp(tempLength, 0, 1));
					return col;
				}
				ENDCG
			}
		}
}
