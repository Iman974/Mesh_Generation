using UnityEngine;

public struct Matrix3x3 {

    private float c0_r0;
    private float c0_r1;
    private float c0_r2;
    private float c1_r0;
    private float c1_r1;
    private float c1_r2;
    private float c2_r0;
    private float c2_r1;
    private float c2_r2;

    private Matrix3x3(float c0_r0, float c0_r1, float c0_r2, float c1_r0, float c1_r1,
        float c1_r2, float c2_r0, float c2_r1, float c2_r2 ) {
        this.c0_r0 = c0_r0;
        this.c0_r1 = c0_r1;
        this.c0_r2 = c0_r2;
        this.c1_r0 = c1_r0;
        this.c1_r1 = c1_r1;
        this.c1_r2 = c1_r2;
        this.c2_r0 = c2_r0;
        this.c2_r1 = c2_r1;
        this.c2_r2 = c2_r2;
    }

    public static Matrix3x3 CreateRotationX(float angle) {
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);

        throw new System.NotImplementedException();
    }

    public static Matrix3x3 CreateRotationY(float angle) { throw new System.NotImplementedException(); }

    public static Matrix3x3 CreateRotationZ(float angle) { throw new System.NotImplementedException(); }

    public static Vector3 operator*(Matrix3x3 matrix, Vector3 vector) {
        float x = (matrix.c0_r0 * vector.x) + (matrix.c1_r0 * vector.y);
        float y = (matrix.c0_r1 * vector.x) + (matrix.c1_r1 * vector.y);

        vector.x = x;
        vector.y = y;

        throw new System.NotImplementedException();
    }
}
