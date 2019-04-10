#ifdef GL_ES
#define pr highp
#else
#define pr
#define highp
#define mediump
#define lowp
#endif

uniform mediump sampler2D texture0; 
uniform mediump sampler2D texture1;

varying mediump vec2 v_uv0;
varying lowp vec4 v_color;

void main() 
{
	gl_FragColor = texture2D(texture0, v_uv0.xy) * v_color;
}