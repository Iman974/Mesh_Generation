using UnityEngine;

public class RotationTransformation : MonoBehaviour, ITransformation {

    [SerializeField] Vector3 rotation = Vector3.zero;

    private Vector3 radians;

    public Matrix4x4 Matrix {
        get {
            float cosX = Mathf.Cos(radians.x);
            float cosY = Mathf.Cos(radians.y);
            float cosZ = Mathf.Cos(radians.z);
            float sinX = Mathf.Sin(radians.x);
            float sinY = Mathf.Sin(radians.y);
            float sinZ = Mathf.Sin(radians.z);

            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(cosY * cosZ, (cosX * sinZ) + (sinX * sinY * cosZ), (sinX * sinZ) - (cosX * sinY * cosZ), 0f),
                new Vector4(-sinZ * cosY, (cosX * cosZ) - (sinX * sinY * sinZ), (sinX * cosZ) + (cosX * sinY * sinZ), 0f),
                new Vector4(sinY, -cosY * sinX, cosX * cosY, 0f),
                new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }

    private void OnValidate() {
        radians = rotation * Mathf.Deg2Rad;
    }
}
