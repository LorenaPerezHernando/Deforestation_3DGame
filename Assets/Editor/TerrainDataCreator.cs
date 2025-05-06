using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainDataCreator
{
    [MenuItem("Tools/Terrain/Generate New TerrainData")]
    public static void GenerateNewTerrainData()
    {
        Terrain terrain = Selection.activeGameObject?.GetComponent<Terrain>();

        if (terrain == null)
        {
            Debug.LogError("Selecciona un objeto con componente Terrain en la jerarquía.");
            return;
        }

        // Crear nuevo TerrainData
        TerrainData newData = new TerrainData();
        newData.heightmapResolution = 513;
        newData.size = new Vector3(500, 100, 500); // Ancho, altura, largo

        // Generar alturas básicas (una montañita)
        int resolution = newData.heightmapResolution;
        float[,] heights = new float[resolution, resolution];

        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float centerX = resolution / 2f;
                float centerY = resolution / 2f;
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                float maxDistance = resolution / 2f;

                float height = Mathf.Clamp01(1f - distance / maxDistance) * 0.1f; // Montaña suavizada
                heights[y, x] = height;
            }
        }

        newData.SetHeights(0, 0, heights);

        // Guardar como asset
        string path = "Assets/NuevoTerrainDataConAlturas.asset";
        AssetDatabase.CreateAsset(newData, path);
        AssetDatabase.SaveAssets();

        // Asignar al terrain
        terrain.terrainData = newData;

        Debug.Log("Nuevo TerrainData con alturas creado y asignado.");
    }
}
