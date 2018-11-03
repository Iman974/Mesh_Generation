using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinAttribute))]
public class MinDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        MinAttribute minAttribute = (MinAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.Float) {
            float fieldValue = EditorGUI.FloatField(position, label, property.floatValue);
            if (fieldValue < minAttribute.minimum) {
                fieldValue = minAttribute.minimum;
            }

            property.floatValue = fieldValue;
        } else if (property.propertyType == SerializedPropertyType.Integer) {
            int fieldValue = EditorGUI.IntField(position, label, property.intValue);
            if (fieldValue < minAttribute.minimum) {
                fieldValue = (int)minAttribute.minimum;
            }

            property.intValue = fieldValue;
        } else if (property.propertyType == SerializedPropertyType.Vector2Int) {
            Vector2Int fieldValue = EditorGUI.Vector2IntField(position, label, property.vector2IntValue);
            if (fieldValue.x < minAttribute.minimum) {
                fieldValue.x = (int)minAttribute.minimum;
            }
            if (fieldValue.y < minAttribute.minimum) {
                fieldValue.y = (int)minAttribute.minimum;
            }

            property.vector2IntValue = fieldValue;
        } else {
            EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
        }
    }
}