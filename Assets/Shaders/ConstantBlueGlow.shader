Shader "Custom/ConstantBlueGlow" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (0, 0.5, 1, 1) // Bleu cyan
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 1
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1) // Couleur de base
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _GlowColor;
        half _GlowIntensity;
        fixed4 _BaseColor;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Texture de base
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex) * _BaseColor;
            o.Albedo = texColor.rgb;

            // Glow constant (pas de calcul de distance)
            o.Emission = lerp(texColor.rgb, _GlowColor.rgb, _GlowIntensity);
            o.Alpha = texColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}