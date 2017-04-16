using UnityEngine;
using UnityEditor;
using System;
[CustomPropertyDrawer(typeof(GameDateTime))]
public class DateTimeEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {


        EditorGUI.BeginProperty(position, label, property);

        Rect daysRect = new Rect(position.x, position.y, 30, position.height);
        Rect hoursRect = new Rect(position.x + 35, position.y, 30, position.height);
        Rect minutesRect = new Rect(position.x + 70, position.y, 30, position.height);
        Rect secondsRect = new Rect(position.x + 105, position.y, 30, position.height);


        EditorGUI.PropertyField(daysRect, property.FindPropertyRelative("days"), GUIContent.none);
        EditorGUI.PropertyField(hoursRect, property.FindPropertyRelative("hours"), GUIContent.none);
        EditorGUI.PropertyField(minutesRect, property.FindPropertyRelative("minutes"), GUIContent.none);
        EditorGUI.PropertyField(secondsRect, property.FindPropertyRelative("seconds"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
