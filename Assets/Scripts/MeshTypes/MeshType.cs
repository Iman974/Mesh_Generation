using UnityEngine;

public abstract class MeshType : ScriptableObject {

    public abstract void GenerateVertices(out Vector3[] vertices);

    public abstract void GenerateTriangles(out int[] triangles);
}
