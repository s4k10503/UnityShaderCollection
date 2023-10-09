Shader "Custom/WaveDistortion" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", float) = 0.0
        _WaveAmount ("Wave Amount", float) = 0.0
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_base v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            sampler2D _MainTex;
            float _WaveSpeed;
            float _WaveAmount;

            fixed4 frag(v2f i) : SV_Target {
                float wave = sin(i.uv.x * _WaveAmount + _Time.y * _WaveSpeed) * 0.1;
                return tex2D(_MainTex, i.uv + float2(wave, wave));
            }
            ENDCG
        }
    }
}
