Shader "Custom/ToonWithOutline" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}  // Main texture
        _RampTex ("Ramp", 2D) = "white" {}      // Ramp texture for toon shading
        _OutlineWidth ("Outline Width", float) = 0.01 // Width of the outline
        _MainColor ("Main Color", Color) = (1,1,1,1)  // Base color of the material
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // Pass for drawing the outline
        Pass {
            Cull Front  // Cull front faces to draw the outline at the back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Vertex data structure
            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            // Output data structure
            struct v2f {
                float4 vertex : SV_POSITION;
            };

            // Outline width property
            float _OutlineWidth;

            // Vertex shader
            v2f vert (appdata v) {
                v2f o;
                v.vertex += float4(v.normal * _OutlineWidth, 0);  // Expand vertices along the normal
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            // Fragment shader
            fixed4 frag (v2f i) : SV_Target {
                return fixed4(0, 0, 0, 1);  // Draw the outline in black
            }
            ENDCG
        }

        // Pass for drawing the main object with toon shading
        CGPROGRAM
        #pragma surface surf ToonRamp
        #pragma target 3.0

        // Sampler for main and ramp textures
        sampler2D _MainTex;
        sampler2D _RampTex;

        // Base color property
        fixed4 _MainColor;

        // Input structure
        struct Input {
            float2 uv_MainTex;
        };

        // Surface shader main function
        void surf (Input IN, inout SurfaceOutput o) {
            // Combine main texture and base color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _MainColor;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        // Lighting model for toon shading
        fixed4 LightingToonRamp (SurfaceOutput s, fixed3 lightDir, fixed atten) {
            // Compute diffuse lighting
            half d = dot(s.Normal, lightDir) * 0.5 + 0.5;
            // Sample the ramp texture to get the toon effect
            fixed3 ramp = tex2D(_RampTex, fixed2(d, 0.5)).rgb;
            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * ramp;
            c.a = s.Alpha;
            return c;
        }
        ENDCG
    }
    FallBack "Diffuse"  // Fallback to default Diffuse shader
}
