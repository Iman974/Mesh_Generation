using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    [SerializeField] private MeshType meshType;

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
        meshType.GenerateVertices(out vertices);
        meshType.GenerateTriangles(out triangles);
    }

    private void UpdateMesh() {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnGUI() {
        if (GUI.Button(new Rect(10f, 10f, 80f, 30f), "Regenerate")) {
            GenerateVerticesAndTris();
            UpdateMesh();
        }
    }
}
