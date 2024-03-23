Shader "Unlit/OutlineByS"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Outline Color", Color) = (1,1,1,1)
        _Width ("Width", Integer) = 1
        _Outline ("Outline Toggle", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  "Queue"="Geometry"}
        Cull Off
		Lighting Off
		ZWrite Off
        LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Width, _Outline;
            float4 _MainTex_ST;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float2 pixelSize = float2(_Width/64, _Width/64);
                float up = tex2D(_MainTex, i.uv + float2(0.0,pixelSize.x)).w;
                float down = tex2D(_MainTex, i.uv + float2(0.0,-pixelSize.x)).w;
                float right = tex2D(_MainTex, i.uv + float2(pixelSize.x,0.0)).w;
                float left = tex2D(_MainTex, i.uv + float2(-pixelSize.x,0.0)).w;

                float outline = max(max(up, down), max(right, left)) - col.w;
                return lerp(col, _Color, _Outline*outline);
            }
            ENDCG
        }
    }
}
