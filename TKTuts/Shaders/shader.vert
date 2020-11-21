#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 model = mat4(1.0);
uniform mat4 view = mat4(1.0);
uniform mat4 projection = mat4(1.0);

out vec4 pos;

void main(void)
{
    
   /*gl_Position = vec4(aPosition, 1.0) * model * view * projection;*/
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
    pos = vec4(aPosition, 1.0);
}