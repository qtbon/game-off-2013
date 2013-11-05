using UnityEngine;
using UnityEditor;
using System.Collections;

public class Automation {

	public static void Build() {
		string[] scenes = { "Assets/Scenes/Menu.unity" };
		var buildInfo = BuildPipeline.BuildPlayer(scenes, string.Format("../Builds/ChangeGame{0}.apk", System.DateTime.Now.ToFileTimeUtc()), BuildTarget.Android, BuildOptions.None);
		EditorApplication.Exit(string.IsNullOrEmpty(buildInfo)? 0 : 1);
	}
	
}
