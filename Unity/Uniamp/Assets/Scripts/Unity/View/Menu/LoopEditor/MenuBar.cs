using UnityEngine;

using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Boundary;

namespace Unity.View.LoopEditor
{
	public class MenuBar : AMenuBar
	{
		public MenuBar( string aFilePathLanguage, ApplicationLoopEditor aApplicationLoopTool )
		{
			Dictionary<string, string> lDictionaryDescription = ReadDictionaryLanguage( aFilePathLanguage );

			MenuBoxFile lMenuBoxFile = new MenuBoxFile( lDictionaryDescription["FILE"], Application.streamingAssetsPath + "/Language/LoopTool/Menu/MenuBar/MenuBoxFile.language", aApplicationLoopTool );
			MenuBoxTool lMenuBoxTool = new MenuBoxTool( lDictionaryDescription["TOOL"], Application.streamingAssetsPath + "/Language/LoopTool/Menu/MenuBar/MenuBoxTool.language", aApplicationLoopTool );

			menuBoxList.Add( lMenuBoxFile );
			menuBoxList.Add( lMenuBoxTool );

			Rect = new Rect( 0.0f, 0.0f, 0.0f, 0.0f );
		}
	}
}
