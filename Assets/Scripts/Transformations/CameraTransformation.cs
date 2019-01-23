using UnityEngine;

public class CameraTransformation : MonoBehaviour, ITransformation {

    [SerializeField] private float focalLength = 1f;

    public Matrix4x4 Matrix {
        get {
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(focalLength, 0f, 0f, 0f),
                new Vector4(0f, focalLength, 0f, 0f),
                new Vector4(0f, 0f, 0f, 1f),
                new Vector4(0f, 0f, 0f, 0f));
            return matrix;
        }
    }
}
