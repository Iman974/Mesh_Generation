using UnityEditor;

[CustomEditor(typeof(MeshType), true)]
public class MeshTypeEditor : Editor {

    private MeshType meshType;
    private SerializedProperty LOD;

    private void OnEnable() {
        meshType = target as MeshType;

        if (meshType == null) {
            return;
        }
        LOD = serializedObject.FindProperty("LOD");
    }

    public override void OnInspectorGUI() {
        if (meshType == null) {
            return;
        }

        if (LOD != null) {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(LOD, true);
            if (EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
                meshType.UpdateVerticesAndTrisCount();
            }
        }
        EditorGUILayout.LabelField("Vertices: " + meshType.VerticesCount + ", Triangles: " + meshType.TrianglesCount);
    }
}