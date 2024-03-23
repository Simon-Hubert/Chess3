Shader "Unlit/Highlights"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        //_PerlinTex ("Perlin Noise", 2D) = "white" {}
        //_Speed ("Speed", Range(0,4)) = 1
        _Tint ("Tint", Color) = (1,1,1,1)
        _Width ("Width", Integer) = 1
    }
    SubShader
    {
        Tags {"RenderType"="Opaque"  "Queue"="BackGround" }
        LOD 100

        Pass
        {
            //Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            Lighting Off
            ZWrite Off
            ZTest Always
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct meshdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Speed;
            sampler2D _MainTex;
            float4 _MainTex_ST, _Tint;
            int _Width;

            Interpolators vert (meshdata v)
            {
                Interpolators i;
                i.vertex = UnityObjectToClipPos(v.vertex);
                i.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return i;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                float pixelSize = float(_Width)/64.0;
                
                if(pixelSize < i.uv.x && i.uv.x < 1-pixelSize && pixelSize < i.uv.y && i.uv.y < 1-pixelSize){
                    discard;
                }


                half4 col = _Tint*3;
                return col;

                // fixed4 noise = tex2D(_PerlinTex, i.uv);
                // //half angle = atan2(i.uv.x * 2.0 + 1.0, i.uv.y * 2.0 +1.0);
                // half rayon = length(float2(0.5,0.5) - i.uv);

                // float scale = 5;
                // float newY = frac((rayon - _Time.x*_Speed)*scale + noise);//Property speed pour le 3
                // float4 grad = float4(newY, newY, newY, newY);
                // grad = pow(grad, float4(2.5, 2.5, 2.5, 2.5));
                
                // return grad * _Tint;
            }
            ENDCG
        }
    }
}
