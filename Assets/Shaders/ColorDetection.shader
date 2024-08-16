Shader "Custom/CubeStarDetection"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _StarMask ("Star Mask", 2D) = "white" {}
        _MouseUV ("Mouse UV", Vector) = (0, 0, 0, 0)
        _StarDetected ("Star Detected", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _StarMask;
        float4 _MouseUV;
        float _StarDetected;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Sample the main texture
            half4 mainColor = tex2D(_MainTex, IN.uv_MainTex);

            // Sample the star mask at the UV coordinates provided by the script
            half maskValue = tex2D(_StarMask, _MouseUV.xy).r;

            // If the mask value is white, set _StarDetected to 1
            if (maskValue > 0.9)
            {
                _StarDetected = 1.0;
                // Optional: Visual feedback
                mainColor.rgb = float3(1,0,0); // Change color to red when hovering over a star
            }
            else
            {
                _StarDetected = 0.0;
            }

            o.Albedo = mainColor.rgb;
            o.Alpha = mainColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
