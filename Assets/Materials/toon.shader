// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/toon"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _ContourSize("Contour Size", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "Queue" = "Geometry"}
        Pass
        {

            CGPROGRAM

            #pragma target 3.0
            #pragma vertex mainVS
            #pragma fragment mainPS


            float4 _Color;
            float _ContourSize;

            struct VS_OUT
            {
                float4 hPos : SV_Position;
                float3 N : NORMAL;
                float3 V : TEXCOORD2;
            };


            VS_OUT mainVS(float4 position : POSITION, float3 normal : NORMAL) {

                VS_OUT OUT = (VS_OUT)0;
                OUT.hPos = UnityObjectToClipPos(position);
                float3 wPos = mul(unity_ObjectToWorld, position).xyz;
                float3 wNormal = mul(float4(normal, 0), unity_WorldToObject).xyz;
                OUT.N = normalize(wNormal);
                OUT.V = normalize(_WorldSpaceCameraPos.xyz - wPos);
                return OUT;
            }

            float4 mainPS(VS_OUT IN) : SV_Target{
                float3 V = normalize(IN.V);
                float3 N = normalize(IN.N);
                float contour = dot(N, V) > _ContourSize;
                contour *= contour;
                float4 finalColor = _Color * contour;
                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
