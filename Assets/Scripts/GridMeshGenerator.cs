using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GridMeshGenerator : MonoBehaviour {

    [SerializeField] [Min(0)] private Vector2Int gridSize = new Vector2Int(50, 50);
    [SerializeField] private Vector2 scale = new Vector2(4f, 4f);

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
        Vector2 cellSize = scale / gridSize;

        vertices = new Vector3[(gridSize.x + 1) * (gridSize.y + 1)];
        const int vertsPerTriangle = 3;
        const int trisPerQuad = 2;
        triangles = new int[gridSize.x * gridSize.y * vertsPerTriangle * trisPerQuad];

        int vertIndex = 0;
        for (int x = 0; x < gridSize.x + 1; x++) {
            for (int y = 0; y < gridSize.y + 1; y++) {
                vertices[vertIndex] = new Vector3(x * cellSize.x, y * cellSize.y, 0f);

                vertIndex++;
            }
        }

        vertIndex = 0;
        int trisIndex = 0;
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                triangles[trisIndex] = vertIndex;
                triangles[trisIndex + 1] = vertIndex + 1;
                triangles[trisIndex + 2] = vertIndex + gridSize.y + 2;
                triangles[trisIndex + 3] = vertIndex;
                triangles[trisIndex + 4] = vertIndex + gridSize.y + 2;
                triangles[trisIndex + 5] = vertIndex + gridSize.y + 1;

                vertIndex++;
                trisIndex += 6;
            }
            vertIndex++;
        }
    }

    private void Update() {
        //for (int i = 0; i < vertices.Length; i++) {
        //    float x = vertices[i].x;
        //    float z = vertices[i].z;
        //    vertices[i] = new Vector3(x, 0f, z);
        //}

        //mesh.vertices = vertices;
        //mesh.RecalculateNormals();
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
}
