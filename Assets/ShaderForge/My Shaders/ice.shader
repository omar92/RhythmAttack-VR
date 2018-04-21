// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33193,y:32530,varname:node_9361,prsc:2|diff-9345-RGB,diffpow-5763-OUT,spec-646-OUT,gloss-5763-OUT,normal-3988-OUT,emission-2177-OUT,transm-1354-OUT,lwrap-2177-OUT,alpha-7431-OUT,refract-1354-OUT;n:type:ShaderForge.SFN_Color,id:9345,x:33215,y:32362,ptovrint:False,ptlb:color,ptin:_color,varname:node_9345,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6764706,c2:1,c3:0.9598377,c4:1;n:type:ShaderForge.SFN_Fresnel,id:4817,x:32562,y:32774,varname:node_4817,prsc:2|EXP-8173-OUT;n:type:ShaderForge.SFN_Vector1,id:8173,x:32394,y:32774,varname:node_8173,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:260,x:32741,y:32748,varname:node_260,prsc:2|A-4817-OUT,B-991-OUT;n:type:ShaderForge.SFN_ValueProperty,id:991,x:32562,y:32935,ptovrint:False,ptlb:Fresnel Strength,ptin:_FresnelStrength,varname:node_991,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2177,x:32914,y:32748,varname:node_2177,prsc:2|A-260-OUT,B-1551-RGB,C-5878-RGB;n:type:ShaderForge.SFN_Tex2d,id:5878,x:32741,y:33126,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:node_5878,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fc71155bd658e5040a44e720aab1ec06,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Cubemap,id:1551,x:32741,y:32952,ptovrint:False,ptlb:Cubemap,ptin:_Cubemap,varname:node_1551,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:d0653b491c407ed4f83fd8228f4b93d4,pvfc:0;n:type:ShaderForge.SFN_Slider,id:7431,x:32894,y:33057,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_7431,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4938548,max:1;n:type:ShaderForge.SFN_Tex2d,id:5002,x:32433,y:32536,ptovrint:False,ptlb:node_5002,ptin:_node_5002,varname:node_5002,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2dd65c410acc7b840a5d2c58e5722f25,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:4684,x:32070,y:32455,ptovrint:False,ptlb:Distortion Strength,ptin:_DistortionStrength,varname:node_4684,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:-0.3596942,max:2;n:type:ShaderForge.SFN_Multiply,id:9536,x:32644,y:32525,varname:node_9536,prsc:2|A-4684-OUT,B-5002-RGB;n:type:ShaderForge.SFN_ComponentMask,id:1354,x:32809,y:32525,varname:node_1354,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-9536-OUT;n:type:ShaderForge.SFN_Vector1,id:5763,x:32874,y:32364,varname:node_5763,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:646,x:32874,y:32422,varname:node_646,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Lerp,id:3988,x:32723,y:32226,varname:node_3988,prsc:2|A-9179-OUT,B-9536-OUT,T-6705-OUT;n:type:ShaderForge.SFN_Vector3,id:9179,x:32510,y:32176,varname:node_9179,prsc:2,v1:0,v2:0,v3:111;n:type:ShaderForge.SFN_Vector1,id:6705,x:32510,y:32260,varname:node_6705,prsc:2,v1:0.5;proporder:9345-991-1551-5878-7431-5002-4684;pass:END;sub:END;*/

Shader "Shader Forge/ice" {
    Properties {
        _color ("color", Color) = (0.6764706,1,0.9598377,1)
        _FresnelStrength ("Fresnel Strength", Float ) = 1
        _Cubemap ("Cubemap", Cube) = "_Skybox" {}
        _MainTexture ("Main Texture", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.4938548
        _node_5002 ("node_5002", 2D) = "bump" {}
        _DistortionStrength ("Distortion Strength", Range(-2, 2)) = -0.3596942
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _color;
            uniform float _FresnelStrength;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform samplerCUBE _Cubemap;
            uniform float _Opacity;
            uniform sampler2D _node_5002; uniform float4 _node_5002_ST;
            uniform float _DistortionStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _node_5002_var = UnpackNormal(tex2D(_node_5002,TRANSFORM_TEX(i.uv0, _node_5002)));
                float3 node_9536 = (_DistortionStrength*_node_5002_var.rgb);
                float3 normalLocal = lerp(float3(0,0,111),node_9536,0.5);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_1354 = node_9536.g;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + float2(node_1354,node_1354);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_5763 = 1.0;
                float gloss = node_5763;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_646 = 0.5;
                float3 specularColor = float3(node_646,node_646,node_646);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 node_2177 = ((pow(1.0-max(0,dot(normalDirection, viewDirection)),0.0)*_FresnelStrength)*texCUBE(_Cubemap,viewReflectDirection).rgb*_MainTexture_var.rgb);
                float3 w = node_2177*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = pow(max(float3(0.0,0.0,0.0), NdotLWrap + w ), node_5763);
                float3 backLight = pow(max(float3(0.0,0.0,0.0), -NdotLWrap + w ), node_5763) * float3(node_1354,node_1354,node_1354);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = _color.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = node_2177;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
