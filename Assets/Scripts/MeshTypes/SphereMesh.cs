using UnityEngine;

[CreateAssetMenu(menuName = "MeshTypes/Sphere mesh", fileName = "New sphere mesh")]
public class SphereMesh : MeshType {

    [SerializeField] private LevelOfDetail LOD = new LevelOfDetail();

    private int verticesCount;

    public override void GenerateVertices(out Vector3[] vertices) {
        verticesCount = (LOD.vertexPerCircumference * LOD.stagesCount) + LOD.vertexPerCircumference + 2;
        vertices = new Vector3[verticesCount];

        float gap = 1f / LOD.vertexPerCircumference;
        Vector3 startPoint = new Vector3(0.5f, 0f, 0f);
        float angle = Mathf.Asin(gap * 0.5f) * 2f;
        for (int i = 0; i < LOD.vertexPerCircumference; i++) {
            vertices[i] = Matrix2x2.CreateRotation(i * angle) * startPoint;
            if (i > 0) Debug.DrawLine(vertices[i - 1], vertices[i], Color.magenta, 50f);
        }
    }

    public override void GenerateTriangles(out int[] triangles) {
        triangles = new int[0];
    }

    [System.Serializable]
    private class LevelOfDetail {
        [Min(MinPolygonSideCount)] public int vertexPerCircumference = MinPolygonSideCount;
        [Min(0)] public int stagesCount = 0;

        private const int MinPolygonSideCount = 3;
    }
}
