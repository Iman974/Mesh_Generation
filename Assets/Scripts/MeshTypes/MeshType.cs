using UnityEngine;

public abstract class MeshType : ScriptableObject {

    public int VerticesCount { get; private set; }
    public int TrianglesCount { get; protected set; }

    //public virtual void UpdateVerticesAndTrisCount() { }

    public void OnEnable() {
        VerticesCount = CalculateVerticesCount();
        TrianglesCount = CalculateTrianglesCount();
    }

    protected abstract int CalculateVerticesCount();

    protected abstract int CalculateTrianglesCount();

    public abstract void GenerateVertices(out Vector3[] vertices);

    public abstract void GenerateTriangles(out int[] triangles);
}
