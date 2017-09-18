Shader "2D Dynamic Lights/Texturized/DefaultTexturized" {
    Properties 
	{
        _MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white" {}
    }
    
    Category {
	
       Lighting Off
       ZWrite Off
       Fog { Mode Off }
       Blend One One

       
       
	   BindChannels {
              Bind "Color", color
              Bind "Vertex", vertex
              Bind "TexCoord", texcoord
       }
       
	   SubShader 
	   {
            Pass 
			{
				
               SetTexture [_MainTex] 
			   {
			     Combine texture * primary
               }
            }
             
             Pass {
	            Color (1,0,1,1)
	            Cull Front
       		}
       
        } 
    }
}