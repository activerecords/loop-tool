using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Boundary;

namespace Unity.View.LoopEditor
{
	public class MenuBoxTool : AMenuBox
	{
		public MenuBoxTool( string aTitle, string aFilePathLanguage, ApplicationLoopEditor aApplicationLoopTool )
			: base( aTitle )
		{
			Dictionary<string, string> lDictionaryDescription = ReadDictionaryLanguage( aFilePathLanguage );

			menuItemList.Add( new MenuItemSettings( lDictionaryDescription["SETTINGS"], Application.streamingAssetsPath + "/Language/LoopTool/Dialog/DialogSettings.language", aApplicationLoopTool ) );
		}
	}
}
