#ifdef GL_ES
#define pr highp
#else
#define pr
#define highp
#define mediump
#define lowp
#endif

uniform highp mat4 u_mvp;
uniform lowp vec4 u_color;

attribute mediump vec4 a_pos;
attribute mediump vec2 a_uv0;
attribute lowp vec4 a_color;

varying mediump vec2 v_uv0;
varying lowp vec4 v_color;

void main() 
{	
	v_uv0 = a_uv0;
	v_color = a_color * u_color;
	gl_Position = u_mvp * a_pos;
}
