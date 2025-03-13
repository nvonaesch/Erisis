Shader "Custom/DepthMask"
{
    SubShader
    {
        Tags { "Queue" = "Geometry-1" } // Dessiné juste avant les objets opaques
        ColorMask 0  // Désactive l’écriture des couleurs
        ZWrite On    // Active l’écriture dans le Depth Buffer

        Pass {}
    }
}
