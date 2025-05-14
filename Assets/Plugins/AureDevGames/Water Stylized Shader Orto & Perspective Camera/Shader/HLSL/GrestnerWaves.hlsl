float3 GerstnerWave(float3 position, float steepness, float wavelength, float speed, float direction, inout float3 tangent, inout float3 binormal, float globalOffset)
{
    // Normalizar la dirección para la propagación de la ola
    direction = direction * 2 - 1;
    float2 d = normalize(float2(cos(3.14159 * direction), sin(3.14159 * direction)));
    
    // Número de onda
    float k = 2 * 3.14159 / wavelength;
    
    // Calcular la fase de la ola, incluyendo el desplazamiento global
    float f = k * (dot(d, position.xz) - speed * _Time.y + globalOffset);
    
    // Amplitud de la ola
    float a = steepness / k;

    // Ajustar el tangente y el binormal para las normales
    tangent += float3(
        -d.x * d.x * (steepness * sin(f)),
        d.x * (steepness * cos(f)),
        -d.x * d.y * (steepness * sin(f))
    );

    binormal += float3(
        -d.x * d.y * (steepness * sin(f)),
        d.y * (steepness * cos(f)),
        -d.y * d.y * (steepness * sin(f))
    );

    // Retornar el desplazamiento de la ola en el plano XZ y la altura vertical
    return float3(
        d.x * (a * cos(f)), // Desplazamiento en X
        a * sin(f), // Desplazamiento en Y (altura)
        d.y * (a * cos(f)) // Desplazamiento en Z
    );
}

void GerstnerWaves_float(float3 position, float steepness, float wavelength, float speed, float4 directions, float tileSize, out float3 Offset, out float3 normal)
{
    // Calcular el desplazamiento global para las olas en base a la posición absoluta
    float globalOffsetX = floor(position.x / tileSize) * tileSize;
    float globalOffsetZ = floor(position.z / tileSize) * tileSize;

    // Envolver la posición X y Z usando fmod para asegurar el tileado, manteniendo el Y sin cambios
    float3 tiledPosition = float3(
        fmod(position.x, tileSize), // Envolver eje X
        position.y, // Y sin cambios
        fmod(position.z, tileSize) // Envolver eje Z
    );

    // Inicializar el desplazamiento y los vectores de tangente/binormal
    Offset = float3(0, 0, 0);
    float3 tangent = float3(1, 0, 0);
    float3 binormal = float3(0, 0, 1);

    // Aplicar las olas Gerstner a las direcciones, con el desplazamiento global en X y Z
    Offset += GerstnerWave(tiledPosition, steepness, wavelength, speed, directions.x, tangent, binormal, globalOffsetX + globalOffsetZ);
    Offset += GerstnerWave(tiledPosition, steepness, wavelength, speed, directions.y, tangent, binormal, globalOffsetX + globalOffsetZ);
    Offset += GerstnerWave(tiledPosition, steepness, wavelength, speed, directions.z, tangent, binormal, globalOffsetX + globalOffsetZ);
    Offset += GerstnerWave(tiledPosition, steepness, wavelength, speed, directions.w, tangent, binormal, globalOffsetX + globalOffsetZ);

    // Normalizar el producto cruzado de binormal y tangente para obtener la normal
    normal = normalize(cross(binormal, tangent));
}
