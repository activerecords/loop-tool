using UnityEngine;

namespace Unity.GuiStyle
{
	public class StylePlayer : MonoBehaviour
	{
		public GUIStyle box;
		public GUIStyle labelTitle;
		public GUIStyle labelTime;
		public GUIStyle toggleStartPause;
		public GUIStyle toggleLoop;
		public GUIStyle buttonStop;
		public GUIStyle buttonPause;
		public GUIStyle buttonRecord;
		public GUIStyle buttonNext;
		public GUIStyle buttonPrevious;
		public GUIStyle toggleMute;
		public GUIStyle seekbar;
		public GUIStyle seekbarThumb;
		public GUIStyle seekbarThumbLoopEditor;
		public GUIStyle seekbarLeftButton;
		public GUIStyle seekbarRightButton;
		public GUIStyle volumebar;
		public GUIStyle volumebarThumb;
		
		private GUIStyle _seekbar;
		private GUIStyle _seekbarEditor;
		private GUIStyle _volumebar;

		public GUIStyle Seekbar{ get{ return _seekbar; } }
		public GUIStyle SeekbarEditor{ get{ return _seekbarEditor; } }
		public GUIStyle Volumebar{ get{ return _volumebar; } }

		void Awake()
		{
			_seekbar = new GUIStyle( seekbar );
			_seekbar.normal.background = null;
			_seekbar.hover.background = null;
			_seekbar.active.background = null;
			_seekbar.focused.background = null;
			_seekbar.onNormal.background = null;
			_seekbar.onHover.background = null;
			_seekbar.onActive.background = null;
			_seekbar.onFocused.background = null;
			
			_seekbarEditor = new GUIStyle( seekbar );
			_seekbarEditor.normal.background = null;
			_seekbarEditor.hover.background = null;
			_seekbarEditor.active.background = null;
			_seekbarEditor.focused.background = null;
			_seekbarEditor.onNormal.background = null;
			_seekbarEditor.onHover.background = null;
			_seekbarEditor.onActive.background = null;
			_seekbarEditor.onFocused.background = null;

			_volumebar = new GUIStyle( volumebar );
			_volumebar.normal.background = null;
			_volumebar.hover.background = null;
			_volumebar.active.background = null;
			_volumebar.focused.background = null;
			_volumebar.onNormal.background = null;
			_volumebar.onHover.background = null;
			_volumebar.onActive.background = null;
			_volumebar.onFocused.background = null;
		}

		void Update()
		{
			_seekbar.margin = seekbar.margin;
			_seekbar.padding = seekbar.padding;
			_seekbar.overflow = seekbar.overflow;
			_seekbar.border = seekbar.border;
			_seekbar.fixedWidth = seekbar.fixedWidth;
			_seekbar.fixedHeight = seekbar.fixedHeight;
			
			_volumebar.margin = volumebar.margin;
			_volumebar.padding = volumebar.padding;
			_volumebar.overflow = volumebar.overflow;
			_volumebar.border = volumebar.border;
			_volumebar.fixedWidth = volumebar.fixedWidth;
			_volumebar.fixedHeight = volumebar.fixedHeight;
			
			_seekbarEditor.margin = seekbar.margin;
			_seekbarEditor.padding = seekbar.padding;
			_seekbarEditor.overflow = seekbar.overflow;
			_seekbarEditor.border = seekbar.border;
			_seekbarEditor.fixedWidth = Screen.width;
			_seekbarEditor.fixedHeight = seekbar.fixedHeight;
		}
	}
}
