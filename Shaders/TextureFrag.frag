#version 330

struct MaterialProperties 
{
	vec3 AmbientReflectivity;
	vec3 DiffuseReflectivity;
	vec3 SpecularReflectivity;
	float Shininess;
	float Opacity;
	mat2 Spin;
};
uniform MaterialProperties uMaterial;

struct PositionalLight
{
	vec4 LightPosition;
	vec3 LightColour;
	float LightStrength;
};
uniform int uPositionalLightCount;
uniform PositionalLight uPositionalLights[5];

uniform vec4 uEyePosition;

uniform sampler2D uTextureSampler;

in vec2 oTexCoords;

in vec4 oColour, oNormal, oSurfacePosition;

out vec4 FragColour;

void main()
{
	vec2 offset = vec2(0.5f, 0.5f);
	vec4 TextureColour = texture(uTextureSampler, (offset - oTexCoords)*uMaterial.Spin);
	if(TextureColour.x >= 0.8f &&TextureColour.y >= 0.8f &&TextureColour.z >= 0.8f)
	{
		discard;
	}	
	vec4 FinalFragColour = oColour;
	float specularFactor;
	for(int i = 0; i < uPositionalLightCount; i++)
	{
		PositionalLight curLight = uPositionalLights[i];

		vec4 lightDir = normalize(curLight.LightPosition - oSurfacePosition);
		float diffuseFactor = max(dot(oNormal, lightDir), 0)*curLight.LightStrength;

		vec4 eyeDirection = normalize(uEyePosition - oSurfacePosition);
		vec4 reflectedVector = reflect(-lightDir, oNormal);
		specularFactor += pow(max(dot( reflectedVector, eyeDirection), 0.0), uMaterial.Shininess);

		FinalFragColour += vec4( (curLight.LightColour * diffuseFactor ) * uMaterial.DiffuseReflectivity, 1);
	}

	specularFactor *= uMaterial.SpecularReflectivity;
	FinalFragColour += vec4(specularFactor,specularFactor,specularFactor,0);
	
	FinalFragColour += TextureColour;

	FragColour = vec4(
		FinalFragColour.x, 
		FinalFragColour.y, 
		FinalFragColour.z, 
		uMaterial.Opacity
	);
}