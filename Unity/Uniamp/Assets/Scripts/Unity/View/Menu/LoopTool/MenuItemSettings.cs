using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View.LoopTool
{
	public class MenuItemSettings : AMenuItem
	{
		private ApplicationLoopTool applicationLoopTool;
		private DialogSettingsLoop dialogSettingsLoop;
		private bool isShow;

		public MenuItemSettings( string aTitle, string aFilePathLanguage, ApplicationLoopTool aApplicationLoopTool )
			: base( aTitle )
		{
			Dictionary<string, string> lDictionaryDescription = ReadDictionaryLanguage( aFilePathLanguage );
			dialogSettingsLoop = new DialogSettingsLoop( ChangeSettings, lDictionaryDescription );
			applicationLoopTool = aApplicationLoopTool;
			isShow = false;
		}
		
		private void ChangeSettings( int a )
		{
			isShow = false;

			if( a == 1 )
			{
				applicationLoopTool.SetIsCutLast( false );
			}
			else
			{
				applicationLoopTool.SetIsCutLast( true );
			}
		}

		public override void Select()
		{
			isShow = true;
		}

		public override void OnGUI()
		{
			if( isShow == true )
			{
				dialogSettingsLoop.OnGUI();
			}
		}
	}
}
