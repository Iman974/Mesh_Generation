using UnityEngine;

public class ScalingTransformation : MonoBehaviour, ITransformation {

    [SerializeField] Vector3 scale = Vector3.one;

    public Matrix4x4 Matrix {
        get {
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(scale.x, 0f, 0f, 0f),
                new Vector4(0f, scale.y, 0f, 0f),
                new Vector4(0f, 0f, scale.z, 0f),
                new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }
}
