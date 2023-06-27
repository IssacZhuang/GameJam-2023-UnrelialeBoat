Shader "Unlit/Animated"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _LightIntensity ("Light Intensity", float) = 1

        [ShowAsVector2] _Splits ("Splits", Vector) = (4,4,0,0)
        _Frame ("Frame", Float) = 0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ColorMask RGB
        Lighting Off ZWrite Off
        
        Pass
        {
            //Blend SrcAlpha OneMinusSrcAlpha
            Blend SrcAlpha One, SrcAlpha One
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
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float t: TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _MainTex_ST;
            fixed4 _Color;
            fixed _LightIntensity;

            uint4 _Splits;
            float _Frame;

            float2 GetUV(float2 uv, float dx, float dy , int stage)
            {
                return float2(
                    (uv.x * dx) + fmod(stage, _Splits.x) * dx,
                    1.0 - ((uv.y * dy) + (stage / _Splits.y) * dy)
                );
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                int Stages = _Splits.x * _Splits.y;
                float Stage = fmod(_Frame, Stages);

                int current = floor(Stage); 
                int next = floor(fmod(Stage + 1, Stages));

                float dx = 1.0 / _Splits.x; 
                float dy = 1.0 / _Splits.y;

                

                o.uv1 = GetUV(v.uv, dx, dy, current);

                o.uv2 = GetUV(v.uv, dx, dy, next);

                o.t = Stage - current;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 finalColor = lerp(tex2D(_MainTex, i.uv1), tex2D(_MainTex, i.uv2), i.t) * _Color * _LightIntensity;
                finalColor.a = saturate(finalColor.a);
                return finalColor;
            }
            ENDCG
        }
    }

}
