using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GridMeshGenerator : MonoBehaviour {

    [SerializeField] private GridSettings settings = new GridSettings(1f, 3);

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void Start () {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateVerticesAndTris();
        UpdateMesh();
    }

    private void GenerateVerticesAndTris() {
        int gridSize = settings.gridSize;
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];

        int vertexIndex = 0;
        float cellSize = settings.cellSize;
        for (int x = 0; x < gridSize + 1; x++) {
            for (int y = 0; y < gridSize + 1; y++) {
                vertices[vertexIndex] = new Vector3(x * cellSize, 0f, y * cellSize);

                vertexIndex++;
            }
        }

        vertexIndex = 0;
        int trisIndex = 0;
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                triangles[trisIndex] = vertexIndex;
                triangles[trisIndex + 1] = vertexIndex + 1;
                triangles[trisIndex + 2] = vertexIndex + gridSize + 2;
                triangles[trisIndex + 3] = vertexIndex;
                triangles[trisIndex + 4] = vertexIndex + gridSize + 2;
                triangles[trisIndex + 5] = vertexIndex + gridSize + 1;

                vertexIndex++;
                trisIndex += 6;
            }
            vertexIndex++;
        }
    }

    private void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    [System.Serializable]
    private struct GridSettings {
        public float cellSize;
        public int gridSize;

        public GridSettings(float cellSize, int gridSize) {
            this.cellSize = cellSize;
            this.gridSize = gridSize;
        }
    }
}
