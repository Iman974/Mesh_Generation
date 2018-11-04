using UnityEngine;

[CreateAssetMenu(menuName = "MeshTypes/Cube mesh", fileName = "New cube mesh")]
public class CubeMesh : MeshType {

    public override int CalculateVerticesCount() {
        return 24;
    }

    public override void GenerateVertices(out Vector3[] vertices) {
        vertices = new Vector3[] {
            // Front face
            new Vector3(0, 0, 0), // 1, 16, 24
            new Vector3(0, 1, 0), // 2, 9, 23
            new Vector3(1, 1, 0), // 3, 12, 18
            new Vector3(1, 0, 0), // 4, 13, 17
            // Back face
            new Vector3(1, 0, 1), // 5, 14, 20
            new Vector3(1, 1, 1), // 6, 11, 19
            new Vector3(0, 1, 1), // 7, 10, 22
            new Vector3(0, 0, 1), // 8, 15, 21
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
        triangles = new int[VerticesCount + 12];
        triangles[0] = -1;

        int[] sequence = new int[] { 1, 1, 1, -2, 2, 1 };
        for (int i = 0; i < triangles.Length; i++) {
            int previousVertex = triangles[i != 0 ? i - 1 : 0];
            triangles[i] = previousVertex + sequence[(int)Mathf.Repeat(i, 6)];
        }
    }
}
