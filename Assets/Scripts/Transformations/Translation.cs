using UnityEngine;

public class Translation : Transformation {

    [SerializeField] Vector3 translation = Vector3.zero;

    public override Vector3 Apply(Vector3 point) {
        return point + translation;
    }
}
