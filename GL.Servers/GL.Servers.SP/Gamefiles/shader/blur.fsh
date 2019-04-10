#ifdef GL_ES
#define pr highp
#else
#define pr
#define highp
#define mediump
#define lowp
#endif

uniform highp mat4 u_mvp;
uniform lowp sampler2D texture0;

varying mediump vec4 v_uv[4];

void main()
{
	highp vec4 color;
	color  = texture2D(texture0, v_uv[0].xy) * 0.383;
	color += texture2D(texture0, v_uv[0].zw) * 0.242;
	color += texture2D(texture0, v_uv[1].xy) * 0.061;
	color += texture2D(texture0, v_uv[1].zw) * 0.006;
	color += texture2D(texture0, v_uv[2].xy) * 0.242;
	color += texture2D(texture0, v_uv[2].zw) * 0.061;
	color += texture2D(texture0, v_uv[3].xy) * 0.006;
	
	gl_FragColor = color;
}


