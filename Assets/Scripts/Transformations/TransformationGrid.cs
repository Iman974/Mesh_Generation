using UnityEngine;
using TransformationList = System.Collections.Generic.List<ITransformation>;

public class TransformationGrid : MonoBehaviour {

    [SerializeField] MeshRenderer prefab = null;
    [SerializeField] Vector3Int gridSize = new Vector3Int(10, 10, 10);

    private Transform[] grid;
    private TransformationList transformations = new TransformationList();
    private Matrix4x4 fullTransformation;

    private void Start() {
        grid = new Transform[gridSize.x * gridSize.y * gridSize.z];

        for (int x = 0, i = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                for (int z = 0; z < gridSize.z; z++, i++) {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }
    }

    private Transform CreateGridPoint(float x, float y, float z) {
        MeshRenderer pointRenderer = Instantiate(prefab, transform);
        pointRenderer.transform.localPosition = GetWorldPosition(x, y ,z);
        pointRenderer.material.color = new Color(x / gridSize.x, y / gridSize.y, z / gridSize.z);
        return pointRenderer.transform;
    }

    private Vector3 GetWorldPosition(float x, float y, float z) {
        Vector3Int sizeMinusOne = gridSize - Vector3Int.one;
        return new Vector3(x - (sizeMinusOne.x * 0.5f), y - (sizeMinusOne.y * 0.5f), z - (sizeMinusOne.z * 0.5f));
    }

    private void Update() {
        UpdateMatrix();
        for (int x = 0, i = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                for (int z = 0; z < gridSize.z; z++, i++) {
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }

    private void UpdateMatrix() {
        GetComponents(transformations);
        if (transformations.Count == 0) {
            return;
        }

        fullTransformation = transformations[0].Matrix;
        for (int i = 1; i < transformations.Count; i++) {
            fullTransformation = transformations[i].Matrix * fullTransformation;
        }
    }

    private Vector3 TransformPoint(float x, float y, float z) {
        Vector3 worldPosition = GetWorldPosition(x, y, z);
        return fullTransformation.MultiplyPoint(worldPosition);
    }
}
