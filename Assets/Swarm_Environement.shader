// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:1,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1,x:35217,y:32638,varname:node_1,prsc:2|diff-8703-OUT,spec-5370-OUT,gloss-5801-OUT,emission-4727-OUT,lwrap-8775-OUT,clip-887-R,voffset-9233-OUT;n:type:ShaderForge.SFN_NormalVector,id:139,x:32708,y:33574,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:151,x:32784,y:32629,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:_Diffuse,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:1932,x:30006,y:32269,varname:node_1932,prsc:2;n:type:ShaderForge.SFN_Sin,id:947,x:30685,y:32387,varname:node_947,prsc:2|IN-5548-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5286,x:29123,y:32971,ptovrint:False,ptlb:Inflate Speed,ptin:_InflateSpeed,varname:node_5286,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Multiply,id:5548,x:30504,y:32418,varname:node_5548,prsc:2|A-7056-OUT,B-5286-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:4801,x:30503,y:33218,ptovrint:False,ptlb:Inflate Patern,ptin:_InflatePatern,varname:node_4801,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:9653,x:32446,y:33433,ptovrint:False,ptlb:Inflate Value,ptin:_InflateValue,varname:node_9653,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:8703,x:33015,y:32533,varname:node_8703,prsc:2|A-3127-RGB,B-151-RGB;n:type:ShaderForge.SFN_Color,id:3127,x:32784,y:32461,ptovrint:False,ptlb:Diffuse Color,ptin:_DiffuseColor,varname:node_3127,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3823529,c2:0.3823529,c3:0.3823529,c4:1;n:type:ShaderForge.SFN_FragmentPosition,id:8201,x:29427,y:33195,varname:node_8201,prsc:2;n:type:ShaderForge.SFN_Append,id:3595,x:30165,y:32579,varname:node_3595,prsc:2|A-9958-OUT,B-9103-OUT;n:type:ShaderForge.SFN_Append,id:2601,x:30058,y:33217,varname:node_2601,prsc:2|A-6948-OUT,B-9507-OUT;n:type:ShaderForge.SFN_Append,id:6132,x:30130,y:33924,varname:node_6132,prsc:2|A-6618-OUT,B-3769-OUT;n:type:ShaderForge.SFN_Tex2d,id:2081,x:30870,y:32599,varname:node_2081,prsc:2,ntxv:0,isnm:False|UVIN-9076-UVOUT,TEX-4801-TEX;n:type:ShaderForge.SFN_Tex2d,id:9520,x:30739,y:33218,varname:node_9520,prsc:2,ntxv:0,isnm:False|UVIN-5-UVOUT,TEX-4801-TEX;n:type:ShaderForge.SFN_Tex2d,id:5562,x:30796,y:33916,varname:node_5562,prsc:2,ntxv:0,isnm:False|UVIN-4054-UVOUT,TEX-4801-TEX;n:type:ShaderForge.SFN_NormalVector,id:5326,x:30269,y:34186,prsc:2,pt:False;n:type:ShaderForge.SFN_Abs,id:8675,x:30529,y:34181,varname:node_8675,prsc:2|IN-5326-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7362,x:30776,y:34171,varname:node_7362,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-8675-OUT;n:type:ShaderForge.SFN_Lerp,id:149,x:31525,y:32999,varname:node_149,prsc:2|A-9733-OUT,B-3806-OUT,T-7362-R;n:type:ShaderForge.SFN_Lerp,id:7947,x:31835,y:33074,varname:node_7947,prsc:2|A-149-OUT,B-3818-OUT,T-7362-G;n:type:ShaderForge.SFN_Panner,id:9076,x:30450,y:32592,varname:node_9076,prsc:2,spu:0.13,spv:0.17|UVIN-3595-OUT;n:type:ShaderForge.SFN_Panner,id:5,x:30269,y:33207,varname:node_5,prsc:2,spu:0.11,spv:-0.28|UVIN-2601-OUT;n:type:ShaderForge.SFN_Panner,id:4054,x:30350,y:33924,varname:node_4054,prsc:2,spu:-0.07,spv:-0.15|UVIN-6132-OUT;n:type:ShaderForge.SFN_Multiply,id:9233,x:33072,y:33368,varname:node_9233,prsc:2|A-7947-OUT,B-7058-OUT,C-139-OUT,D-7612-RGB;n:type:ShaderForge.SFN_RemapRange,id:7499,x:30870,y:32387,cmnt:maxValue,varname:node_7499,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:0.84|IN-947-OUT;n:type:ShaderForge.SFN_Multiply,id:9733,x:31143,y:32462,varname:node_9733,prsc:2|A-7499-OUT,B-2081-RGB;n:type:ShaderForge.SFN_Time,id:492,x:29946,y:32907,varname:node_492,prsc:2;n:type:ShaderForge.SFN_Sin,id:9800,x:30564,y:32960,varname:node_9800,prsc:2|IN-7365-OUT;n:type:ShaderForge.SFN_Multiply,id:7365,x:30395,y:32960,varname:node_7365,prsc:2|A-3578-OUT,B-9803-OUT;n:type:ShaderForge.SFN_RemapRange,id:1082,x:30748,y:32960,varname:node_1082,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1.37|IN-9800-OUT;n:type:ShaderForge.SFN_Multiply,id:3806,x:31042,y:33037,varname:node_3806,prsc:2|A-1082-OUT,B-9520-RGB;n:type:ShaderForge.SFN_Multiply,id:9803,x:29427,y:33412,varname:node_9803,prsc:2|A-5286-OUT,B-8710-OUT;n:type:ShaderForge.SFN_Vector1,id:8710,x:29090,y:33386,varname:node_8710,prsc:2,v1:0.35;n:type:ShaderForge.SFN_Time,id:4256,x:30017,y:33580,varname:node_4256,prsc:2;n:type:ShaderForge.SFN_Sin,id:4505,x:30612,y:33645,varname:node_4505,prsc:2|IN-6946-OUT;n:type:ShaderForge.SFN_Multiply,id:6946,x:30443,y:33645,varname:node_6946,prsc:2|A-3667-OUT,B-2220-OUT;n:type:ShaderForge.SFN_RemapRange,id:8039,x:30796,y:33645,varname:node_8039,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-4505-OUT;n:type:ShaderForge.SFN_Multiply,id:3818,x:31076,y:33754,varname:node_3818,prsc:2|A-8039-OUT,B-5562-RGB;n:type:ShaderForge.SFN_Multiply,id:2220,x:29404,y:33803,varname:node_2220,prsc:2|A-5286-OUT,B-3896-OUT;n:type:ShaderForge.SFN_Vector1,id:3896,x:29088,y:33767,varname:node_3896,prsc:2,v1:0.83;n:type:ShaderForge.SFN_Multiply,id:7058,x:32708,y:33418,varname:node_7058,prsc:2|A-9653-OUT,B-7928-OUT;n:type:ShaderForge.SFN_Vector1,id:7928,x:32446,y:33535,varname:node_7928,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Add,id:7056,x:30269,y:32342,varname:node_7056,prsc:2|A-1932-T,B-7245-OUT;n:type:ShaderForge.SFN_Vector1,id:7245,x:30006,y:32434,cmnt:offset,varname:node_7245,prsc:2,v1:1.65;n:type:ShaderForge.SFN_Add,id:3667,x:30238,y:33645,varname:node_3667,prsc:2|A-4256-T,B-5422-OUT;n:type:ShaderForge.SFN_Vector1,id:5422,x:30028,y:33779,varname:node_5422,prsc:2,v1:0.17;n:type:ShaderForge.SFN_Tex2d,id:7612,x:32708,y:33170,ptovrint:False,ptlb:Inflate mask ,ptin:_Inflatemask,varname:node_7612,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5620,x:32938,y:32997,varname:node_5620,prsc:2|A-7947-OUT,B-7612-RGB;n:type:ShaderForge.SFN_Add,id:4727,x:33561,y:32755,varname:node_4727,prsc:2|A-117-RGB,B-8160-OUT,C-9884-OUT;n:type:ShaderForge.SFN_Multiply,id:8160,x:33292,y:32864,varname:node_8160,prsc:2|A-117-RGB,B-5620-OUT;n:type:ShaderForge.SFN_Add,id:3578,x:30160,y:32960,varname:node_3578,prsc:2|A-492-T,B-4135-OUT;n:type:ShaderForge.SFN_Vector1,id:4135,x:29946,y:33047,varname:node_4135,prsc:2,v1:5.39;n:type:ShaderForge.SFN_Vector1,id:1602,x:29645,y:32631,cmnt:Tiling,varname:node_1602,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:9958,x:29890,y:32533,varname:node_9958,prsc:2|A-1602-OUT,B-8201-X;n:type:ShaderForge.SFN_Multiply,id:9103,x:29890,y:32655,varname:node_9103,prsc:2|A-1602-OUT,B-8201-Z;n:type:ShaderForge.SFN_Vector1,id:8823,x:29703,y:33243,cmnt:Tiling,varname:node_8823,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6948,x:29858,y:33161,varname:node_6948,prsc:2|A-8823-OUT,B-8201-Y;n:type:ShaderForge.SFN_Multiply,id:9507,x:29858,y:33283,varname:node_9507,prsc:2|A-8823-OUT,B-8201-Z;n:type:ShaderForge.SFN_Vector1,id:1466,x:29597,y:33910,cmnt:Tiling,varname:node_1466,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Multiply,id:6618,x:29843,y:33841,varname:node_6618,prsc:2|A-1466-OUT,B-8201-X;n:type:ShaderForge.SFN_Multiply,id:3769,x:29843,y:33963,varname:node_3769,prsc:2|A-1466-OUT,B-8201-Y;n:type:ShaderForge.SFN_Tex2d,id:887,x:34865,y:32800,ptovrint:False,ptlb:Opacity Mask,ptin:_OpacityMask,varname:node_887,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:4557,x:33559,y:32246,varname:node_4557,prsc:2|NRM-960-OUT,EXP-578-OUT;n:type:ShaderForge.SFN_NormalVector,id:960,x:33322,y:32135,prsc:2,pt:False;n:type:ShaderForge.SFN_Color,id:949,x:33544,y:32068,ptovrint:False,ptlb:Fresnel Color,ptin:_FresnelColor,varname:node_949,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:9884,x:33740,y:32197,varname:node_9884,prsc:2|A-949-RGB,B-4557-OUT;n:type:ShaderForge.SFN_ValueProperty,id:578,x:33235,y:32336,ptovrint:False,ptlb:Fresnel Value,ptin:_FresnelValue,varname:node_578,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:8068,x:34570,y:32215,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_8068,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:117,x:32784,y:32824,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_117,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Vector1,id:8775,x:35019,y:32800,varname:node_8775,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:714,x:34413,y:32398,ptovrint:False,ptlb:Specular Value,ptin:_SpecularValue,varname:node_714,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:2998,x:34503,y:32536,ptovrint:False,ptlb:Roughness,ptin:_Roughness,varname:node_2998,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:5370,x:34789,y:32308,varname:node_5370,prsc:2|A-8068-RGB,B-714-OUT;n:type:ShaderForge.SFN_OneMinus,id:5801,x:34833,y:32557,varname:node_5801,prsc:2|IN-2998-OUT;proporder:151-3127-8068-714-2998-887-117-5286-9653-4801-7612-949-578;pass:END;sub:END;*/

Shader "Custom/Swarm_Environement" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _DiffuseColor ("Diffuse Color", Color) = (0.3823529,0.3823529,0.3823529,1)
        _Specular ("Specular", 2D) = "white" {}
        _SpecularValue ("Specular Value", Range(0, 1)) = 0
        _Roughness ("Roughness", Range(0, 1)) = 0
        _OpacityMask ("Opacity Mask", 2D) = "white" {}
        _Emission ("Emission", 2D) = "black" {}
        _InflateSpeed ("Inflate Speed", Float ) = 5
        _InflateValue ("Inflate Value", Float ) = 3
        _InflatePatern ("Inflate Patern", 2D) = "black" {}
        _Inflatemask ("Inflate mask ", 2D) = "black" {}
        _FresnelColor ("Fresnel Color", Color) = (0.5,0.5,0.5,1)
        _FresnelValue ("Fresnel Value", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _InflateSpeed;
            uniform sampler2D _InflatePatern; uniform float4 _InflatePatern_ST;
            uniform float _InflateValue;
            uniform float4 _DiffuseColor;
            uniform sampler2D _Inflatemask; uniform float4 _Inflatemask_ST;
            uniform sampler2D _OpacityMask; uniform float4 _OpacityMask_ST;
            uniform float4 _FresnelColor;
            uniform float _FresnelValue;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float _SpecularValue;
            uniform float _Roughness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_1932 = _Time + _TimeEditor;
                float4 node_4221 = _Time + _TimeEditor;
                float node_1602 = 1.0; // Tiling
                float2 node_9076 = (float2((node_1602*mul(_Object2World, v.vertex).r),(node_1602*mul(_Object2World, v.vertex).b))+node_4221.g*float2(0.13,0.17));
                float4 node_2081 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_9076, _InflatePatern),0.0,0));
                float4 node_492 = _Time + _TimeEditor;
                float node_8823 = 0.5; // Tiling
                float2 node_5 = (float2((node_8823*mul(_Object2World, v.vertex).g),(node_8823*mul(_Object2World, v.vertex).b))+node_4221.g*float2(0.11,-0.28));
                float4 node_9520 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_5, _InflatePatern),0.0,0));
                float2 node_7362 = abs(v.normal).rb;
                float4 node_4256 = _Time + _TimeEditor;
                float node_1466 = 0.8; // Tiling
                float2 node_4054 = (float2((node_1466*mul(_Object2World, v.vertex).r),(node_1466*mul(_Object2World, v.vertex).g))+node_4221.g*float2(-0.07,-0.15));
                float4 node_5562 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_4054, _InflatePatern),0.0,0));
                float3 node_7947 = lerp(lerp(((sin(((node_1932.g+1.65)*_InflateSpeed))*0.42+0.42)*node_2081.rgb),((sin(((node_492.g+5.39)*(_InflateSpeed*0.35)))*0.685+0.685)*node_9520.rgb),node_7362.r),((sin(((node_4256.g+0.17)*(_InflateSpeed*0.83)))*0.5+0.5)*node_5562.rgb),node_7362.g);
                float4 _Inflatemask_var = tex2Dlod(_Inflatemask,float4(TRANSFORM_TEX(o.uv0, _Inflatemask),0.0,0));
                v.vertex.xyz += (node_7947*(_InflateValue*0.1)*v.normal*_Inflatemask_var.rgb);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _OpacityMask_var = tex2D(_OpacityMask,TRANSFORM_TEX(i.uv0, _OpacityMask));
                clip(_OpacityMask_var.r - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 1.0 - (1.0 - _Roughness); // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float3 specularColor = (_Specular_var.rgb*_SpecularValue);
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_8775 = 1.0;
                float3 w = float3(node_8775,node_8775,node_8775)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = forwardLight * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = (_DiffuseColor.rgb*_Diffuse_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float4 node_1932 = _Time + _TimeEditor;
                float4 node_4221 = _Time + _TimeEditor;
                float node_1602 = 1.0; // Tiling
                float2 node_9076 = (float2((node_1602*i.posWorld.r),(node_1602*i.posWorld.b))+node_4221.g*float2(0.13,0.17));
                float4 node_2081 = tex2D(_InflatePatern,TRANSFORM_TEX(node_9076, _InflatePatern));
                float4 node_492 = _Time + _TimeEditor;
                float node_8823 = 0.5; // Tiling
                float2 node_5 = (float2((node_8823*i.posWorld.g),(node_8823*i.posWorld.b))+node_4221.g*float2(0.11,-0.28));
                float4 node_9520 = tex2D(_InflatePatern,TRANSFORM_TEX(node_5, _InflatePatern));
                float2 node_7362 = abs(i.normalDir).rb;
                float4 node_4256 = _Time + _TimeEditor;
                float node_1466 = 0.8; // Tiling
                float2 node_4054 = (float2((node_1466*i.posWorld.r),(node_1466*i.posWorld.g))+node_4221.g*float2(-0.07,-0.15));
                float4 node_5562 = tex2D(_InflatePatern,TRANSFORM_TEX(node_4054, _InflatePatern));
                float3 node_7947 = lerp(lerp(((sin(((node_1932.g+1.65)*_InflateSpeed))*0.42+0.42)*node_2081.rgb),((sin(((node_492.g+5.39)*(_InflateSpeed*0.35)))*0.685+0.685)*node_9520.rgb),node_7362.r),((sin(((node_4256.g+0.17)*(_InflateSpeed*0.83)))*0.5+0.5)*node_5562.rgb),node_7362.g);
                float4 _Inflatemask_var = tex2D(_Inflatemask,TRANSFORM_TEX(i.uv0, _Inflatemask));
                float3 emissive = (_Emission_var.rgb+(_Emission_var.rgb*(node_7947*_Inflatemask_var.rgb))+(_FresnelColor.rgb*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_FresnelValue)));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _InflateSpeed;
            uniform sampler2D _InflatePatern; uniform float4 _InflatePatern_ST;
            uniform float _InflateValue;
            uniform float4 _DiffuseColor;
            uniform sampler2D _Inflatemask; uniform float4 _Inflatemask_ST;
            uniform sampler2D _OpacityMask; uniform float4 _OpacityMask_ST;
            uniform float4 _FresnelColor;
            uniform float _FresnelValue;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float _SpecularValue;
            uniform float _Roughness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_1932 = _Time + _TimeEditor;
                float4 node_5145 = _Time + _TimeEditor;
                float node_1602 = 1.0; // Tiling
                float2 node_9076 = (float2((node_1602*mul(_Object2World, v.vertex).r),(node_1602*mul(_Object2World, v.vertex).b))+node_5145.g*float2(0.13,0.17));
                float4 node_2081 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_9076, _InflatePatern),0.0,0));
                float4 node_492 = _Time + _TimeEditor;
                float node_8823 = 0.5; // Tiling
                float2 node_5 = (float2((node_8823*mul(_Object2World, v.vertex).g),(node_8823*mul(_Object2World, v.vertex).b))+node_5145.g*float2(0.11,-0.28));
                float4 node_9520 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_5, _InflatePatern),0.0,0));
                float2 node_7362 = abs(v.normal).rb;
                float4 node_4256 = _Time + _TimeEditor;
                float node_1466 = 0.8; // Tiling
                float2 node_4054 = (float2((node_1466*mul(_Object2World, v.vertex).r),(node_1466*mul(_Object2World, v.vertex).g))+node_5145.g*float2(-0.07,-0.15));
                float4 node_5562 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_4054, _InflatePatern),0.0,0));
                float3 node_7947 = lerp(lerp(((sin(((node_1932.g+1.65)*_InflateSpeed))*0.42+0.42)*node_2081.rgb),((sin(((node_492.g+5.39)*(_InflateSpeed*0.35)))*0.685+0.685)*node_9520.rgb),node_7362.r),((sin(((node_4256.g+0.17)*(_InflateSpeed*0.83)))*0.5+0.5)*node_5562.rgb),node_7362.g);
                float4 _Inflatemask_var = tex2Dlod(_Inflatemask,float4(TRANSFORM_TEX(o.uv0, _Inflatemask),0.0,0));
                v.vertex.xyz += (node_7947*(_InflateValue*0.1)*v.normal*_Inflatemask_var.rgb);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _OpacityMask_var = tex2D(_OpacityMask,TRANSFORM_TEX(i.uv0, _OpacityMask));
                clip(_OpacityMask_var.r - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 1.0 - (1.0 - _Roughness); // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float3 specularColor = (_Specular_var.rgb*_SpecularValue);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_8775 = 1.0;
                float3 w = float3(node_8775,node_8775,node_8775)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = forwardLight * attenColor;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = (_DiffuseColor.rgb*_Diffuse_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform float _InflateSpeed;
            uniform sampler2D _InflatePatern; uniform float4 _InflatePatern_ST;
            uniform float _InflateValue;
            uniform sampler2D _Inflatemask; uniform float4 _Inflatemask_ST;
            uniform sampler2D _OpacityMask; uniform float4 _OpacityMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
                float3 normalDir : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_1932 = _Time + _TimeEditor;
                float4 node_4225 = _Time + _TimeEditor;
                float node_1602 = 1.0; // Tiling
                float2 node_9076 = (float2((node_1602*mul(_Object2World, v.vertex).r),(node_1602*mul(_Object2World, v.vertex).b))+node_4225.g*float2(0.13,0.17));
                float4 node_2081 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_9076, _InflatePatern),0.0,0));
                float4 node_492 = _Time + _TimeEditor;
                float node_8823 = 0.5; // Tiling
                float2 node_5 = (float2((node_8823*mul(_Object2World, v.vertex).g),(node_8823*mul(_Object2World, v.vertex).b))+node_4225.g*float2(0.11,-0.28));
                float4 node_9520 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_5, _InflatePatern),0.0,0));
                float2 node_7362 = abs(v.normal).rb;
                float4 node_4256 = _Time + _TimeEditor;
                float node_1466 = 0.8; // Tiling
                float2 node_4054 = (float2((node_1466*mul(_Object2World, v.vertex).r),(node_1466*mul(_Object2World, v.vertex).g))+node_4225.g*float2(-0.07,-0.15));
                float4 node_5562 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_4054, _InflatePatern),0.0,0));
                float3 node_7947 = lerp(lerp(((sin(((node_1932.g+1.65)*_InflateSpeed))*0.42+0.42)*node_2081.rgb),((sin(((node_492.g+5.39)*(_InflateSpeed*0.35)))*0.685+0.685)*node_9520.rgb),node_7362.r),((sin(((node_4256.g+0.17)*(_InflateSpeed*0.83)))*0.5+0.5)*node_5562.rgb),node_7362.g);
                float4 _Inflatemask_var = tex2Dlod(_Inflatemask,float4(TRANSFORM_TEX(o.uv0, _Inflatemask),0.0,0));
                v.vertex.xyz += (node_7947*(_InflateValue*0.1)*v.normal*_Inflatemask_var.rgb);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _OpacityMask_var = tex2D(_OpacityMask,TRANSFORM_TEX(i.uv0, _OpacityMask));
                clip(_OpacityMask_var.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _InflateSpeed;
            uniform sampler2D _InflatePatern; uniform float4 _InflatePatern_ST;
            uniform float _InflateValue;
            uniform float4 _DiffuseColor;
            uniform sampler2D _Inflatemask; uniform float4 _Inflatemask_ST;
            uniform float4 _FresnelColor;
            uniform float _FresnelValue;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float _SpecularValue;
            uniform float _Roughness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_1932 = _Time + _TimeEditor;
                float4 node_8886 = _Time + _TimeEditor;
                float node_1602 = 1.0; // Tiling
                float2 node_9076 = (float2((node_1602*mul(_Object2World, v.vertex).r),(node_1602*mul(_Object2World, v.vertex).b))+node_8886.g*float2(0.13,0.17));
                float4 node_2081 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_9076, _InflatePatern),0.0,0));
                float4 node_492 = _Time + _TimeEditor;
                float node_8823 = 0.5; // Tiling
                float2 node_5 = (float2((node_8823*mul(_Object2World, v.vertex).g),(node_8823*mul(_Object2World, v.vertex).b))+node_8886.g*float2(0.11,-0.28));
                float4 node_9520 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_5, _InflatePatern),0.0,0));
                float2 node_7362 = abs(v.normal).rb;
                float4 node_4256 = _Time + _TimeEditor;
                float node_1466 = 0.8; // Tiling
                float2 node_4054 = (float2((node_1466*mul(_Object2World, v.vertex).r),(node_1466*mul(_Object2World, v.vertex).g))+node_8886.g*float2(-0.07,-0.15));
                float4 node_5562 = tex2Dlod(_InflatePatern,float4(TRANSFORM_TEX(node_4054, _InflatePatern),0.0,0));
                float3 node_7947 = lerp(lerp(((sin(((node_1932.g+1.65)*_InflateSpeed))*0.42+0.42)*node_2081.rgb),((sin(((node_492.g+5.39)*(_InflateSpeed*0.35)))*0.685+0.685)*node_9520.rgb),node_7362.r),((sin(((node_4256.g+0.17)*(_InflateSpeed*0.83)))*0.5+0.5)*node_5562.rgb),node_7362.g);
                float4 _Inflatemask_var = tex2Dlod(_Inflatemask,float4(TRANSFORM_TEX(o.uv0, _Inflatemask),0.0,0));
                v.vertex.xyz += (node_7947*(_InflateValue*0.1)*v.normal*_Inflatemask_var.rgb);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float4 node_1932 = _Time + _TimeEditor;
                float4 node_8886 = _Time + _TimeEditor;
                float node_1602 = 1.0; // Tiling
                float2 node_9076 = (float2((node_1602*i.posWorld.r),(node_1602*i.posWorld.b))+node_8886.g*float2(0.13,0.17));
                float4 node_2081 = tex2D(_InflatePatern,TRANSFORM_TEX(node_9076, _InflatePatern));
                float4 node_492 = _Time + _TimeEditor;
                float node_8823 = 0.5; // Tiling
                float2 node_5 = (float2((node_8823*i.posWorld.g),(node_8823*i.posWorld.b))+node_8886.g*float2(0.11,-0.28));
                float4 node_9520 = tex2D(_InflatePatern,TRANSFORM_TEX(node_5, _InflatePatern));
                float2 node_7362 = abs(i.normalDir).rb;
                float4 node_4256 = _Time + _TimeEditor;
                float node_1466 = 0.8; // Tiling
                float2 node_4054 = (float2((node_1466*i.posWorld.r),(node_1466*i.posWorld.g))+node_8886.g*float2(-0.07,-0.15));
                float4 node_5562 = tex2D(_InflatePatern,TRANSFORM_TEX(node_4054, _InflatePatern));
                float3 node_7947 = lerp(lerp(((sin(((node_1932.g+1.65)*_InflateSpeed))*0.42+0.42)*node_2081.rgb),((sin(((node_492.g+5.39)*(_InflateSpeed*0.35)))*0.685+0.685)*node_9520.rgb),node_7362.r),((sin(((node_4256.g+0.17)*(_InflateSpeed*0.83)))*0.5+0.5)*node_5562.rgb),node_7362.g);
                float4 _Inflatemask_var = tex2D(_Inflatemask,TRANSFORM_TEX(i.uv0, _Inflatemask));
                o.Emission = (_Emission_var.rgb+(_Emission_var.rgb*(node_7947*_Inflatemask_var.rgb))+(_FresnelColor.rgb*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_FresnelValue)));
                
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffColor = (_DiffuseColor.rgb*_Diffuse_var.rgb);
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float3 specColor = (_Specular_var.rgb*_SpecularValue);
                float roughness = (1.0 - _Roughness);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
