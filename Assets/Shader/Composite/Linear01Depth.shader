Shader "Custom/LinearDepth"
{
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _CameraDepthTexture;
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };
            
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            half4 frag (v2f i) : COLOR
            {
                float linear01Depth = Linear01Depth (tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)).r);
                half4 depthValue;
                depthValue = linear01Depth;
                return depthValue;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"   // Fallback to default Diffuse shader
}
