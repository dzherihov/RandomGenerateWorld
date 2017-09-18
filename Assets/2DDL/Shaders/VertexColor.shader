// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "2D Dynamic Lights/Vertex Color"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Background" }
        Cull Off
        ZWrite Off
        Fog { Mode Off }
    Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        fixed4 _Color;
        struct appdata
        {
            float4 vertex : POSITION;
            float4 color : COLOR;
        };
        struct v2f
        {
            float4 pos : SV_POSITION;
            fixed4 color : COLOR;
        };
        v2f vert (appdata v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.color = v.color;
            return o;
        }
        half4 frag(v2f i) : COLOR
        {
            return i.color * _Color;
        }
        ENDCG
        }
    }
}