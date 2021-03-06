﻿Shader "Custom/blood"
{

    Properties
    {
    //No Properties
    }
    SubShader
    {
        //Render transparent
        Tags {"RenderType" = "Transparent" "Queue" = "Transparent" } //make transparent
     
        Pass{
            Zwrite Off
            Blend SrcAlpha OneminusSrcAlpha
            

            CGPROGRAM
            #include "UnityCG.cginc"
         
            #pragma vertex vert
            #pragma fragment frag
        
            //Input for vertex
            struct Input
            {
              float4 vertex: POSITION;
              float2 uv:TEXCOORD0;
              float4 tangent: TANGENT;
              float3 normal: NORMAL;
            };
            
            //Output for vertex
            struct Output{
                float4 pos: SV_POSITION;
                float2 uv: TEXCOORD0;
            };
            
            //Arithmetic Functions
            float3 fmod289(float3 x) {return x - floor(x * (1.0 /289.0)) * 289.0 ;}
            float2 fmod289(float2 x) {return x - floor(x * (1.0 /289.0)) * 289.0 ;}
            
            float3 permute(float3 x) {return fmod289(((x * 34.0) + 1.0) * x) ;}
            
            
            //Scale
            fixed2x2 scale(fixed2 _scale){
                fixed2x2 e = fixed2x2(_scale.x,0.0,
                0.0,_scale.y); 
                 return e;
            }
            
            //Snoise
            float snoise(float2 v){
            
                //Computational Arithmetic for triangular grid
                
                /*values: float4 C( ((3.0-sqrt(3.0))/6.0) , (0.5*(sqrt(3.0) -1.0)) ,
                (-1.0 + 2.0 *C.x) , (1.0/41.0) )  */
                const float4 C = float4(0.211324865405187, 0.366025403784439,
                 -0.577350269189626, 0.024390243902439);
                 
                //x0 - First corner
                float2 i = floor(v + dot(v, C.yy));
                float2 x0 = v -i + dot(i, C.xx);
                
                //x1 & x2 - Two other Corners
                float2 i1 = float2(0.0 , 0.0);
                i1 = (x0.x > x0.y)? float2(1.0,0.0):float2(0.0, 1.0);
                float2 x1 = x0.xy + C.xx - i1;
                float2 x2 = x0.xy + C.zz;
                
                //Permutations
                i = fmod289(i);
                float3 p = permute(permute(i.y + float3(0.0, i1.y, 1.0)) + i.x + float3(0.0, i1.x, 1.0));
                
                float3 m = max(0.5 - float3(dot(x0,x0), dot(x1,x1), dot(x2,x2)), 0.0);
                 m = m * m;
                 m = m * m;
                 
                //Gradients: Mapped onto a diamond shape 
                float3 x = 2.0 * frac(p * C.www) - 1.0;
                float3 h = (abs(x) - 0.5);
                float3 ox = floor(x + 0.5);
                float3 a0 = x - ox;
                
                //Normalize gradients by scaling m by  m *= inversesrt(a0*a0 +h*h)
                 m *= 1.79284291400159 - 0.85373472095314 * (a0*a0+h*h);
                 
                 //Compute noise calue at P
                 float3 g = float3(0.0,0.0,0.0);
                 g.x  = (a0.x  * x0.x  + h.x  * x0.y);
                 g.yz = a0.yz * float2(x1.x,x2.x) + h.yz * float2(x1.y,x2.y);
                 return 130.0 * dot(m, g);
            }

            #define OCTAVES 4
            
            //Ridged Multifractal
            float ridge(float h, float offset) {
                h = abs(h);     // creases
                h = offset - h; // inversion
                h = h * h;      
                return h;
            }
             
            
           float ridgedMF(float2 p) {
             float lacunarity = 2.0;
             float gain = 0.5;
             float offset = 0.9;

             float sum = 0.0;
             float freq = 1.0, amp = 0.5;
             float prev = 1.0;
             [unroll(100)] //not  sure if necessary
            for(int i=0; i < OCTAVES; i++) {
                 float n = ridge(snoise(p*freq), offset);
                 sum += n*amp;
                 sum += n*amp*prev;  
                 prev = n;
                 freq *= lacunarity;
                 amp *= gain;
             }
            return sum;
           }
           
           float3 hsb2rgb( in float3 c ){
                float3 rgb = clamp(abs(fmod(c.x*6.0 + float3( 0.0,4.0,2.0),6.0)-3.0)-1.0,0.0,1.0 );
                 rgb = rgb*rgb*(3.0-2.0*rgb);
                 return c.z * lerp(float3(1.0,1.0,1.0), rgb, c.y);
            }
            #define PI 3.14159265359
            
            //Takes in vertex input to translate to output
            Output vert(Input v){
                Output o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = v.uv;
                return o;    
            }
            
            float4 frag(Output i) : SV_TARGET{
                // Normalized pixel coordinates
                float2 uv = i.uv/1;
                float time = _Time.y/20;
                
                //Fixed Aspect Ratio
                float fx = 1/1;
                uv.x *= fx;
                
                float2 p = float2(0.5*fx,0.5) - uv;
                
                //R = radius & A = angle
                float r = length(p);
                float a = atan2(p.y,p.x);
                
                //Make a snoise
                float n = snoise(float2(uv.x*10.,uv.y*10.+_Time.y)*0.002) ;
                
                //RigedMF in RigedMF
                //RidgeCEPTION
                float e = ridgedMF(float2(0.5,0.5) *
                          (ridgedMF(float2(uv.x*0.8,uv.y*0.5-time*0.1)))+n
                         *ridgedMF(float2(uv.x*1.,uv.y+time*0.002)
                         *ridgedMF(float2(uv.x*0.5,uv.y*0.5+time*0.02))));
          
          
                //Colors
                 
                float3 col1 = float3(0.866, 0.086, 0.011);
                float3 col2 = float3(0.603, 0.086, 0.015);
             
                //Oscilate color1,2 in patterns
                float3 fin = lerp(col1,col2,e);
                     
                
                return float4(fin,1.0);
            
             }
             ENDCG
        }
    }
}
