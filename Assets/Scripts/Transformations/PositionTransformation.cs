using UnityEngine;

public class PositionTransformation : MonoBehaviour, ITransformation {

    [SerializeField] Vector3 position = Vector3.zero;

    public Matrix4x4 Matrix {
        get {
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(1f, 0f, 0f, 0f),
                new Vector4(0f, 1f, 0f, 0f),
                new Vector4(0f, 0f, 1f, 0f),
                new Vector4(position.x, position.y, position.z, 1f));
            return matrix;
        }
    }
}
