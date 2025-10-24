using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public Material defaultMaterial;

    Mesh CreateTriangle()
    {
        Mesh mesh = new Mesh();

        // Define vertices with a specific winding order (counterclockwise)
        Vector3[] vertices = new Vector3[3];
        vertices[0] = new Vector3(-0.5f, 0, 0); // Bottom-left corner
        vertices[1] = new Vector3(0, 1, 0); // Top center
        vertices[2] = new Vector3(0.5f, 0, 0); // Bottom-right corner

        // Define triangles with vertices in counterclockwise winding order
        int[] triangles = new int[3] { 0, 1, 2 };

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals for proper shading
        mesh.RecalculateNormals();

        return mesh;
    }
    
    Mesh CreateQuad()
    {
        Mesh mesh = new Mesh();
        
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0,0,0);
        vertices[1] = new Vector3(0,1,0);
        vertices[2] = new Vector3(1,1,0);
        vertices[3] = new Vector3(1,0,0);

        int[] triangles = new int[6] {0,1,2,0,2,3};

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        return mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Add a MeshFilter component and assign the created mesh
        gameObject.AddComponent<MeshFilter>().mesh = CreateTriangle();

        // Add a MeshRenderer component
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the default material to the MeshRenderer
        renderer.material = defaultMaterial;
    }
}
