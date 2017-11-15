//Base from
// Alan Zucconi
// www.alanzucconi.com
// Modified by Matthew Oldfield
Shader "Hidden/Heatmap" {
	Properties{
		_PlatetText("Planet Texture",2D) = "white"{}
		_HeatTex("Heat Texture", 2D) = "white" {}
	}
		SubShader{
		Tags{ "Queue" = "Transparent" }
	

		Pass{
		CGPROGRAM
#pragma vertex vert             
#pragma fragment frag

		struct vertInput {
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
		};

		struct vertOutput {
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
			fixed3 worldPos : TEXCOORD1;
		};

		vertOutput vert(vertInput input) {
			vertOutput o;
			o.uv = input.uv;
			o.pos = mul(UNITY_MATRIX_MVP, input.pos);
			o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
			return o;
		}

		uniform int _Points_Length = 0;
		uniform float3 _Points[20];		// (x, y, z) = position
		uniform float _radius[20];	// x = radius, y = intensity
		uniform float _intensity[20];
		sampler2D _HeatTex;
		sampler2D _PlatetText;
		float _globalIntensity;

		half4 frag(vertOutput output) : COLOR{
		// Loops over all the points
		half h = 0;
		half minPoint = 0;
		//tweeks min valuse to allow you to see lower valuse
		if (_globalIntensity > 1)
			minPoint = (_globalIntensity - 1) * 0.01;
		for (int i = 0; i < _Points_Length; i++)
		{
			// Calculates the contribution of each point
			half dis = distance(output.worldPos, _Points[i].xyz);
			half rad = _radius[i];
			half hi = 1 - saturate(dis / (rad));
			h += lerp(minPoint, _intensity[i] * _globalIntensity, hi);
		}

		// Converts (0-1) according to the heat texture
		h = saturate(h);
		float2 heatuv = float2(h,0.5);

		half4 planet = tex2D(_PlatetText, output.uv);
		half4 heatmap =	tex2D(_HeatTex, heatuv);
		half4 color = lerp(planet, heatmap, heatmap.a);
		return color;
		}
			ENDCG
		}
	}
		Fallback "Diffuse"
}