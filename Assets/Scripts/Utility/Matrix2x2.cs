using UnityEngine;

public struct Matrix2x2 {

    private float c0_r0;
    private float c0_r1;
    private float c1_r0;
    private float c1_r1;

    private Matrix2x2(float c0_r0, float c0_r1, float c1_r0, float c1_r1) {
        this.c0_r0 = c0_r0;
        this.c0_r1 = c0_r1;
        this.c1_r0 = c1_r0;
        this.c1_r1 = c1_r1;
    }

    public static Matrix2x2 CreateRotation(float angle) {
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);

        return new Matrix2x2(cosAngle, sinAngle, -sinAngle, cosAngle);
    }

    public static Vector2 operator*(Matrix2x2 matrix, Vector2 vector) {
        vector.x = (matrix.c0_r0 * vector.x) + (matrix.c1_r0 * vector.y);
        vector.y = (matrix.c0_r1 * vector.x) + (matrix.c1_r1 * vector.y);

        return vector;
    }
}
