using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    
    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;
    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrianFace[] terrianFaces;

    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);
        if (meshFilters == null || meshFilters.Length ==0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrianFaces = new TerrianFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for(int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;
                meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Lightweight Render Pipeline/Lit"));
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }           

            terrianFaces[i] = new TerrianFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }
    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }
    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }
    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }
    void GenerateMesh()
    {
        foreach(TerrianFace face in terrianFaces)
        {
            face.ConstructMesh();
        }
    }
    void GenerateColors()
    {
        foreach (MeshFilter meshFilter in meshFilters)
        {
            meshFilter.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.planetColor;
        }
    }
}
