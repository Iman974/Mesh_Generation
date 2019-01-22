using UnityEngine;

public class MovingGroundGenerator : MonoBehaviour {

    [SerializeField] private int gridSize = 3;
    [SerializeField] private GameObject solid = null;
    [SerializeField] private float moveSpeed = 0.5f;

    private Transform[] solidTransforms;

	private void Start () {
        solidTransforms = new Transform[gridSize * gridSize];

        int solidIndex = 0;
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                solidTransforms[solidIndex] = Instantiate(solid, new Vector3(x, 0f, y),Quaternion.identity, transform).transform;
                solidIndex++;
            }
        }
	}
	
	private void Update () {
        foreach (Transform transf in solidTransforms) {
            Vector3 pos = transf.position;
            float noise = Mathf.PerlinNoise(pos.x * Time.time * moveSpeed, pos.z * Time.time * moveSpeed);
            transf.position = new Vector3(pos.x, noise, pos.z);
        }
	}
}
