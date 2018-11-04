using UnityEngine;

[CreateAssetMenu(menuName = "MeshTypes/Sphere mesh", fileName = "New sphere mesh")]
public class SphereMesh : MeshType {

    [SerializeField] private LevelOfDetail LOD = new LevelOfDetail();

    private int verticesCount;

    public override void GenerateVertices(out Vector3[] vertices) {
        verticesCount = (LOD.vertexPerCircumference * LOD.stageCount * 2) + LOD.vertexPerCircumference + 2;
        vertices = new Vector3[verticesCount];

        const float OffsetToCenter = 0.5f;
        float sideLength = 2 * Mathf.PI / LOD.vertexPerCircumference;
        Vector3 startPoint = new Vector3(OffsetToCenter, 0f, 0f);
        float roll = Mathf.PI * 0.5f / (LOD.stageCount + 1);
        float yaw = Mathf.Asin(sideLength * 0.5f) * 2f;
        int vertexIndex = 0;
        Debug.Log("vertices: " + verticesCount);

        for (int stageIndex = -LOD.stageCount; stageIndex < LOD.stageCount + 1; stageIndex++) {
            //startPoint *= Mathf.Lerp(1f, 0f, LOD.radiusChange.Evaluate(stageHeight * stageIndex));
            //startPoint.y = Mathf.Lerp(0f, 0.5f, LOD.stagesDivision.Evaluate(stageHeight * stageIndex));

            for (int i = 0; i < LOD.vertexPerCircumference ; i++) {
                vertices[vertexIndex] = Matrix3x3.CreateRotation(yaw * i, 0f, roll * stageIndex) * startPoint;
                vertexIndex++;
                if (i > 0) Debug.DrawLine(vertices[i - 1], vertices[i], Color.magenta, 150f);
            }
        }

        vertices[vertices.Length - 2] = Vector3.up * OffsetToCenter;
        vertices[vertices.Length - 1] = -Vector3.up * OffsetToCenter;
    }

    public override void GenerateTriangles(out int[] triangles) {
        triangles = new int[0];
    }

    [System.Serializable]
    private class LevelOfDetail {
        [Min(MinPolygonSideCount)] public int vertexPerCircumference = MinPolygonSideCount;
        [Min(1)] public int stageCount = 1;
        public AnimationCurve stagesDivision = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        //public AnimationCurve radiusChange = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private const int MinPolygonSideCount = 3;
    }
}
