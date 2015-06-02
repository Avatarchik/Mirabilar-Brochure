using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class ScreenShotBridge 
{
#if UNITY_IOS
	public delegate void ScreenShotDelegate(bool screenShotStatus);
	[DllImport("__Internal")]
	private static extern bool _SaveScreenshotToGallery(string albumName);
	public static IEnumerator SaveScreenShot(string albumName,ScreenShotDelegate callBack)
	{
		yield return new WaitForEndOfFrame ();
		Application.CaptureScreenshot ("Screenshot.png");
		bool screenShotStatus = _SaveScreenshotToGallery (albumName);
		callBack(screenShotStatus);
	}
#endif
}
