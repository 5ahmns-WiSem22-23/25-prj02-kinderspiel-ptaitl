Shader "Unlit/Healthbar"
{
    Properties
    {
        [HideInInspector] _Health ("_Health", Range(0, 1)) = 1

        _BottomThreshold ("Bottom Threshold", Range(0, 1)) = 0.2
        _TopThreshold ("Top Threshold", Range(0, 1)) = 0.8
        _TopColor("Top Color", Color) = (1, 1, 1, 1)
        _BottomColor("Bottom Color", Color) = (0, 0, 0, 0)

        [HideInInspector] _MainTex ("Base (RGB)", 2D) = "white" { }
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha // Alpha Blending

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
                float4 vertex : SV_POSITION;
            };

            float _Health;
            float _BottomThreshold;
            float _TopThreshold;
            float4 _TopColor;
            float4 _BottomColor;

            Interpolators vert (MeshData m)
            {
                Interpolators i;
                i.vertex = UnityObjectToClipPos(m.vertex);
                i.uv = m.uv;
                return i;
            }

            float InverseLerp(float a, float b, float v)
            {
                return (v-a)/(b-a);
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float healthbarMask = _Health > i.uv.x;
                float tHealthColor = saturate(InverseLerp(_BottomThreshold, _TopThreshold, _Health));   
                float3 healthbarColor = lerp(_BottomColor, _TopColor, tHealthColor);

                return float4(healthbarColor, healthbarMask);
            }

            ENDCG
        }
    }
}
