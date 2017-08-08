// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Ludus/WobblyMesh" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount ("Extrusion Amount", Range(0,1)) = 0.5
		_NoiseTex ("Noise", 2D) = "white"{}
		_NoiseDamper ("Noise Damper", Range(1,100)) = 50
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      	_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
//		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		 
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		float _Amount;
		sampler2D _NoiseTex;
		float _NoiseDamper;
		float4 _RimColor;
      	float _RimPower;

		void vert (inout appdata_full v) {
			float4 offset = float4(
			tex2Dlod(_NoiseTex, float4(v.vertex.x + _Time.y,0,0,0)).r,
			0,0,0);

			v.vertex.xyz += v.normal * _Amount + (offset.x / _NoiseDamper);
 	    }

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END


//		float4x4 _World2Camera;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          	o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}

//Properties {
//      _MainTex ("Texture", 2D) = "white" {}
//      _Amount ("Extrusion Amount", Range(-1,1)) = 0.5
//    }
//    SubShader {
//      Tags { "RenderType" = "Opaque" }
//      CGPROGRAM
//      #pragma surface surf Lambert vertex:vert
//      struct Input {
//          float2 uv_MainTex;
//      };
//      float _Amount;
//      void vert (inout appdata_full v) {
//          v.vertex.xyz += v.normal * _Amount;
//      }
//      sampler2D _MainTex;
//      void surf (Input IN, inout SurfaceOutput o) {
//          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
//      }
//      ENDCG
//    } 
//    Fallback "Diffuse"
//  }