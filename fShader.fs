#version 330 core

in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

uniform vec3 light_position;
uniform vec3 view_position;
uniform float ambient_value;
uniform vec3 object_color;
uniform vec3 light_color;

void main()
{
    // Ambient
    float ambient_strength = ambient_value;
    vec3 ambient = ambient_strength * light_color;

    // Diffuse
    vec3 norm = normalize(Normal);
    vec3 light_dir = normalize(light_position - FragPos);
    float difference = max(dot(norm, light_dir), 0.0);
    vec3 diffuse = difference * light_color;

    // Specular
    float specular_strength = 0.5;
    vec3 view_dir = normalize(view_position - FragPos);
    vec3 reflect_dir = reflect(-light_dir, norm);
    float spec = pow(max(dot(view_dir, reflect_dir), 0.0), 32);
    vec3 specular = specular_strength * spec * light_color;

    vec3 result = (ambient + diffuse + specular) * object_color;
    FragColor = vec4(result, 1.0f);
}