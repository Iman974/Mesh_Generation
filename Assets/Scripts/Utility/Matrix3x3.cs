﻿using UnityEngine;

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
        float c1_r2, float c2_r0, float c2_r1, float c2_r2) {
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

    /// <summary>
    /// Creates a rotation matrix that rotates a vector around Z, then X and finally Y axis.
    /// </summary>
    public static Matrix3x3 CreateRotationZXY(float yaw, float pitch, float roll) {
        return CreateRotationY(yaw) * CreateRotationX(pitch) * CreateRotationZ(roll);
    }

    public static Matrix3x3 CreateRotationZXY(Vector3 radians) {
        return CreateRotationY(radians.y) * CreateRotationX(radians.x) * CreateRotationZ(radians.z);
    }

    public static Matrix3x3 CreateRotationZYX(float roll, float yaw, float pitch) {
        Vector3 cos = new Vector3(Mathf.Cos(pitch), Mathf.Cos(yaw), Mathf.Cos(roll));
        Vector3 sin = new Vector3(Mathf.Sin(pitch), Mathf.Sin(yaw), Mathf.Sin(roll));

        return new Matrix3x3(cos.y * cos.z, (cos.x * sin.z) + (sin.x * sin.y * cos.z),
            (sin.x * sin.z) - (cos.x * sin.y * cos.z), -sin.z * cos.y,
            (cos.x * cos.z) - (sin.x * sin.y * sin.z), (sin.x * cos.z) + (cos.x * sin.y * sin.z),
            sin.y, -cos.y * sin.x, cos.x * cos.y);
    }

    public static Matrix3x3 CreateRotationZYX(Vector3 radians) {
        Vector3 cos = new Vector3(Mathf.Cos(radians.x), Mathf.Cos(radians.y), Mathf.Cos(radians.z));
        Vector3 sin = new Vector3(Mathf.Sin(radians.x), Mathf.Sin(radians.y), Mathf.Sin(radians.z));

        return new Matrix3x3(cos.y * cos.z, (cos.x * sin.z) + (sin.x * sin.y * cos.z),
            (sin.x * sin.z) - (cos.x * sin.y * cos.z), -sin.z * cos.y,
            (cos.x * cos.z) - (sin.x * sin.y * sin.z), (sin.x * cos.z) + (cos.x * sin.y * sin.z),
            sin.y, -cos.y * sin.x, cos.x * cos.y);
    }

    public static Matrix3x3 CreateRotationYZ(float yaw, float roll) {
        return CreateRotationY(yaw) * CreateRotationZ(roll);
    }

    public static Matrix3x3 CreateRotationYX(float yaw, float pitch) {
        return CreateRotationY(yaw) * CreateRotationX(pitch);
    }

    public static Matrix3x3 CreateRotationXZ(float pitch, float roll) {
        return CreateRotationX(pitch) * CreateRotationZ(roll);
    }

    public static Matrix3x3 CreateRotationX(float radians) {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Matrix3x3(1f, 0f, 0f, 0f, cos, -sin, 0f, sin, cos);
    }

    public static Matrix3x3 CreateRotationY(float radians) {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Matrix3x3(cos, 0f, -sin, 0f, 1f, 0f, sin, 0f, cos);
    }

    public static Matrix3x3 CreateRotationZ(float radians) {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Matrix3x3(cos, sin, 0f, -sin, cos, 0f, 0f, 0f, 1f);
    }

    public static Matrix3x3 operator *(Matrix3x3 mat1, Matrix3x3 mat2) {
        return new Matrix3x3() {
            c0_r0 = (mat1.c0_r0 * mat2.c0_r0) + (mat1.c1_r0 * mat2.c0_r1) + (mat1.c2_r0 * mat2.c0_r2),
            c0_r1 = (mat1.c0_r1 * mat2.c0_r0) + (mat1.c1_r1 * mat2.c0_r1) + (mat1.c2_r1 * mat2.c0_r2),
            c0_r2 = (mat1.c0_r2 * mat2.c0_r0) + (mat1.c1_r2 * mat2.c0_r1) + (mat1.c2_r2 * mat2.c0_r2),
            c1_r0 = (mat1.c0_r0 * mat2.c1_r0) + (mat1.c1_r0 * mat2.c1_r1) + (mat1.c2_r0 * mat2.c1_r2),
            c1_r1 = (mat1.c0_r1 * mat2.c1_r0) + (mat1.c1_r1 * mat2.c1_r1) + (mat1.c2_r1 * mat2.c1_r2),
            c1_r2 = (mat1.c0_r2 * mat2.c1_r0) + (mat1.c1_r2 * mat2.c1_r1) + (mat1.c2_r2 * mat2.c1_r2),
            c2_r0 = (mat1.c0_r0 * mat2.c2_r0) + (mat1.c1_r0 * mat2.c2_r1) + (mat1.c2_r0 * mat2.c2_r2),
            c2_r1 = (mat1.c0_r1 * mat2.c2_r0) + (mat1.c1_r1 * mat2.c2_r1) + (mat1.c2_r1 * mat2.c2_r2),
            c2_r2 = (mat1.c0_r2 * mat2.c2_r0) + (mat1.c1_r2 * mat2.c2_r1) + (mat1.c2_r2 * mat2.c2_r2)
        };
    }

    public static Vector3 operator *(Matrix3x3 matrix, Vector3 vector) {
        return new Vector3 {
            x = (matrix.c0_r0 * vector.x) + (matrix.c1_r0 * vector.y) + (matrix.c2_r0 * vector.z),
            y = (matrix.c0_r1 * vector.x) + (matrix.c1_r1 * vector.y) + (matrix.c2_r1 * vector.z),
            z = (matrix.c0_r2 * vector.x) + (matrix.c1_r2 * vector.y) + (matrix.c2_r2 * vector.z)
        };
    }
}
