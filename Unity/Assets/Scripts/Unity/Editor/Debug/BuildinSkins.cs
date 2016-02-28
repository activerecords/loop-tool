using UnityEngine;
using UnityEditor;

public class BuildinSkins: ScriptableObject
{
	public GUISkin game = EditorGUIUtility.GetBuiltinSkin( EditorSkin.Game );
	public GUISkin inspector = EditorGUIUtility.GetBuiltinSkin( EditorSkin.Inspector );
	public GUISkin scene = EditorGUIUtility.GetBuiltinSkin( EditorSkin.Scene );
}
