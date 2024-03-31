Shader "GagaGames/Hole"
{
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {

      Tags {
        "Queue" = "Geometry-1" //This needs to be drawn before "Geometry"
      }

      ColorMask 0
      ZWrite off

      Stencil 
      {
        Ref 1 //Value that will be written to stencil buffer
        Comp always //Always write 1 to the stencil buffer

        Pass replace //One draw call which will replace anything in the frame buffer with this pass
      }

      CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };
        
        void surf (Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
            }
      
      ENDCG
    }
    Fallback "Diffuse"
}
