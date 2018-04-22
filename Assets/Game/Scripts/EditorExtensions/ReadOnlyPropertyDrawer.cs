﻿namespace FarmingShooter
{
	using UnityEngine;


	public class ReadOnlyAttribute : PropertyAttribute
	{
	}
}


#region Editor
#if UNITY_EDITOR

namespace FarmingShooter
{
	using UnityEditor;
	using UnityEngine;


	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(
			SerializedProperty property,
			GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}


		public override void OnGUI(
			Rect position,
			SerializedProperty property,
			GUIContent label)
		{
			GUI.enabled = false;
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true;
		}
	}
}

#endif
#endregion