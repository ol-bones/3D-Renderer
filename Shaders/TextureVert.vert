#version 330

struct MaterialProperties 
{
	vec3 AmbientReflectivity;
	vec3 DiffuseReflectivity;
	vec3 SpecularReflectivity;
	float Shininess;
};
uniform MaterialProperties uMaterial;

struct DirectionalLight
{
	vec3 LightDirection;
	vec3 LightColour;
	float LightStrength;
};

uniform int uDirectionalLightCount;
uniform DirectionalLight uDirectionalLights[5];

struct PositionalLight
{
	vec4 LightPosition;
	vec3 LightColour;
	float LightStrength;
};
uniform int uPositionalLightCount;
uniform PositionalLight uPositionalLights[5];

struct AmbientLight
{
	vec3 LightColour;
	float LightStrength;
};
uniform int uAmbientLightCount;
uniform AmbientLight uAmbientLights[5];

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

layout(location = 0) in vec3 vPosition; 
layout(location = 1) in vec3 vNormal;
layout(location = 2) in vec2 vTexCoords;

out vec4 oColour;

out vec2 oTexCoords;
out vec4 oNormal, oSurfacePosition;

void main() 
{ 
	vec3 pos = vec3(vPosition.x, vPosition.y, vPosition.z);
	gl_Position = vec4(pos, 1.0) * uModel * uView * uProjection; 
	vec3 inverseTransposeNormal = normalize(vNormal * mat3(transpose(inverse(uModel * uView))));

	oSurfacePosition = vec4(pos, 1) * uModel * uView;
	oNormal = vec4(normalize(vNormal * mat3(transpose(inverse(uModel * uView)))), 1);

	vec3 FinalVertexColour = vec3(0,0,0);

	for(int i = 0; i < uAmbientLightCount; i++)
	{
		AmbientLight curLight = uAmbientLights[i];
		FinalVertexColour += (curLight.LightColour * curLight.LightStrength) * uMaterial.AmbientReflectivity;
	}

	for(int i = 0; i < uDirectionalLightCount; i++)
	{
		DirectionalLight curLight = uDirectionalLights[i];
		vec3 lightDir = normalize(-curLight.LightDirection * mat3(uView));
		vec3 lightStr = vec3(max(dot(inverseTransposeNormal, lightDir), 0));

		FinalVertexColour += ((lightStr*curLight.LightColour)*curLight.LightStrength);
	}
	
	oTexCoords = vTexCoords;
	
	oColour = vec4(FinalVertexColour, 1);
}