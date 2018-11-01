using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GridMeshGenerator : MonoBehaviour {

    [SerializeField] private GridSettings settings = new GridSettings();
    [SerializeField] private float moveSpeed = 0.5f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void Awake() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Start () {
        GenerateVerticesAndTris();
        UpdateMesh();
    }

    private void GenerateVerticesAndTris() {
        Vector2Int gridSize = settings.gridSize;
        Vector2 cellSize = settings.scale / gridSize;

        vertices = new Vector3[(gridSize.x + 1) * (gridSize.y + 1)];
        triangles = new int[gridSize.x * gridSize.y * 6];

        int vertexIndex = 0;
        for (int x = 0; x < gridSize.x + 1; x++) {
            for (int y = 0; y < gridSize.y + 1; y++) {
                vertices[vertexIndex] = new Vector3(x * cellSize.x, 0f, y * cellSize.y);

                vertexIndex++;
            }
        }

        vertexIndex = 0;
        int trisIndex = 0;
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                triangles[trisIndex] = vertexIndex;
                triangles[trisIndex + 1] = vertexIndex + 1;
                triangles[trisIndex + 2] = vertexIndex + gridSize.y + 2;
                triangles[trisIndex + 3] = vertexIndex;
                triangles[trisIndex + 4] = vertexIndex + gridSize.y + 2;
                triangles[trisIndex + 5] = vertexIndex + gridSize.y + 1;

                vertexIndex++;
                trisIndex += 6;
            }
            vertexIndex++;
        }
    }

    private void Update() {
        for (int i = 0; i < vertices.Length; i++) {
            float noise = Mathf.PerlinNoise(vertices[i].x * Time.time * moveSpeed, vertices[i].z * Time.time * moveSpeed);
            vertices[i] = new Vector3(vertices[i].x, noise, vertices[i].z);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnGUI() {
        if (GUI.Button(new Rect(10f, 10f, 80f, 30f), "Regenerate")) {
            Start();
        }
    }

    [System.Serializable]
    private class GridSettings {
        public Vector2Int gridSize = new Vector2Int(3, 3);
        public Vector2 scale = new Vector2(1f, 1f);
    }
}
