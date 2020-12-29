﻿Shader "Unlit/Wireframe"
{
    // exposed parameters for the material inspector
    Properties
    {
        _LineColor("Line Color", Color) = (1, 1, 1, 1)
        _SurfaceColor("Surface Color", Color) = (1, 1, 1, 1)
        _LineWidth("Line Width", Float) = 1
    }
    SubShader
    {
        Lighting Off

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
                float4 color : Color;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : POSITION;
                float4 color : Color;
            };

            // re-declaration of the material inspector values
            fixed4 _LineColor;
            fixed4 _SurfaceColor;
            half _LineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                float2 d = fwidth(i.uv);

                float lineY = smoothstep(float(0), d.y * _LineWidth, 1 - i.uv.y);
                float lineX = smoothstep(float(0), d.x * _LineWidth, 1 - i.uv.x);

                float diagonal = smoothstep(float(0), fwidth(i.uv.x - i.uv.y) * _LineWidth, i.uv.x - i.uv.y);

                float4 color = lerp(_LineColor, _SurfaceColor, diagonal * lineX * lineY);
                return color;
            }
            ENDCG
        }
    }
}
