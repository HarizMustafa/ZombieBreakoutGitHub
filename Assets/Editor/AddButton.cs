using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RandomSpawn))]
public class AddButton : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		RandomSpawn myScript = (RandomSpawn)target;
		if(GUILayout.Button("Refresh"))
		{
			if(myScript.StartMode == false)
			{
				if(myScript.PortalAmount <= myScript.Portals.Length)
				myScript.SetRandomNum();
				myScript.SetRandomLocation();
			}
		}
	}
}