Shader "GagaGames/SeeThrough"
{
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Color ("Colour", Color) = (1,1,1,1)

    }
    SubShader {

      Tags {
        "Queue" = "Geometry"
      }

      Stencil 
      {
        Ref 1
        Comp notequal //If the stencil value is not equal to what's already in the stencil buffer then

        Pass keep //Keep the pixel that belongs to the wall
      }

      CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        
        void surf (Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb * _Color.rgb;
            }
      
      ENDCG
    }
    Fallback "Diffuse"
}
