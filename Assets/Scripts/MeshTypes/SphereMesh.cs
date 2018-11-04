using UnityEngine;

[CreateAssetMenu(menuName = "MeshTypes/Sphere mesh", fileName = "New sphere mesh")]
public class SphereMesh : MeshType {

    [SerializeField] private LevelOfDetail LOD;

    public override int CalculateVerticesCount() {
        return (LOD.vertexPerStage * LOD.stageCount * 2) + LOD.vertexPerStage + 2;
    }

    //public override void UpdateVerticesAndTrisCount() {
    //    base.UpdateVerticesAndTrisCount();
    //    // TODO
    //}

    public override void GenerateVertices(out Vector3[] vertices) {
        vertices = new Vector3[VerticesCount];

        const float OffsetToCenter = 0.5f;
        float sideLength = 2 * Mathf.PI / LOD.vertexPerStage;
        Vector3 startPoint = new Vector3(OffsetToCenter, 0f, 0f);
        float roll = Mathf.PI * 0.5f / (LOD.stageCount + 1);
        float yaw = Mathf.Asin(sideLength * 0.5f) * 2f;
        int vertexIndex = 0;

        for (int stageIndex = -LOD.stageCount; stageIndex < LOD.stageCount + 1; stageIndex++) {
            for (int i = 0; i < LOD.vertexPerStage ; i++) {
                vertices[vertexIndex] = Matrix3x3.CreateRotationYZ(-yaw * i, roll * stageIndex) * startPoint;
                //if (vertexIndex % LOD.vertexPerStage != 0)
                //    Debug.DrawLine(vertices[vertexIndex - 1], vertices[vertexIndex], Color.magenta, 10f);
                vertexIndex++;
            }
        }

        vertices[vertices.Length - 2] = -Vector3.up * OffsetToCenter;
        vertices[vertices.Length - 1] = Vector3.up * OffsetToCenter;
    }

    public override void GenerateTriangles(out int[] triangles) {
        triangles = new int[CalculateTrianglesCount()];

        int vertexIndex = 0;
        int trisIndex = 0;
        for (int stageIndex = 0; stageIndex < LOD.stageCount * 2; stageIndex++) {
            for (int i = 0; i < LOD.vertexPerStage - 1; i++) {
                triangles[trisIndex] = vertexIndex;
                triangles[trisIndex + 1] = vertexIndex + LOD.vertexPerStage;
                triangles[trisIndex + 4] = vertexIndex;
                vertexIndex++;
                triangles[trisIndex + 2] = vertexIndex + LOD.vertexPerStage;
                triangles[trisIndex + 3] = vertexIndex;
                triangles[trisIndex + 5] = vertexIndex + LOD.vertexPerStage;
                trisIndex += 6;
            }
            triangles[trisIndex] = vertexIndex;
            triangles[trisIndex + 1] = vertexIndex + LOD.vertexPerStage;
            triangles[trisIndex + 4] = vertexIndex;
            vertexIndex++;
            triangles[trisIndex + 2] = vertexIndex;
            triangles[trisIndex + 3] = vertexIndex - LOD.vertexPerStage;
            triangles[trisIndex + 5] = vertexIndex;
            trisIndex += 6;
        }

        for (int i = 0; i < LOD.vertexPerStage - 1; i++) {
            triangles[trisIndex] = vertexIndex;
            triangles[trisIndex + 1] = VerticesCount - 1;
            vertexIndex++;
            triangles[trisIndex + 2] = vertexIndex;
            trisIndex += 3;
        }
        triangles[trisIndex] = vertexIndex;
        triangles[trisIndex + 1] = VerticesCount - 1;
        vertexIndex++;
        triangles[trisIndex + 2] = vertexIndex - LOD.vertexPerStage;
        trisIndex += 3;

        for (int i = 0; i < LOD.vertexPerStage - 1; i++) {
            triangles[trisIndex] = VerticesCount - 2;
            triangles[trisIndex + 1] = i;
            triangles[trisIndex + 2] = i + 1;
            trisIndex += 3;
        }
        triangles[trisIndex] = VerticesCount - 2;
        triangles[trisIndex + 1] = LOD.vertexPerStage - 1;
        triangles[trisIndex + 2] = 0;
    }

    private int CalculateTrianglesCount() {
        const int verticesPerTriangle = 3;
        const int trianglesPerQuad = 2;

        return ((LOD.stageCount * 4 * LOD.vertexPerStage) + (2 * LOD.vertexPerStage)) *
            verticesPerTriangle * trianglesPerQuad;
    }

    [System.Serializable]
    private struct LevelOfDetail {
        [Min(MinPolygonSideCount)] public int vertexPerStage;
        [Min(1)] public int stageCount;
        //public AnimationCurve stagesDivision = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private const int MinPolygonSideCount = 3;
    }
}
