using UnityEngine;
using TransformationList = System.Collections.Generic.List<Transformation>;

public class TransformationGrid : MonoBehaviour {

    [SerializeField] MeshRenderer prefab = null;
    [SerializeField] Vector3Int gridSize = new Vector3Int(10, 10, 10);

    private Transform[] grid;
    private TransformationList transformations = new TransformationList();

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
        GetComponents(transformations);
        for (int x = 0, i = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                for (int z = 0; z < gridSize.z; z++, i++) {
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }

    private Vector3 TransformPoint(float x, float y, float z) {
        Vector3 worldPosition = GetWorldPosition(x, y, z);
        for (int i = 0; i < transformations.Count; i++) {
            worldPosition = transformations[i].Apply(worldPosition);
        }
        return worldPosition;
    }
}
