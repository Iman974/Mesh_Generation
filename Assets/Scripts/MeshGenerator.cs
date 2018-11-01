using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private const int VerticesCount = 24;

	private void Start () {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateVerticesAndTris();
        UpdateMesh();
    }

    private void GenerateVerticesAndTris() {
        vertices = new Vector3[] {
            // Front face
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0),
            // Back face
            new Vector3(1, 0, 1),
            new Vector3(1, 1, 1),
            new Vector3(0, 1, 1),
            new Vector3(0, 0, 1),
            // Top face
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(1, 1, 0),
            // Bottom face
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 0),
            // Right face
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 1, 1),
            new Vector3(1, 0, 1),
            // Left face
            new Vector3(0, 0, 1),
            new Vector3(0, 1, 1),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 0)
        };

        triangles = new int[VerticesCount + 12];
        triangles[0] = -1;

        int[] sequence = new int[] { 1, 1, 1, -2, 2, 1 };
        for (int i = 0; i < triangles.Length; i++) {
            int previousVertex = triangles[i != 0 ? i - 1 : 0];
            triangles[i] = previousVertex + sequence[(int)Mathf.Repeat(i, 6)];
        }
    }

    private void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
