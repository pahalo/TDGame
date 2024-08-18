using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurretStats))]
public class TurretStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the turret stats object
        TurretStats turretStats = (TurretStats)target;

        // Display the turret type dropdown
        turretStats.turretType = (TurretStats.TurretType)EditorGUILayout.EnumPopup("Turret Type", turretStats.turretType);

        // Only display turretAttributes and turretModes if the turret is of type Support
        if (turretStats.turretType == TurretStats.TurretType.Support)
        {
            // Display the list of turret attributes with dropdowns and values
            EditorGUILayout.LabelField("Support Turret Attributes");
            SerializedProperty turretAttributesProperty = serializedObject.FindProperty("turretAttributes");

            for (int i = 0; i < turretAttributesProperty.arraySize; i++)
            {
                SerializedProperty attributeProperty = turretAttributesProperty.GetArrayElementAtIndex(i);
                SerializedProperty typeProperty = attributeProperty.FindPropertyRelative("attributeType");
                SerializedProperty valueProperty = attributeProperty.FindPropertyRelative("value");

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(typeProperty, GUIContent.none);
                EditorGUILayout.PropertyField(valueProperty, GUIContent.none);

                if (GUILayout.Button("Remove"))
                {
                    turretAttributesProperty.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Attribute"))
            {
                turretAttributesProperty.arraySize++;
                SerializedProperty newAttribute = turretAttributesProperty.GetArrayElementAtIndex(turretAttributesProperty.arraySize - 1);
                newAttribute.FindPropertyRelative("attributeType").enumValueIndex = 0; // Default to first enum value
                newAttribute.FindPropertyRelative("value").floatValue = 0f;
            }
        }

        // Display the properties specific to the Attack turret type
        if (turretStats.turretType == TurretStats.TurretType.Attack)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("turretTurnSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("turretDamage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("turretShotSpeed"));
        }
        // Display the properties specific to the Support turret type
        if (turretStats.turretType == TurretStats.TurretType.Support)
        {
            
        }

        // Display the properties common to both turret types
        EditorGUILayout.PropertyField(serializedObject.FindProperty("turretRange"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("turretDistanceToOtherTurrets"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("turretID"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("turretLevel"));

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
