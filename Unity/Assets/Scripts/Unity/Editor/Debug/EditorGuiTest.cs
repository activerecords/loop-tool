using UnityEngine;
using UnityEditor;

using Unity.Data;
using Unity.GuiStyle;

using System;

namespace LayerEditor.Information
{
	public enum EnumHoge
	{
		Hoge = 1,
		Foo  = 2,
		Mofu = 4,
		Unyu = 8,
	}

	public class EditorGuiTest : EditorWindow
	{
		bool isEditable;
		bool childToggleBool = true;
		float childSliderFloat = 2.22f;

		[MenuItem( "Window/Debug/Gui Test" )]
		public static void Init()
		{
			EditorWindow.GetWindow<EditorGuiTest>( false, "Gui Test" );
		}
		
		void OnInspectorUpdate()
		{
			Repaint();
		}
		
		void OnGUI()
		{
			GUILayout.Label("DispTest", EditorStyles.boldLabel);
			int sw = Screen.width;
			int sh = Screen.height;
			isEditable = EditorGUILayout.BeginToggleGroup("ToggleGroupTest", isEditable );
			childToggleBool = GUILayout.Toggle(childToggleBool, "childToggle", GuiStyleSet.StyleGeneral.toggleRadio );
			childSliderFloat = EditorGUILayout.Slider("childSlider", childSliderFloat, -10, 10);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EnumPopup( "EnumPopup", EnumHoge.Foo, GuiStyleSet.StyleGeneral.toggleRadio );

		}
	}
}
