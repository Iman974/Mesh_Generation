using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GridMeshGenerator : MonoBehaviour {

    [SerializeField] private GridSettings settings = new GridSettings();
    [SerializeField] private Vector2 moveSpeed = new Vector2(0.75f, 0.75f);
    [SerializeField] private Vector2 noiseScale = new Vector2(0.5f, 0.5f);

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
            float x = vertices[i].x;
            float z = vertices[i].z;
            float noise = Mathf.PerlinNoise((x * noiseScale.x) + (Time.time * moveSpeed.x), (z * noiseScale.y) + (Time.time * moveSpeed.y));
            vertices[i] = new Vector3(x, noise, z);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private void UpdateMesh() {
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
        [Min(0)] public Vector2Int gridSize = new Vector2Int(50, 50);
        public Vector2 scale = new Vector2(4f, 4f);
    }
}
