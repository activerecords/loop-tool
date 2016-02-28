using UnityEngine;

using Unity.GuiStyle;

using System.Collections.Generic;

namespace Unity.Data
{
	public static class GuiStyleSet
	{
		public static bool isSetGuiStyle{ get; private set; }

		public static StyleFolder StyleFolder{ get; private set; }
		public static StyleGeneral StyleGeneral{ get; private set; }
		public static StyleList StyleList{ get; private set; }
		public static StyleLoopTool StyleLoopTool{ get; private set; }
		public static StyleMenu StyleMenu{ get; private set; }
		public static StylePlayer StylePlayer{ get; private set; }
		public static StyleProgressbar StyleProgressbar{ get; private set; }
		public static StyleScrollbar StyleScrollbar{ get; private set; }
		public static StyleSlider StyleSlider{ get; private set; }
		public static StyleTable StyleTable{ get; private set; }
		public static StyleWindow StyleWindow{ get; private set; }

		static GuiStyleSet()
		{
			isSetGuiStyle = false;
			GameObject obj = ( GameObject )Resources.Load( "Prefab/GuiStyleSet", typeof( GameObject ) );

			StyleFolder = obj.GetComponent<StyleFolder>();
			StyleGeneral = obj.GetComponent<StyleGeneral>();
			StyleList = obj.GetComponent<StyleList>();
			StyleLoopTool = obj.GetComponent<StyleLoopTool>();
			StyleMenu = obj.GetComponent<StyleMenu>();
			StylePlayer = obj.GetComponent<StylePlayer>();
			StyleProgressbar = obj.GetComponent<StyleProgressbar>();
			StyleScrollbar = obj.GetComponent<StyleScrollbar>();
			StyleSlider = obj.GetComponent<StyleSlider>();
			StyleTable = obj.GetComponent<StyleTable>();
			StyleWindow = obj.GetComponent<StyleWindow>();
		}
		
		public static void Reset( GameObject aObj )
		{
			isSetGuiStyle = false;

			StyleFolder = aObj.GetComponent<StyleFolder>();
			StyleGeneral = aObj.GetComponent<StyleGeneral>();
			StyleList = aObj.GetComponent<StyleList>();
			StyleLoopTool = aObj.GetComponent<StyleLoopTool>();
			StyleMenu = aObj.GetComponent<StyleMenu>();
			StylePlayer = aObj.GetComponent<StylePlayer>();
			StyleProgressbar = aObj.GetComponent<StyleProgressbar>();
			StyleScrollbar = aObj.GetComponent<StyleScrollbar>();
			StyleSlider = aObj.GetComponent<StyleSlider>();
			StyleTable = aObj.GetComponent<StyleTable>();
			StyleWindow = aObj.GetComponent<StyleWindow>();
		}

		public static void SetGuiStyles()
		{
			if( isSetGuiStyle == false )
			{
				isSetGuiStyle = true;
				
				Dictionary<string, GUIStyle> guiStyleDictionary = new Dictionary<string, GUIStyle>();
				
				GuiStyleSet.StyleLoopTool.nullbar.name =  "nullbarbar";
				GuiStyleSet.StyleLoopTool.nullbarThumb.name = "nullbarbarthumb";
				GuiStyleSet.StyleLoopTool.nullbarLeftButton.name = "nullbarbarleftbutton";
				GuiStyleSet.StyleLoopTool.nullbarRightButton.name = "nullbarbarrightbutton";
				
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.nullbar.name, GuiStyleSet.StyleLoopTool.nullbar );
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.nullbarThumb.name, GuiStyleSet.StyleLoopTool.nullbarThumb );
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.nullbarLeftButton.name, GuiStyleSet.StyleLoopTool.nullbarLeftButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.nullbarRightButton.name, GuiStyleSet.StyleLoopTool.nullbarRightButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbar.name, GuiStyleSet.StyleScrollbar.verticalbar );
				
				GuiStyleSet.StyleLoopTool.waveformbar.name =  "waveformbar";
				GuiStyleSet.StyleLoopTool.waveformbarThumb.name = "waveformbarthumb";
				GuiStyleSet.StyleLoopTool.waveformbarLeftButton.name = "waveformbarleftbutton";
				GuiStyleSet.StyleLoopTool.waveformbarRightButton.name = "waveformbarrightbutton";
				
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.waveformbar.name, GuiStyleSet.StyleLoopTool.waveformbar );
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.waveformbarThumb.name, GuiStyleSet.StyleLoopTool.waveformbarThumb );
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.waveformbarLeftButton.name, GuiStyleSet.StyleLoopTool.waveformbarLeftButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleLoopTool.waveformbarRightButton.name, GuiStyleSet.StyleLoopTool.waveformbarRightButton );

				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbarThumb.name, GuiStyleSet.StyleScrollbar.verticalbarThumb );
				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbarUpButton.name, GuiStyleSet.StyleScrollbar.verticalbarUpButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbarDownButton.name, GuiStyleSet.StyleScrollbar.verticalbarDownButton );
				
				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbar.name, GuiStyleSet.StyleScrollbar.horizontalbar );
				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbarThumb.name, GuiStyleSet.StyleScrollbar.horizontalbarThumb );
				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbarLeftButton.name, GuiStyleSet.StyleScrollbar.horizontalbarLeftButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbarRightButton.name, GuiStyleSet.StyleScrollbar.horizontalbarRightButton );

				guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbar.name, GuiStyleSet.StyleProgressbar.progressbar );
				guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbarThumb.name, GuiStyleSet.StyleProgressbar.progressbarThumb );
				guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbarLeftButton.name, GuiStyleSet.StyleProgressbar.progressbarLeftButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbarRightButton.name, GuiStyleSet.StyleProgressbar.progressbarRightButton );
				
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeader.name, GuiStyleSet.StyleTable.verticalbarHeader );
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeaderThumb.name, GuiStyleSet.StyleTable.verticalbarHeaderThumb );
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeaderUpButton.name, GuiStyleSet.StyleTable.verticalbarHeaderUpButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeaderDownButton.name, GuiStyleSet.StyleTable.verticalbarHeaderDownButton );
				
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeader.name, GuiStyleSet.StyleTable.horizontalbarHeader );
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeaderThumb.name, GuiStyleSet.StyleTable.horizontalbarHeaderThumb );
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeaderLeftButton.name, GuiStyleSet.StyleTable.horizontalbarHeaderLeftButton );
				guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeaderRightButton.name, GuiStyleSet.StyleTable.horizontalbarHeaderRightButton );

				if( GUI.skin.GetStyle( GuiStyleSet.StyleLoopTool.nullbar.name ).name == "" )
				{
					GUIStyle[] lCustomStylesAfter = new GUIStyle[GUI.skin.customStyles.Length + guiStyleDictionary.Count];
					
					for( int i = 0; i < lCustomStylesAfter.Length; i++ )
					{
						lCustomStylesAfter[i] = lCustomStylesAfter[i];
					}
					
					int lIndex = 0;
					
					foreach( KeyValuePair<string, GUIStyle> l in guiStyleDictionary )
					{
						lCustomStylesAfter[GUI.skin.customStyles.Length + lIndex] = l.Value;
						lIndex++;
					}
					
					GUI.skin.customStyles = lCustomStylesAfter;
				}
				else
				{
					for( int i = 0; i < GUI.skin.customStyles.Length; i++ )
					{
						if( guiStyleDictionary.ContainsKey( GUI.skin.customStyles[i].name ) == true )
						{
							GUI.skin.customStyles[i] = guiStyleDictionary[GUI.skin.customStyles[i].name];
						}
					}
					
					GUI.skin.customStyles = GUI.skin.customStyles;
				}
			}
		}
	}
}
