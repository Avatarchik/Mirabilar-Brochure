using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class TakeScreenShot : MonoBehaviour 
{
	public string albumName = "";
	bool isScreenShotSave;
	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 200, 50), "Take ScreenShot")) 
		{
			StartCoroutine(ScreenShotBridge.SaveScreenShot(albumName,ScreenShotStatus));	
			isScreenShotSave = false;
		}
		if(isScreenShotSave)
			GUI.Label(new Rect(50,50,100,50),"Saved To Gallery");
	}
	
	void ScreenShotStatus(bool status)
	{
		isScreenShotSave = status;
	}
}