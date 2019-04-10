#ifdef GL_ES
#define pr highp
#else
#define pr
#define highp
#define mediump
#define lowp
#endif

attribute highp   vec3 a_pos;
attribute mediump vec2 a_uv0;

uniform highp mat4 u_mvp;
uniform mediump vec2 u_direction;

varying mediump vec4 v_uv[4]; 

void main () 
{	
	gl_Position = u_mvp * vec4(a_pos, 1.0);
	v_uv[0].xy = a_uv0;
	v_uv[0].zw = a_uv0 + u_direction;
	v_uv[1].xy = a_uv0 + u_direction * 2.0;
	v_uv[1].zw = a_uv0 + u_direction * 3.0;
	v_uv[2].xy = a_uv0 - u_direction;
	v_uv[2].zw = a_uv0 - u_direction * 2.0;
	v_uv[3].xy = a_uv0 - u_direction * 3.0;	
}

