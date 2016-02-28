using UnityEngine;
using UnityEditor;

public class CreateBuildinSkins
{
	[MenuItem("Assets/CreateBuildinSkins")]
	static void Create()
	{
		BuildinSkins buildinSkins = ScriptableObject.CreateInstance<BuildinSkins>();
		
		AssetDatabase.CreateAsset( buildinSkins, "Assets/BuildinSkins.asset" );
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();   
	}
}
