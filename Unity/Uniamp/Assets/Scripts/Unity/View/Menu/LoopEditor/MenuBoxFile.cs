using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Boundary;

namespace Unity.View.LoopEditor
{
	public class MenuBoxFile : AMenuBox
	{
		public MenuBoxFile( string aTitle, string aFilePathLanguage, ApplicationLoopEditor aApplicationLoopTool )
			: base( aTitle )
		{
			Dictionary<string, string> lDictionaryDescription = ReadDictionaryLanguage( aFilePathLanguage );
			
			MenuItemChangeDirectory lMenuItemChangeDirectoryInput = new MenuItemChangeDirectory( lDictionaryDescription["INPUT"], aApplicationLoopTool.SetInput, aApplicationLoopTool.directoryInfoRecentInputList );
			MenuItemChangeDirectory lMenuItemChangeDirectoryOutput = new MenuItemChangeDirectory( lDictionaryDescription["OUTPUT"], aApplicationLoopTool.SetOutput, aApplicationLoopTool.directoryInfoRecentOutputList );
			
			menuItemList.Add( lMenuItemChangeDirectoryInput );
			menuItemList.Add( lMenuItemChangeDirectoryOutput );
		}
	}
}
