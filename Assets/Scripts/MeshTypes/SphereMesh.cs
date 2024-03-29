﻿using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;

[CreateAssetMenu(menuName = "MeshTypes/Sphere mesh", fileName = "New sphere mesh")]
public class SphereMesh : MeshType {

    [SerializeField] private LevelOfDetail LOD = new LevelOfDetail();

    public override void GenerateVertices(out Vector3[] vertices) {
        vertices = new Vector3[VerticesCount];

        const float DistanceToCenter = 0.5f;
        float sideLength = 2 * Mathf.PI / LOD.vertexPerStage;
        float roll = Mathf.PI * 0.5f / (LOD.stageCount + 1);
        float yaw = Mathf.Asin(sideLength * 0.5f) * 2f;

        vertices[vertices.Length - 2] = -Vector3.up * DistanceToCenter;
        vertices[vertices.Length - 1] = Vector3.up * DistanceToCenter;
        Vector3 startPoint = Matrix3x3.CreateRotationZ(roll) * vertices[vertices.Length - 2];

        for (int z = 0, vertIndex = 0; z < LOD.vertexPerStage; z++) {
            for (int y = 0; y < (LOD.stageCount * 2) + 1; y++) {
                vertices[vertIndex] = Matrix3x3.CreateRotationYZ(-yaw * z, roll * y) * startPoint;
                //Debug.DrawLine(vertices[vert - 1], vertices[vert], Color.magenta, 150f);
                vertIndex++;
            }
        }

    }

    public override void GenerateTriangles(out int[] triangles) {
        triangles = new int[CalculateTrianglesCount()];
        Stopwatch sw = new Stopwatch();
        sw.Start();
        int stageCount = LOD.stageCount * 2;
        int vertCount = VerticesCount - 2;

        //int[] pattern = new[] { 0, 1, stageCount + 2, 0, stageCount + 2, stageCount + 1 };
        //for (int i = 0, vertIndex = 0; i < triangles.Length; i++) {

        //    triangles[i] = (int)Mathf.Repeat(vertIndex + pattern[(int)Mathf.Repeat(i, 6)], vertCount);
        //    vertIndex += ((i + 1) % 6) == 0 ? 1 : 0;
        //}

        for (int i = 0, vertIndex = 0, trisIndex = 0; i < LOD.vertexPerStage; i++) {
            triangles[trisIndex] = VerticesCount - 2;
            triangles[trisIndex + 1] = vertIndex;
            triangles[trisIndex + 2] = (int)Mathf.Repeat(vertIndex + stageCount + 1, vertCount);
            trisIndex += 3;

            for (int y = 0; y < stageCount; y++) {
                triangles[trisIndex] = vertIndex;
                triangles[trisIndex + 1] = vertIndex + 1;
                triangles[trisIndex + 2] = (int)Mathf.Repeat(vertIndex + stageCount + 2, vertCount);
                triangles[trisIndex + 3] = vertIndex;
                triangles[trisIndex + 4] = (int)Mathf.Repeat(vertIndex + stageCount + 2, vertCount);
                triangles[trisIndex + 5] = (int)Mathf.Repeat(vertIndex + stageCount + 1, vertCount);
                vertIndex++;
                trisIndex += 6;
            }

            triangles[trisIndex] = vertIndex;
            triangles[trisIndex + 1] = VerticesCount - 1;
            triangles[trisIndex + 2] = (int)Mathf.Repeat(vertIndex + stageCount + 1, vertCount);
            trisIndex += 3;
            vertIndex++;
        }
        sw.Stop();
        Debug.Log(sw.Elapsed);
    }

    protected override int CalculateTrianglesCount() {
        const int vertsPerTriangle = 3;
        const int trisPerQuad = 2;

        return ((LOD.stageCount * 2) + 2) * LOD.vertexPerStage * vertsPerTriangle * trisPerQuad;
    }

    protected override int CalculateVerticesCount() {
        return (((LOD.stageCount * 2) + 1) * LOD.vertexPerStage) + 2;
    }

    [System.Serializable]
    private struct LevelOfDetail {
        [Min(MinPolygonSideCount)] public int vertexPerStage;
        [Min(1)] public int stageCount;
        //public AnimationCurve stagesDivision = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private const int MinPolygonSideCount = 3;

        public LevelOfDetail(int vertexPerStage, int stageCount) {
            this.vertexPerStage = vertexPerStage;
            this.stageCount = stageCount;
        }
    }
}
