using UnityEngine;

[CreateAssetMenu(menuName = "MeshTypes/Cube mesh", fileName = "New cube mesh")]
public class CubeMesh : MeshType {

    public override void GenerateVertices(out Vector3[] vertices) {
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
    }

    public override void GenerateTriangles(out int[] triangles) {
        triangles = new int[CalculateTrianglesCount()];

        int[] pattern = new[] { 1, 1, 1, -2, 2, 1 };
        for (int i = 0, previousVertex = -1; i < triangles.Length; i++) {
            triangles[i] = previousVertex + pattern[(int)Mathf.Repeat(i, 6)];
            previousVertex = triangles[i];
        }
    }

    protected override int CalculateTrianglesCount() {
        return VerticesCount + 12;
    }

    protected override int CalculateVerticesCount() {
        return 24;
    }
}
