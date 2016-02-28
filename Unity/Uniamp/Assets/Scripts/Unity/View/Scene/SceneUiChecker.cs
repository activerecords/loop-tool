using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;
using Unity.Function.Graphic;

using System;
using System.IO;

namespace Unity.View.LoopEditor
{
	public class SceneUiChecker : MonoBehaviour
	{
		void Awake()
		{
			camera.orthographicSize = Screen.height / 2.0f;
			Gui.camera = camera;
			GameObject obj = GameObject.Find( "GuiStyleSet" );
			GuiStyleSet.Reset( obj );
		}

		void Start()
		{

		}

		void Update()
		{

		}

		void OnGUI()
		{
			GuiStyleSet.SetGuiStyles();
			/*
			GUILayout.BeginArea( new Rect( 8.0f, 8.0f, Screen.width - 16.0f, Screen.height - 16.0f ) );
			{
				GUILayout.BeginHorizontal();
				{
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					TextArea();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					Label();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					HorizontalScrollbar();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					HorizontalSlider();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					Toggle();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonPrevious();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ToggleStartPause();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonNext();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					LabelTime();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonCircleMinus();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					Volumebar();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonCirclePlus();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();
				
				GUILayout.FlexibleSpace();

				GUILayout.BeginVertical();
				{
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					TextArea2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					Label2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					HorizontalScrollbar2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					HorizontalSlider2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					Toggle2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					ButtonPrevious2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					ToggleStartPause2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					ButtonNext2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					LabelTime2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					ButtonCircleMinus2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					Volumebar2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
					ButtonCirclePlus2();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndArea();*/
		}

		private void TextArea()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.TextArea( "TextArea", GuiStyleSet.StylePlayer.labelTitle );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}

		private void Label()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Label( "Label", GuiStyleSet.StyleGeneral.none );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void HorizontalScrollbar()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.HorizontalScrollbar( 0.25f, 0.5f, 0.0f, 1.0f, GuiStyleSet.StyleLoopTool.waveformbar );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void HorizontalSlider()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.HorizontalSlider( 0.5f, 0.0f, 1.0f, GuiStyleSet.StylePlayer.seekbar, GuiStyleSet.StylePlayer.seekbarThumb );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void Toggle()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Toggle( false, "", GuiStyleSet.StylePlayer.toggleLoop );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void ButtonPrevious()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Button( "", GuiStyleSet.StylePlayer.buttonPrevious );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void ToggleStartPause()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Button( "", GuiStyleSet.StylePlayer.toggleStartPause );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void ButtonNext()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Button( "", GuiStyleSet.StylePlayer.buttonNext );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void LabelTime()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Label( "Label", GuiStyleSet.StylePlayer.labelTime );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void ButtonCircleMinus()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Button( "-", GuiStyleSet.StyleGeneral.buttonCircle );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}

		private void Volumebar()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.HorizontalSlider( 0.5f, 0.0f, 1.0f, GuiStyleSet.StylePlayer.volumebar, GuiStyleSet.StyleSlider.horizontalbarThumb );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void ButtonCirclePlus()
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				GUILayout.Button( "+", GuiStyleSet.StyleGeneral.buttonCircle );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
			}
			GUILayout.EndVertical();
		}
		
		private void TextArea2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.TextArea( "TextArea", GuiStyleSet.StylePlayer.labelTitle );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}

		private void Label2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Label( "Label", GuiStyleSet.StyleGeneral.none );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void HorizontalScrollbar2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.HorizontalScrollbar( 0.25f, 0.5f, 0.0f, 1.0f, GuiStyleSet.StyleLoopTool.waveformbar );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void HorizontalSlider2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.HorizontalSlider( 0.5f, 0.0f, 1.0f, GuiStyleSet.StylePlayer.seekbar, GuiStyleSet.StylePlayer.seekbarThumb );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void Toggle2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Toggle( false, "", GuiStyleSet.StylePlayer.toggleLoop );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void ButtonPrevious2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Button( "", GuiStyleSet.StylePlayer.buttonPrevious );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void ToggleStartPause2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Button( "", GuiStyleSet.StylePlayer.toggleStartPause );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void ButtonNext2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Button( "", GuiStyleSet.StylePlayer.buttonNext );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void LabelTime2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Label( "Label", GuiStyleSet.StylePlayer.labelTime );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void ButtonCircleMinus2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Button( "-", GuiStyleSet.StyleGeneral.buttonCircle );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void Volumebar2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.HorizontalSlider( 0.5f, 0.0f, 1.0f, GuiStyleSet.StylePlayer.volumebar, GuiStyleSet.StyleSlider.horizontalbarThumb );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}
		
		private void ButtonCirclePlus2()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
				GUILayout.Button( "+", GuiStyleSet.StyleGeneral.buttonCircle );
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
			}
			GUILayout.EndHorizontal();
		}

		void OnRenderObject()
		{
			Gui.BeginArea( new Rect( 80.0f, 80.0f, Screen.width, Screen.height ) );
			{
				//GUILayout.BeginHorizontal();
				{
					//GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					Gui.Button( "", GuiStyleSet.StylePlayer.toggleStartPause );
					/*
					TextArea();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					Label();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					HorizontalScrollbar();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					HorizontalSlider();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					Toggle();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonPrevious();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ToggleStartPause();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonNext();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					LabelTime();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonCircleMinus();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					Volumebar();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					ButtonCirclePlus();
					GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionVertical" ), GuiStyleSet.StyleGeneral.partitionVertical );
					
					GUILayout.FlexibleSpace();
					*/
				}
				//GUILayout.EndHorizontal();
			}
			Gui.EndArea();
		}
		
		void OnApplicationQuit()
		{

		}
	}
}
