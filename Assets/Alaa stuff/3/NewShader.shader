// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32274,y:32502,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f405a011dc840984496a5b67fdc34865,ntxv:0,isnm:False|UVIN-8980-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32513,y:32735,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT,E-4092-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:3;n:type:ShaderForge.SFN_ValueProperty,id:1806,x:31744,y:32871,ptovrint:False,ptlb:x speed,ptin:_xspeed,varname:node_1806,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6251,x:31744,y:32954,ptovrint:False,ptlb:y speed,ptin:_yspeed,varname:node_6251,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_Append,id:7840,x:31931,y:32871,varname:node_7840,prsc:2|A-1806-OUT,B-6251-OUT;n:type:ShaderForge.SFN_Multiply,id:1481,x:31983,y:32743,varname:node_1481,prsc:2|A-2452-T,B-7840-OUT;n:type:ShaderForge.SFN_Time,id:2452,x:31744,y:32693,varname:node_2452,prsc:2;n:type:ShaderForge.SFN_Add,id:8980,x:32002,y:32547,varname:node_8980,prsc:2|A-2258-OUT,B-1481-OUT;n:type:ShaderForge.SFN_Tex2d,id:5139,x:32212,y:33263,ptovrint:False,ptlb:ramp,ptin:_ramp,varname:node_5139,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e66926eec2ca52e438754c4e89724105,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:4092,x:32493,y:32896,varname:node_4092,prsc:2|A-6074-A,B-5139-R;n:type:ShaderForge.SFN_ValueProperty,id:3784,x:31244,y:32407,ptovrint:False,ptlb:x speed_copy,ptin:_xspeed_copy,varname:_xspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:8402,x:31244,y:32490,ptovrint:False,ptlb:y speed_copy,ptin:_yspeed_copy,varname:_yspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_Append,id:1831,x:31432,y:32407,varname:node_1831,prsc:2|A-3784-OUT,B-8402-OUT;n:type:ShaderForge.SFN_Multiply,id:502,x:31484,y:32280,varname:node_502,prsc:2|A-7124-T,B-1831-OUT;n:type:ShaderForge.SFN_Time,id:7124,x:31244,y:32229,varname:node_7124,prsc:2;n:type:ShaderForge.SFN_Add,id:9153,x:31484,y:32083,varname:node_9153,prsc:2|A-2288-UVOUT,B-502-OUT;n:type:ShaderForge.SFN_TexCoord,id:2288,x:31216,y:32027,varname:node_2288,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:1701,x:31656,y:32083,ptovrint:False,ptlb:noise texture,ptin:_noisetexture,varname:node_1701,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5f6015725b1e6ca4fb862866340e088d,ntxv:0,isnm:False|UVIN-9153-OUT;n:type:ShaderForge.SFN_Slider,id:8853,x:31777,y:32259,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_8853,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2950273,max:1;n:type:ShaderForge.SFN_Lerp,id:7396,x:32059,y:32088,varname:node_7396,prsc:2|A-5389-UVOUT,B-1701-RGB,T-8853-OUT;n:type:ShaderForge.SFN_TexCoord,id:5389,x:31856,y:32067,varname:node_5389,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:8048,x:32553,y:32478,ptovrint:False,ptlb:noise mask,ptin:_noisemask,varname:node_8048,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ec0b122b8eff0e445a89609b977dd539,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:2258,x:32336,y:32085,varname:node_2258,prsc:2|A-4541-UVOUT,B-7396-OUT,T-8048-RGB;n:type:ShaderForge.SFN_TexCoord,id:4541,x:32059,y:31929,varname:node_4541,prsc:2,uv:0,uaff:False;proporder:6074-797-1806-6251-5139-3784-8402-1701-8853-8048;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _xspeed ("x speed", Float ) = 0
        _yspeed ("y speed", Float ) = -0.2
        _ramp ("ramp", 2D) = "white" {}
        _xspeed_copy ("x speed_copy", Float ) = 0
        _yspeed_copy ("y speed_copy", Float ) = -0.2
        _noisetexture ("noise texture", 2D) = "white" {}
        _noise ("noise", Range(0, 1)) = 0.2950273
        _noisemask ("noise mask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _xspeed;
            uniform float _yspeed;
            uniform sampler2D _ramp; uniform float4 _ramp_ST;
            uniform float _xspeed_copy;
            uniform float _yspeed_copy;
            uniform sampler2D _noisetexture; uniform float4 _noisetexture_ST;
            uniform float _noise;
            uniform sampler2D _noisemask; uniform float4 _noisemask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_7124 = _Time;
                float2 node_9153 = (i.uv0+(node_7124.g*float2(_xspeed_copy,_yspeed_copy)));
                float4 _noisetexture_var = tex2D(_noisetexture,TRANSFORM_TEX(node_9153, _noisetexture));
                float4 _noisemask_var = tex2D(_noisemask,TRANSFORM_TEX(i.uv0, _noisemask));
                float4 node_2452 = _Time;
                float3 node_8980 = (lerp(float3(i.uv0,0.0),lerp(float3(i.uv0,0.0),_noisetexture_var.rgb,_noise),_noisemask_var.rgb)+float3((node_2452.g*float2(_xspeed,_yspeed)),0.0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8980, _MainTex));
                float4 _ramp_var = tex2D(_ramp,TRANSFORM_TEX(i.uv0, _ramp));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*3.0*(_MainTex_var.a*_ramp_var.r));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
