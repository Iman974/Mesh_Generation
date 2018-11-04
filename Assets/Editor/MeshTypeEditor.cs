using UnityEditor;

[CustomEditor(typeof(MeshType), true)]
public class MeshTypeEditor : Editor {

    private MeshType meshType;

    private void OnEnable() {
        meshType = (MeshType)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (meshType == null) {
            return;
        }
        EditorGUILayout.LabelField("Vertices: " + meshType.VerticesCount);
    }
}