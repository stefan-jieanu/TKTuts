#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 pr_matrix = mat4(1.0);
uniform mat4 ml_matrix = mat4(1.0);

void main(void)
{
    gl_Position = vec4(aPosition, 1.0) * ml_matrix * pr_matrix;
}