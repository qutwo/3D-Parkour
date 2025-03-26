Shader "Unlit/Achivement"
{
    Properties
    {
         _Color ("color",Color) = (1,1,1,1)
         _FresnelColor ("color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent"
               "Queue" = "Transparent"
            }
      

        Pass
        {
            Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            float4 _Color;
            float4 _FresnelColor;

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
               
                float3 normals:NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normals : TEXCOORD1;
                float3  wPos : TEXCOORD2;
    
         
            };

           

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normals = UnityObjectToWorldNormal(v.normals);
                o.wPos = mul(unity_ObjectToWorld,v.vertex);
                return o;
            
        
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 N = normalize(i.normals);
                float3 V = normalize(_WorldSpaceCameraPos - i.wPos);
                float3 fresnel = saturate(1- dot(V,N));
                fresnel = pow(fresnel,1)* _FresnelColor;
                float3 col = saturate(fresnel*0.5f + _Color*0.5f);
                return float4(col,1);
            }
            ENDCG
        }
    }
}
