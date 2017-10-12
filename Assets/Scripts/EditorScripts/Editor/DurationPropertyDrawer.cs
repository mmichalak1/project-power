using UnityEngine;
using System;
using UnityEditor;

[CustomPropertyDrawer(typeof(Duration))]
public class DurationPropertyDrawer : PropertyDrawer
{
    SerializedProperty days, hours, minutes, seconds;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        days = property.FindPropertyRelative("days");
        hours = property.FindPropertyRelative("hours");
        minutes = property.FindPropertyRelative("minutes");
        seconds = property.FindPropertyRelative("seconds");

        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        EditorGUIUtility.labelWidth = 18f;
        if (position.height > 16f)
        {
            position.height = 16f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 18f;
        }
        contentPosition.width *= 0.25f;
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, days, new GUIContent("D:"));
        contentPosition.x += contentPosition.width;
        EditorGUI.PropertyField(contentPosition, hours, new GUIContent("H:"));
        contentPosition.x += contentPosition.width;
        EditorGUI.PropertyField(contentPosition, minutes, new GUIContent("M:"));
        contentPosition.x += contentPosition.width;
        EditorGUI.PropertyField(contentPosition, seconds, new GUIContent("S:"));
        contentPosition.x += contentPosition.width;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return Screen.width < 333 ? (16f + 18f) : 16f;
    }
}
