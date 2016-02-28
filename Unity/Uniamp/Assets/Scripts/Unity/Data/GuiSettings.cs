using UnityEngine;

using Unity.GuiStyle;

using System.Collections.Generic;

namespace Unity.Data
{
	public static class GuiSettings
	{
		public static GuiSettingLoopTool GuiSettingLoopTool{ get; private set; }
		public static GuiSettingLoopEditor GuiSettingLoopEditor{ get; private set; }

		static GuiSettings()
		{
			GameObject obj = ( GameObject )Resources.Load( "Prefab/GuiSettings", typeof( GameObject ) );
			
			GuiSettingLoopTool = obj.GetComponent<GuiSettingLoopTool>();
			GuiSettingLoopEditor = obj.GetComponent<GuiSettingLoopEditor>();
		}
		
		public static void Reset( GameObject aObj )
		{
			GuiSettingLoopTool = aObj.GetComponent<GuiSettingLoopTool>();
			GuiSettingLoopEditor = aObj.GetComponent<GuiSettingLoopEditor>();
		}
	}
}
