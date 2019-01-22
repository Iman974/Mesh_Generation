using UnityEngine;

public class Rotation : Transformation {

    [SerializeField] Vector3 rotation = Vector3.zero;

    public override Vector3 Apply(Vector3 point) {
        return Matrix3x3.CreateRotationZYX(rotation * Mathf.Deg2Rad) * point;
    }
}
